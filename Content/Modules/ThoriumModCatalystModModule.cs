using Microsoft.Xna.Framework;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;
using ThoriumMod.Items.Misc;

namespace RandomModCompat.Content.Modules;

internal sealed class ThoriumModCatalystModModule : CrossModModule<CatalystModAPI>
{
	public override string CrossModName => ModNames.ThoriumMod;

	protected internal override void OnModLoad()
	{
		API.AddWulfrumFlashlightLens(ModContent.ItemType<Aquamarine>(), new Color(109, 255, 216));
		API.AddWulfrumFlashlightLens(ModContent.ItemType<Opal>(), new Color(254, 176, 184));
	}
}