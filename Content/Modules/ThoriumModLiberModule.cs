using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;
using ThoriumMod.Items.Misc;

namespace RandomModCompat.Content.Modules;

internal sealed class ThoriumModLiberModule : CrossModModule<LiberAPI>
{
	public override string CrossModName => ModNames.ThoriumMod;

	protected internal override void OnModLoad()
	{
		API.AddLargeGemPedestal(ModContent.ItemType<Aquamarine>(), ModContent.ItemType<LargeAquamarine>());
		API.AddLargeGemPedestal(ModContent.ItemType<Opal>(), ModContent.ItemType<LargeOpal>());
		// TODO: Prismite? Needs a small version.
	}
}