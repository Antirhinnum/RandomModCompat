using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using ThoriumMod.NPCs.Bat;
using ThoriumMod.NPCs.Beholder;
using ThoriumMod.NPCs.Blizzard;
using ThoriumMod.NPCs.Buried;
using ThoriumMod.NPCs.Depths;
using ThoriumMod.NPCs.Granite;
using ThoriumMod.NPCs.Lich;
using ThoriumMod.NPCs.Primordials;
using ThoriumMod.NPCs.QueenJelly;
using ThoriumMod.NPCs.Scouter;
using ThoriumMod.NPCs.Thunder;

namespace RandomModCompat.Content.Modules;

internal sealed class ThoriumModROR2HealthBarsModule : CrossModModule<ROR2HealthBarsAPI>
{
	private const string _localizationHeader = "Mods.RandomModCompat.Thorium.";

	public override string CrossModName => ModNames.ThoriumMod;

	protected internal override void PostSetupContent()
	{
		API.AddDesc<TheGrandThunderBirdv2>(_localizationHeader, "GrandThunderBird");
		API.AddDesc<QueenJelly>(_localizationHeader, "QueenJelly");
		API.AddDesc<Viscount>(_localizationHeader, "Viscount");
		API.AddDesc<GraniteEnergyStorm>(_localizationHeader, "GraniteEnergyStorm");
		API.AddDesc<TheBuriedWarrior>(_localizationHeader, "BuriedChampion");
		API.AddDesc<ThePrimeScouter>(_localizationHeader, "StarScouter");
		API.AddDesc<BoreanStrider>(_localizationHeader, "BoreanStrider");
		API.AddDesc<BoreanStriderPopped>(_localizationHeader, "BoreanStrider");
		API.AddDesc<FallenDeathBeholder>(_localizationHeader, "FallenBeholder");
		API.AddDesc<FallenDeathBeholder2>(_localizationHeader, "FallenBeholder");
		API.AddDesc<Lich>(_localizationHeader, "Lich");
		API.AddDesc<LichHeadless>(_localizationHeader, "Lich");
		API.AddDesc<Abyssion>(_localizationHeader, "ForgottenOne");
		API.AddDesc<AbyssionCracked>(_localizationHeader, "ForgottenOne");
		API.AddDesc<AbyssionReleased>(_localizationHeader, "ForgottenOne");
		API.AddDesc<Aquaius>(_localizationHeader, "Aquais");
		API.AddDesc<Omnicide>(_localizationHeader, "Omnicide");
		API.AddDesc<SlagFury>(_localizationHeader, "SlagFury");
		API.AddDesc<RealityBreaker>(_localizationHeader, "DreamEater");
	}
}