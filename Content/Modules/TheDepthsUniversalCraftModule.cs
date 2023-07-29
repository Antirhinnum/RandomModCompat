using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria;
using TheDepths.Tiles;

namespace RandomModCompat.Content.Modules;

internal sealed class TheDepthsUniversalCraftModule : CrossModModule<UniversalCraftAPI>
{
	public override string CrossModName => ModNames.TheDepths;

	protected internal override void PostSetupContent()
	{
		API.AddStation<CoreBuilderTile>(() => NPC.downedBoss2);
		API.AddStation<Gemforge>(() => NPC.downedBoss2);
	}
}