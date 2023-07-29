using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;
using TheDepths.Items.Placeable;

namespace RandomModCompat.Content.Modules;

internal sealed class TheDepthsLiberModule : CrossModModule<LiberAPI>
{
	public override string CrossModName => ModNames.TheDepths;

	protected internal override void OnModLoad()
	{
		// Internal type :(
		API.AddLargeGemPedestal(ModContent.ItemType<Onyx>(), CrossMod.Find<ModItem>("LargeOnyx").Type);
	}
}