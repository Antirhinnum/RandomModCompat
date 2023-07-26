using PrimeRework.NPCs;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class PrimeReworkROR2HealthBarsModule : CrossModModule<ROR2HealthBarsAPI>
{
	private const string _localizationHeader = "Mods.RandomModCompat.PrimeRework.";

	public override string CrossModName => ModNames.PrimeRework;

	protected internal override void PostSetupContent()
	{
		API.AddDesc<TheTerminator>(_localizationHeader, "Terminator");
		API.AddDesc<Caretaker>(_localizationHeader, "Caretaker");
	}
}