using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour.HookGen;
using RandomModCompat.Common.Configs;
using RandomModCompat.Common.Edits;
using RandomModCompat.Utilities;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheDepths;
using TheDepths.Tiles;

namespace RandomModCompat.Common.APIs;

internal sealed partial class TheDepthsAPI
{
	[JITWhenModsEnabled(ModNames.TheDepths)]
	private sealed class TheDepthsAPISystem : ModSystem
	{
		/// <summary>
		/// The number of merging shale tiles added by The Depths.
		/// </summary>
		private static int _originalShaleTileCount;

		/// <summary>
		/// Tiles that merge with all shale tiles.
		/// </summary>
		private static readonly List<int> _mergingShaleTiles = new();

		/// <summary>
		/// The frequencies of each tile in <see cref="_mergingShaleTiles"/> past <see cref="_originalShaleTileCount"/>.
		/// </summary>
		private static readonly List<float> _customFrequencies = new();

		/// <summary>
		/// The generated texture for each added shale tile.
		/// </summary>
		private static readonly Dictionary<int, Asset<Texture2D>> _tileTypeToAsset = new();

		/// <summary>
		/// The texture of <see cref="Shalestone"/>.
		/// </summary>
		private static Texture2D _shaleTexture;

		/// <summary>
		/// The method in <see cref="TheDepthsWorldGen"/> that generates shalestone gems.
		/// </summary>
		private static MethodInfo _gemsMethod;

		public override bool IsLoadingEnabled(Mod mod)
		{
			if (!ModLoader.HasMod(ModNames.TheDepths))
			{
				// Expected behaviour, don't log.
				return false;
			}

			if (ModContent.GetInstance<RandomModCompatConfig>().DisableIL)
			{
				Mod.Logger.Info("Not loading The Depths shale gems because IL edits are disabled.");
				return false;
			}

			if (ModTileTextureBypass.Failed)
			{
				Mod.Logger.Info("Not loading The Depths shale gems because ModTile texture bypassing failed.");
				return false;
			}

			if (!LoadGemsMethod())
			{
				Mod.Logger.Info("Not loading The Depths shale gems because the generation method could not be found.");
				return false;
			}

			return ModLoader.HasMod(ModNames.TheDepths);
		}

		// JIT issues
		private static bool LoadGemsMethod()
		{
			_gemsMethod = typeof(TheDepthsWorldGen).GetMethods(ReflectionHelper.AllFlags).FirstOrDefault(m => m.Name.Contains("Gems"));
			return _gemsMethod != null;
		}

		public override void Load()
		{
			_mergingShaleTiles.AddRange(new[]
			{
				ModContent.TileType<ShaleBlock>(),
				ModContent.TileType<Shalestone>(),
				ModContent.TileType<ShalestoneAmethyst>(),
				ModContent.TileType<ShalestoneDiamond>(),
				ModContent.TileType<ShalestoneEmerald>(),
				ModContent.TileType<ShalestoneRuby>(),
				ModContent.TileType<ShalestoneSapphire>(),
				ModContent.TileType<ShalestoneTopaz>(),
				ModContent.TileType<OnyxShalestone>()
			});
			_originalShaleTileCount = _mergingShaleTiles.Count;

			//MethodInfo gemsMethod = typeof(TheDepthsWorldGen).GetMethods(ReflectionHelper.AllFlags).FirstOrDefault(m => m.Name.Contains("Gems"));
			//if (gemsMethod)
			HookEndpointManager.Modify(_gemsMethod, AddNewGems);
		}

		public override void Unload()
		{
			if (_gemsMethod != null)
			{
				HookEndpointManager.Unmodify(_gemsMethod, AddNewGems);
			}

			_tileTypeToAsset?.Clear();
			_mergingShaleTiles?.Clear();
			_shaleTexture = null;
			_gemsMethod = null;
		}

		private static void AddNewGems(ILContext il)
		{
			ILCursor c = new(il);

			// Match (C#):
			//	for (int i = 63; i <= 68; i++)
			// Match (IL):
			//	ldc.i4.s 63
			//	stloc.0
			//	br LABEL
			// Finding the end-of-loop check.
			int loopIndex = -1;
			ILLabel endOfLoopLabel = null;
			if (!c.TryGotoNext(MoveType.Before,
				i => i.MatchLdcI4(TileID.Sapphire),
				i => i.MatchStloc(out loopIndex),
				i => i.MatchBr(out endOfLoopLabel)
				))
			{
				throw new Exception("Couldn't find end-of-loop label.");
			}

			// Match (C#):
			//	float num2 = num * 0.2f;
			// Match (IL):
			//	ldloc x
			//	ldc.r4 0.2
			//	mul
			//	stloc y
			// Local 'x' here is the "frequency" value passed into AddNewTile().
			int frequencyIndex = -1;
			if (!c.TryGotoNext(MoveType.Before,
				i => i.MatchLdloc(out frequencyIndex),
				i => i.MatchLdcR4(0.2f),
				i => i.MatchMul(),
				i => i.MatchStloc(out _)
				))
			{
				throw new Exception("Couldn't find frequency local.");
			}

			// Match (C#):
			//	WorldGen.TileRunner(num3, num4, WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(3, 7), type);
			// Match (IL):
			//	ldloc x
			//	ldc.i4.0
			//	ldc.r4 0.0
			//	ldc.r4 0.0
			//	ldc.i4.0
			//	ldc.i4.1
			//	ldc.i4.m1
			//	call void [tModLoader]Terraria.WorldGen::TileRunner(int32, int32, float64, int32, int32, bool, float32, float32, bool, bool, int32)
			// Get the tile type local. It's easier to go from the back, since all of the default values are always the same.
			int typeIndex = -1;
			if (!c.TryGotoNext(MoveType.Before,
				i => i.MatchLdloc(out typeIndex),
				i => i.MatchLdcI4(0),
				i => i.MatchLdcR4(0f),
				i => i.MatchLdcR4(0f),
				i => i.MatchLdcI4(0),
				i => i.MatchLdcI4(1),
				i => i.MatchLdcI4(-1),
				i => i.MatchCall<WorldGen>(nameof(WorldGen.TileRunner))
				))
			{
				throw new Exception("Couldn't find type local.");
			}

			// Back to the beginning.
			c.Index = 0;

			// Match (C#):
			//	float num = 0f;
			// Match (IL):
			//	ldc.r4.0
			//	stloc frequencyIndex
			// Right after this assignment is where the custom code will go.
			if (!c.TryGotoNext(MoveType.After,
				i => i.MatchLdcR4(0f),
				i => i.MatchStloc(frequencyIndex)
				))
			{
				throw new Exception("Couldn't find frequency init.");
			}

			// GetCustomGemInfo(i, ref type, ref frequency);
			// This runs before The Depths' logic and only when type > 68 (which The Depths doesn't do anything with).
			c.Emit(OpCodes.Ldloc, loopIndex);
			c.Emit(OpCodes.Ldloca, typeIndex);
			c.Emit(OpCodes.Ldloca, frequencyIndex);
			c.EmitDelegate(GetCustomGemInfo);

			// Move to the bounds check.
			c.GotoLabel(endOfLoopLabel, MoveType.Before);

			// Match (C#):
			//	... i <= 68; ...
			// Match (IL):
			//	ldloc loopIndex
			//	ldc.i4.s 68
			//	ble _
			// Increase the upper bound of the loop to (68 + (_mergingShaleTiles.Count - _originalShaleTileCount))
			if (!c.TryGotoNext(MoveType.Before,
				i => i.MatchLdloc(loopIndex),
				i => i.MatchLdcI4(TileID.Diamond)
				))
			{
				throw new Exception("Couldn't find loop bounds check.");
			}

			c.Index += 2; // Prev = LdcI4
			c.EmitDelegate((int oldBounds) => oldBounds + (_mergingShaleTiles.Count - _originalShaleTileCount));
		}

		private static void GetCustomGemInfo(int loopIndex, ref int type, ref float frequency)
		{
			// Original loop only goes to 68 (TileID.Diamond), so anything higher is custom.
			if (loopIndex <= 68)
			{
				return;
			}

			int index = loopIndex - (68 + 1); // 69 => 0, 70 => 1, etc.
			type = _mergingShaleTiles[_originalShaleTileCount + index];
			frequency = _customFrequencies[index] * Main.maxTilesX;
		}

		public override void SetStaticDefaults()
		{
			if (_mergingShaleTiles.Count != _originalShaleTileCount)
			{
				UpdateTileMerging();
			}
		}

		/// <summary>
		/// Updates all shale tile merging to merge with newly-added tiles.
		/// </summary>
		private static void UpdateTileMerging()
		{
			foreach (int baseMergingTile in _mergingShaleTiles)
			{
				foreach (int otherMergingTile in _mergingShaleTiles)
				{
					if (baseMergingTile == otherMergingTile)
					{
						continue;
					}

					Main.tileMerge[baseMergingTile][otherMergingTile] = true;
				}
			}
		}

		/// <summary>
		/// Adds a type of gem to spawn in The Depths.
		/// </summary>
		/// <param name="mod">The mod to add the new tile to.</param>
		/// <param name="gemItemId">The item ID of the gem item. Needed for drops. (Ex: <see cref="ItemID.Amethyst"/>)</param>
		/// <param name="gemTileId">The tile ID of the "embedded" gem tile. (Ex: <see cref="TileID.Amethyst"/>)</param>
		/// <param name="gemBaseTileId">The tile ID of the tile <paramref name="gemTileId"/> is based off of. (Ex: <see cref="TileID.Stone"/>)</param>
		/// <param name="frequency">How much of this gem should spawn. Scales with <see cref="Main.maxTilesX"/>. For reference, Amethyst uses <c>0.5f</c>, while Diamond uses <c>0.05f</c>.</param>
		internal static void AddNewTile(Mod mod, int gemItemId, int gemTileId, int gemBaseTileId, float frequency)
		{
			string internalName = ItemLoader.GetItem(gemItemId).Name;
			ShaleGemTile instance = new(gemItemId, gemTileId, internalName);
			mod.AddContent(instance);
			_mergingShaleTiles.Add(instance.Type);
			_customFrequencies.Add(frequency);
			Main.QueueMainThreadAction(() => GenerateTextureForTile(instance.Type, gemTileId, gemBaseTileId, internalName));
		}

		private static void GenerateTextureForTile(int tileType, int gemTileId, int gemBaseTileId, string gemInternalName)
		{
			static Texture2D GetTileTexture(int id)
			{
				string path = id < TileID.Count ? $"Terraria/Images/Tiles_{id}" : TileLoader.GetTile(id).Texture;
				return ModContent.Request<Texture2D>(path, AssetRequestMode.ImmediateLoad).Value;
			}

			_shaleTexture ??= GetTileTexture(ModContent.TileType<Shalestone>());
			Texture2D gems = TextureHelper.GetOverlaidTexture(GetTileTexture(gemBaseTileId), GetTileTexture(gemTileId));
			Texture2D shaleGems = TextureHelper.OverlayTextures(_shaleTexture, gems);
			_tileTypeToAsset[tileType] = TextureHelper.CreateAssetFromTexture(shaleGems, $"RandomModCompat/Assets/TheDepths/Shalestone{gemInternalName}", ModContent.GetInstance<TheDepthsAPISystem>().Mod);
		}

		internal static Asset<Texture2D> GetTexture(ushort type)
		{
			// If this fails, let it throw.
			return _tileTypeToAsset[type];
		}
	}
}