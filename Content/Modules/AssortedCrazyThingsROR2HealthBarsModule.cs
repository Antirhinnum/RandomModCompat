using AssortedCrazyThings;
using AssortedCrazyThings.NPCs.Harvester;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class AssortedCrazyThingsROR2HealthBarsModule : CrossModModule<ROR2HealthBarsAPI>
{
	private const string _localizationHeader = $"Mods.RandomModCompat.{ModNames.AssortedCrazyThings}.";

	public override string CrossModName => ModNames.AssortedCrazyThings;

	protected internal override void PostSetupContent()
	{
		if (ModContent.GetInstance<ContentConfig>().Bosses)
		{
			API.AddDesc<HarvesterBoss>(_localizationHeader, "HarvesterBoss");
		}
	}
}