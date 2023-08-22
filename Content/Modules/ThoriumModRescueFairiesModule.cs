using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria;
using ThoriumMod.NPCs;

namespace RandomModCompat.Content.Modules;

internal sealed class ThoriumModRescueFairiesModule : CrossModModule<RescueFairiesAPI>
{
	public override string CrossModName => ModNames.ThoriumMod;

	protected internal override void PostSetupContent()
	{
		static bool CoinBagCondition(NPC npc) => npc.ModNPC is CoinBagCopper; // All three coin bags inherit from copper.

		// The normal mimics and biome mimics are already handled via Rescue Fairies' default settings.
		// Pot mimics are not added.
		API.AddTrackingCondition(CoinBagCondition);
		API.AddTrackingCondition<LifeCrystalMimic>();

#if TML_2022_09
		API.AddTrackingCondition<ThoriumMod.NPCs.Buried.BuriedSpawn>();
		API.AddTrackingCondition<ThoriumMod.NPCs.Granite.GraniteSpawn>();
#else
		API.AddTrackingCondition<ThoriumMod.NPCs.BossBuriedChampion.BizarreRockFormation>();
		API.AddTrackingCondition<ThoriumMod.NPCs.BossGraniteEnergyStorm.UnstableEnergyAnomaly>();
#endif
	}
}