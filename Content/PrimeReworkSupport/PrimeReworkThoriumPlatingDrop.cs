﻿using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Core.Handlers.SharedBossLootHandler;

namespace RandomModCompat.Content.PrimeReworkSupport;

// Adds Strange Plating and Life-Powered Energy Cells to The Terminator and Caretaker's drops.
[JITWhenModsEnabled(BaseMod, SupportMod)]
internal sealed class PrimeReworkThoriumPlatingDrop : ModSystem
{
	private const string BaseMod = ModNames.PrimeRework;
	private const string SupportMod = ModNames.ThoriumMod;

	public override bool IsLoadingEnabled(Mod mod)
	{
		return RandomModCompat.SupportEnabled(BaseMod, SupportMod);
	}

	public override void SetStaticDefaults()
	{
		SharedBossLootSystem.ByType[ModContent.NPCType<PrimeRework.NPCs.TheTerminator>()] = SharedBossLootSystem.ByType[NPCID.TheDestroyer];
		SharedBossLootSystem.ByType[ModContent.NPCType<PrimeRework.NPCs.Caretaker>()] = SharedBossLootSystem.ByType[NPCID.TheDestroyer];
	}
}