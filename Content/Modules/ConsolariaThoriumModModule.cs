using Consolaria.Content.NPCs;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class ConsolariaThoriumModModule : CrossModModule<ThoriumModAPI>
{
	public override string CrossModName => ModNames.Consolaria;

	protected internal override void PostSetupContent()
	{
		API.AddRepelledEnemy<AlbinoAntlion>(ThoriumModAPI.ThoriumRepellentType.Bug);
		API.AddRepelledEnemy<AlbinoCharger>(ThoriumModAPI.ThoriumRepellentType.Bug);
		API.AddRepelledEnemy<DragonHornet>(ThoriumModAPI.ThoriumRepellentType.Bug);
		API.AddRepelledEnemy<GiantAlbinoCharger>(ThoriumModAPI.ThoriumRepellentType.Bug);

		API.AddUndeadEnemy<VampireMiner>();
	}
}