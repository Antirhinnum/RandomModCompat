using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using StormDiversMod.NPCs.Boss;

namespace RandomModCompat.Content.Modules;

internal sealed class StormDiversModROR2HealthBarsModule : CrossModModule<ROR2HealthBarsAPI>
{
	private const string _localizationHeader = $"Mods.RandomModCompat.{ModNames.StormDiversMod}.";

	public override string CrossModName => ModNames.StormDiversMod;

	protected internal override void PostSetupContent()
	{
		API.AddDesc<AridBoss>(_localizationHeader, "AridBoss");
		API.AddDesc<StormBoss>(_localizationHeader, "StormBoss");
		API.AddDesc<ThePainBoss>(_localizationHeader, "ThePainBoss");
	}
}