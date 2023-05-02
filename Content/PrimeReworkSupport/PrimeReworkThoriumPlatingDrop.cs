using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Core.Handlers.SharedBossLootHandler;

namespace RandomModCompat.Content.PrimeReworkSupport;

// Adds Strange Plating and Life-Powered Energy Cells to The Terminator and Caretaker's drops.
[JITWhenModsEnabled("PrimeRework", "ThoriumMod")]
internal sealed class PrimeReworkThoriumPlatingDrop : ModSystem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return ModLoader.HasMod("PrimeRework") && ModLoader.HasMod("ThoriumMod");
	}

	public override void SetStaticDefaults()
	{
		SharedBossLootSystem.ByType[ModContent.NPCType<PrimeRework.NPCs.TheTerminator>()] = SharedBossLootSystem.ByType[NPCID.TheDestroyer];
		SharedBossLootSystem.ByType[ModContent.NPCType<PrimeRework.NPCs.Caretaker>()] = SharedBossLootSystem.ByType[NPCID.TheDestroyer];
	}
}