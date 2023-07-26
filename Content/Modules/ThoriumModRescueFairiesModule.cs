using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria;
using ThoriumMod.NPCs;
using ThoriumMod.NPCs.Buried;
using ThoriumMod.NPCs.Granite;

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
		API.AddTrackingCondition<BuriedSpawn>();
		API.AddTrackingCondition<GraniteSpawn>();
	}
}