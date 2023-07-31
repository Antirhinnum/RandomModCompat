using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.Misc;

namespace RandomModCompat.Content.Modules;

internal sealed class ThoriumModTheDepthsModule : CrossModModule<TheDepthsAPI>
{
	public override string CrossModName => ModNames.ThoriumMod;

	protected internal override void OnModLoad()
	{
		API.AddShaleGem(ModContent.ItemType<Aquamarine>(), ModContent.TileType<ThoriumMod.Tiles.Aquamarine>(), TileID.Stone, 0.45f);
		API.AddShaleGem(ModContent.ItemType<Opal>(), ModContent.TileType<ThoriumMod.Tiles.Opal>(), TileID.Stone, 0.4f);
	}
}