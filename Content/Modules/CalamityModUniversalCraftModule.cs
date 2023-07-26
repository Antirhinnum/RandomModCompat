using CalamityMod;
using CalamityMod.Tiles.Furniture.CraftingStations;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria;

namespace RandomModCompat.Content.Modules;

internal sealed class CalamityModUniversalCraftModule : CrossModModule<UniversalCraftAPI>
{
	public override string CrossModName => ModNames.CalamityMod;

	protected internal override void PostSetupContent()
	{
		static bool PostDesertScourge() => DownedBossSystem.downedDesertScourge;
		static bool PostSlimeGod() => DownedBossSystem.downedSlimeGod;
		static bool PostDragonfolly() => DownedBossSystem.downedDragonfolly;
		static bool PostProvidence() => DownedBossSystem.downedProvidence;
		static bool PostDoG() => DownedBossSystem.downedDoG;
		static bool PostYharon() => DownedBossSystem.downedYharon;
		static bool DraedonsForgeCondition()
		{
			return DownedBossSystem.downedDoG // Cosmic Anvil, Phantoplasm
				&& DownedBossSystem.downedYharon // Auric bars
				&& DownedBossSystem.downedExoMechs; // Exo Prism
		}

		API.AddStation<WulfrumLabstation>();
		API.AddStation<EutrophicShelf>(PostDesertScourge);
		API.AddStation<StaticRefiner>(PostSlimeGod);
		API.AddStation<AncientAltar>(() => Main.hardMode);
		API.AddStation<AshenAltar>(() => Main.hardMode);
		API.AddStation<MonolithAmalgam>(() => Main.hardMode);
		API.AddStation<PlagueInfuser>(() => NPC.downedGolemBoss);
		API.AddStation<VoidCondenser>(() => Main.hardMode);
		API.AddStation<ProfanedCrucible>(() => NPC.downedMoonlord);
		API.AddStation<BotanicPlanter>(PostProvidence);
		API.AddStation<CosmicAnvil>(PostDoG);
		API.AddStation<SilvaBasin>(PostDragonfolly);
		API.AddStation<DraedonsForge>(DraedonsForgeCondition);
		API.AddStation<SCalAltar>(PostYharon);
	}
}