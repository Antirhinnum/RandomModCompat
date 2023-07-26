using AssortedCrazyThings;
using AssortedCrazyThings.NPCs.Harvester;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class AssortedCrazyThingsMagicStorageModule : CrossModModule<MagicStorageAPI>
{
	public override string CrossModName => ModNames.AssortedCrazyThings;

	protected internal override void PostSetupContent()
	{
		if (ModContent.GetInstance<ContentConfig>().Bosses)
		{
			API.RegisterShadowDiamondDrop<HarvesterBoss>(1);
		}
	}
}