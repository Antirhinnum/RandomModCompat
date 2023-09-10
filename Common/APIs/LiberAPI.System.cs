using Liber;
using Liber.Content.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RandomModCompat.Utilities;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace RandomModCompat.Common.APIs;

internal sealed partial class LiberAPI
{
	/// <summary>
	/// A generic pedestal tile, akin to vanillas Master Mode relics.
	/// </summary>
	[Autoload(false)]
	internal sealed class GemPedestal : ModType
	{
		[Autoload(false)]
		[JITWhenModsEnabled(ModNames.Liber)]
		private sealed class GemPedestalItem : ModItem
		{
			private const string _name = "Mods.RandomModCompat.Liber.GemPedestalItem";
			private readonly int _gemType;
			private readonly string _gemInternalName;
			internal int tileType;

			public override string Name => $"GemRelic{_gemInternalName}";
			protected override bool CloneNewInstances => true;

#if !TML_2022_09
			public override LocalizedText DisplayName => Language.GetText(_name).WithFormatArgs(Lang.GetItemName(_gemType));
#endif

			public GemPedestalItem(int gemType, string gemInternalName)
			{
				_gemType = gemType;
				_gemInternalName = gemInternalName;
			}

#if TML_2022_09
			public override void Unload()
			{
				LanguageManager.Instance.OnLanguageChanged -= UpdateItemName;
			}
#endif

			public override void AutoStaticDefaults()
			{
				if (Main.dedServ)
				{
					return;
				}

				Main.RunOnMainThread(() =>
				{
					GemRelicItemTextureCreator creator = GemRelicItemTextureCreator.Instance;
					creator.UseItem(_gemType);
					creator.Request();
					creator.PrepareRenderTarget(Main.graphics.GraphicsDevice, Main.spriteBatch);
					TextureAssets.Item[Item.type] = creator.ToAsset($"RandomModCompat/Assets/Liber/GemRelic{_gemInternalName}", RandomModCompat.Instance);
				});
			}

#if TML_2022_09
			public override void SetStaticDefaults()
			{
				DisplayName.SetDefault(_name);
				SacrificeTotal = 1;
				LanguageManager.Instance.OnLanguageChanged += UpdateItemName;
			}
#endif

			public override void SetDefaults()
			{
				Item.DefaultToPlaceableTile(tileType);
				Item.width = 30;
				Item.height = 40;
				Item.maxStack = 99;
				Item.rare = ItemRarityID.Green;
				Item.value = Item.sellPrice(1, 50);

#if TML_2022_09
				UpdateItemName(LanguageManager.Instance);
#endif
			}

			public override void AddRecipes()
			{
				CreateRecipe(10)
					.AddIngredient(_gemType, 15)
					.AddRecipeGroup(LiberRecipes.anySilver, 10)
					.AddIngredient(ItemID.FallenStar, 5)
					.AddTile<TransmutationTable>()
					.Register();
			}

#if TML_2022_09
			private void UpdateItemName(LanguageManager languageManager)
			{
				Item.SetNameOverride(languageManager.GetTextValue(_name, Lang.GetItemNameValue(_gemType)));
			}
#endif
		}

		[Autoload(false)]
		private sealed class GemPedestalTile : ModTile
		{
			private const string PedestalTexturePath = "Liber/Content/Tiles/GemRelics/GemRelicPedestal";
			public const int FrameWidth = 54;
			public const int FrameHeight = 72;
			public const int HorizontalFrames = 1;
			public const int VerticalFrames = 1;

			private readonly int _largeGemType;
			private readonly string _gemInternalName;
			internal int itemType;

			public override string Name => $"GemRelic{_gemInternalName}Tile";
			public override string Texture => PedestalTexturePath;

			public GemPedestalTile(int largeGemType, string gemInternalName)
			{
				_largeGemType = largeGemType;
				_gemInternalName = gemInternalName;
			}

			public override void SetStaticDefaults()
			{
				Main.tileFrameImportant[Type] = true;
				TileID.Sets.InteractibleByNPCs[Type] = true;
				TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
				TileObjectData.newTile.LavaDeath = false;
				TileObjectData.newTile.DrawYOffset = 2;
				TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
				TileObjectData.newTile.StyleHorizontal = false;
				TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
				TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
				TileObjectData.addAlternate(1);
				TileObjectData.addTile(Type);
				AddMapEntry(new Color(233, 207, 94), Language.GetText("MapObject.Relic"));
			}

			public override void KillMultiTile(int i, int j, int frameX, int frameY)
			{
				Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, itemType);
			}

			public override bool CreateDust(int i, int j, ref int type)
			{
				return false;
			}

			public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
			{
				tileFrameX %= 54;
				tileFrameY %= 144;
			}

			public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
			{
				if (drawData.tileFrameX % 54 == 0 && drawData.tileFrameY % 72 == 0)
				{
					Main.instance.TilesRenderer.AddSpecialLegacyPoint(i, j);
				}
			}

			public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
			{
				Vector2 offScreen = new(Main.offScreenRange);
				if (Main.drawToScreen)
				{
					offScreen = Vector2.Zero;
				}

				Point p = new(i, j);
				Tile tile = Main.tile[p.X, p.Y];
				if (tile == null || !tile.HasTile)
				{
					return;
				}

				Texture2D texture = TextureAssets.Item[_largeGemType].Value;

				int frameY = tile.TileFrameX / FrameWidth;
				Rectangle frame = texture.Frame(HorizontalFrames, VerticalFrames, 0, frameY);

				Vector2 origin = frame.Size() / 2f;
				Vector2 worldPos = p.ToWorldCoordinates(24f, 64f);

				Color color = Lighting.GetColor(p.X, p.Y);

				bool direction = tile.TileFrameY / FrameHeight != 0;
				SpriteEffects effects = direction ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

				const float TwoPi = (float)Math.PI * 2f;
				float offset = (float)Math.Sin(Main.GlobalTimeWrappedHourly * TwoPi / 5f);
				Vector2 drawPos = worldPos + offScreen - Main.screenPosition + new Vector2(0f, -40f) + new Vector2(0f, offset * 4f);
				//drawPos += frameOffset;

				spriteBatch.Draw(texture, drawPos, frame, color, 0f, origin, 1f, effects, 0f);

				float scale = (float)Math.Sin(Main.GlobalTimeWrappedHourly * TwoPi / 2f) * 0.3f + 0.7f;
				Color effectColor = color;
				effectColor.A = 0;
				effectColor = effectColor * 0.1f * scale;
				for (float num5 = 0f; num5 < 1f; num5 += 355f / (678f * (float)Math.PI))
				{
					spriteBatch.Draw(texture, drawPos + (TwoPi * num5).ToRotationVector2() * (6f + offset * 2f), frame, effectColor, 0f, origin, 1f, effects, 0f);
				}
			}
		}

		private readonly int _gemType;
		private readonly int _largeGemType;
		private readonly string _gemInternalName;

		public override string Name => $"{_gemInternalName}GemPedestal";

		internal GemPedestal(int gemType, int largeGemType)
		{
			_gemType = gemType;
			_largeGemType = largeGemType;
			_gemInternalName = ItemLoader.GetItem(_gemType).Name;
		}

		public override void Load()
		{
			GemPedestalItem item = new(_gemType, _gemInternalName);
			GemPedestalTile tile = new(_largeGemType, _gemInternalName);

			Mod.AddContent(item);
			Mod.AddContent(tile);

			item.tileType = tile.Type;
			tile.itemType = item.Type;
		}

		protected override void Register()
		{
			ModTypeLookup<GemPedestal>.Register(this);
		}
	}

	/// <summary>
	/// Creates textures for the Gem Relic items from Liber.
	/// </summary>
	[Autoload(Side = ModSide.Client)]
	internal sealed class GemRelicItemTextureCreator : ARenderTargetContentByRequest, ILoadable
	{
		internal static GemRelicItemTextureCreator Instance;

		private const string BaseAssetPath = "RandomModCompat/Assets/Liber/GemRelicItemBase";
		private static Asset<Texture2D> _baseAsset;
		private int _itemId;

		void ILoadable.Load(Mod mod)
		{
			Instance = new();
			Main.ContentThatNeedsRenderTargets.Add(Instance);
			_baseAsset = ModContent.Request<Texture2D>(BaseAssetPath);
		}

		void ILoadable.Unload()
		{
			Main.ContentThatNeedsRenderTargets.Remove(Instance);
			Instance = null;
			_baseAsset = null;
		}

		internal void UseItem(int itemId)
		{
			_itemId = itemId;
		}

		protected override void HandleUseReqest(GraphicsDevice device, SpriteBatch spriteBatch)
		{
			const int BaseWidth = 18;
			const int BaseHeight = 10;
			const int Spacing = 2;

			Asset<Texture2D> itemAsset = TextureAssets.Item[_itemId];
			Vector2 itemSize = itemAsset.Size();
			int totalHeight = BaseHeight + Spacing + (int)itemSize.Y;
			int itemX = (int)(BaseWidth - itemSize.X) / 2; // TODO: Mixels?

			PrepareARenderTarget_AndListenToEvents(ref _target, device, BaseWidth, totalHeight, RenderTargetUsage.PreserveContents);
			device.SetRenderTarget(_target);
			device.Clear(Color.Transparent);
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, Main.DefaultSamplerState, DepthStencilState.Default, Main.Rasterizer);

			spriteBatch.Draw(_baseAsset.Value, new Vector2(0f, totalHeight), null, Color.White, 0f, _baseAsset.Frame().BottomLeft(), 1f, SpriteEffects.None, 0f);
			spriteBatch.Draw(itemAsset.Value, new Vector2(itemX, 0f), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

			spriteBatch.End();
			device.SetRenderTarget(null);
			_wasPrepared = true;
		}
	}
}