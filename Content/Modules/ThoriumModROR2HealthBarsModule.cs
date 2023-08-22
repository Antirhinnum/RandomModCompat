using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

#if TML_2022_09
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
#else

using ThoriumMod.NPCs.BossBoreanStrider;
using ThoriumMod.NPCs.BossBuriedChampion;
using ThoriumMod.NPCs.BossFallenBeholder;
using ThoriumMod.NPCs.BossForgottenOne;
using ThoriumMod.NPCs.BossGraniteEnergyStorm;
using ThoriumMod.NPCs.BossLich;
using ThoriumMod.NPCs.BossQueenJellyfish;
using ThoriumMod.NPCs.BossStarScouter;
using ThoriumMod.NPCs.BossTheGrandThunderBird;
using ThoriumMod.NPCs.BossThePrimordials;
using ThoriumMod.NPCs.BossViscount;

#endif

namespace RandomModCompat.Content.Modules;

internal sealed class ThoriumModROR2HealthBarsModule : CrossModModule<ROR2HealthBarsAPI>
{
	private const string _localizationHeader = "Mods.RandomModCompat.Thorium.";

	public override string CrossModName => ModNames.ThoriumMod;

	protected internal override void PostSetupContent()
	{
		API.AddDesc<Viscount>(_localizationHeader, "Viscount");
		API.AddDesc<GraniteEnergyStorm>(_localizationHeader, "GraniteEnergyStorm");
		API.AddDesc<BoreanStrider>(_localizationHeader, "BoreanStrider");
		API.AddDesc<BoreanStriderPopped>(_localizationHeader, "BoreanStrider");
		API.AddDesc<Lich>(_localizationHeader, "Lich");
		API.AddDesc<LichHeadless>(_localizationHeader, "Lich");
		API.AddDesc<Aquaius>(_localizationHeader, "Aquais");
		API.AddDesc<Omnicide>(_localizationHeader, "Omnicide");
		API.AddDesc<SlagFury>(_localizationHeader, "SlagFury");

#if TML_2022_09
		API.AddDesc<TheGrandThunderBirdv2>(_localizationHeader, "GrandThunderBird");
		API.AddDesc<QueenJelly>(_localizationHeader, "QueenJelly");
		API.AddDesc<TheBuriedWarrior>(_localizationHeader, "BuriedChampion");
		API.AddDesc<ThePrimeScouter>(_localizationHeader, "StarScouter");
		API.AddDesc<FallenDeathBeholder>(_localizationHeader, "FallenBeholder");
		API.AddDesc<FallenDeathBeholder2>(_localizationHeader, "FallenBeholder");
		API.AddDesc<Abyssion>(_localizationHeader, "ForgottenOne");
		API.AddDesc<AbyssionCracked>(_localizationHeader, "ForgottenOne");
		API.AddDesc<AbyssionReleased>(_localizationHeader, "ForgottenOne");
		API.AddDesc<RealityBreaker>(_localizationHeader, "DreamEater");
#else
		API.AddDesc<TheGrandThunderBird>(_localizationHeader, "GrandThunderBird");
		API.AddDesc<QueenJellyfish>(_localizationHeader, "QueenJelly");
		API.AddDesc<BuriedChampion>(_localizationHeader, "BuriedChampion");
		API.AddDesc<StarScouter>(_localizationHeader, "StarScouter");
		API.AddDesc<FallenBeholder>(_localizationHeader, "FallenBeholder");
		API.AddDesc<FallenBeholder2>(_localizationHeader, "FallenBeholder");
		API.AddDesc<ForgottenOne>(_localizationHeader, "ForgottenOne");
		API.AddDesc<ForgottenOneCracked>(_localizationHeader, "ForgottenOne");
		API.AddDesc<ForgottenOneReleased>(_localizationHeader, "ForgottenOne");
		API.AddDesc<DreamEater>(_localizationHeader, "DreamEater");
#endif
	}
}