using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Map;
using Terraria.ModLoader;
using TheDepths.Dusts;

namespace RandomModCompat.Common.APIs;

internal sealed partial class TheDepthsAPI
{
	/// <summary>
	/// A general imitation of "The Depth"'s Shale gem tiles.
	/// </summary>
	[JITWhenModsEnabled(ModNames.TheDepths)]
	[Autoload(false)]
	private sealed class ShaleGemTile : ModTile, ILoadOwnTexture
	{
		private readonly int _gemItemId;
		private readonly int _gemTileId;
		private readonly string _gemInternalName;

		public ShaleGemTile(int gemItemId, int gemTileId, string gemInternalName)
		{
			_gemItemId = gemItemId;
			_gemTileId = gemTileId;
			_gemInternalName = gemInternalName;
		}

		public override string Name => $"Shalestone{_gemInternalName}";

		Asset<Texture2D> ILoadOwnTexture.GetTexture()
		{
			return TheDepthsAPISystem.GetTexture(Type);
		}

		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			DustType = ModContent.DustType<ShaleDust>();
			HitSound = SoundID.Tink;
			MinPick = 65;

#if TML_2022_09
			ItemDrop = _gemItemId;
#else
			RegisterItemDrop(_gemItemId);
#endif

			//LocalizedText originalText = Lang._mapLegendCache[MapHelper.TileToLookup(_gemTileId, 0)];
			ModTile originalTile = TileLoader.GetTile(_gemTileId);
			MapTile mapTile = MapTile.Create((ushort)_gemTileId, byte.MaxValue, 0);
			Color color = MapHelper.GetMapTileXnaColor(ref mapTile);

#if TML_2022_09
			AddMapEntry(color, LocalizationLoader.GetOrCreateTranslation(originalTile.Mod, "MapObject." + originalTile.Name));
#else
			AddMapEntry(color, Language.GetOrRegister(originalTile.Mod.GetLocalizationKey("MapObject." + originalTile.Name)));
#endif

			// Tile merging is handled in TheDepthsAPISystem.
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
}