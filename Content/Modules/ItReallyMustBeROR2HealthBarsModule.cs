using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ID;

namespace RandomModCompat.Content.Modules;

internal sealed class ItReallyMustBeROR2HealthBarsModule : CrossModModule<ROR2HealthBarsAPI>
{
	public override string CrossModName => ModNames.ItReallyMustBe;

	protected internal override void PostSetupContent()
	{
		API.BossDesc(NPCID.BloodNautilus, "Mods.RandomModCompat.ItReallyMustBe.ROR2.Dreadnautilus.Description");
	}
}