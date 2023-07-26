using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.Placeable;

namespace RandomModCompat.Content.Modules;

internal sealed class ThoriumModOverpoweredGoldDustModule : CrossModModule<OverpoweredGoldDustAPI>
{
	public override string CrossModName => ModNames.ThoriumMod;

	protected internal override void PostSetupContent()
	{
		// Ducks
		API.RegisterItem(ItemID.Duck, ModContent.ItemType<GoldDuck>());
		API.RegisterItem(ItemID.MallardDuck, ModContent.ItemType<GoldDuck>());
		API.RegisterItem(ItemID.DuckCage, ModContent.ItemType<GoldDuckCage>());
		API.RegisterItem(ItemID.MallardDuckCage, ModContent.ItemType<GoldDuckCage>());

		API.RegisterNPC(NPCID.Duck, ModContent.NPCType<ThoriumMod.NPCs.GoldDuck>());
		API.RegisterNPC(NPCID.DuckWhite, ModContent.NPCType<ThoriumMod.NPCs.GoldDuck>());
		API.RegisterNPC(NPCID.Duck2, ModContent.NPCType<ThoriumMod.NPCs.GoldDuckFlying>());
		API.RegisterNPC(NPCID.DuckWhite2, ModContent.NPCType<ThoriumMod.NPCs.GoldDuckFlying>());

		// Dumbo Octopi
		API.RegisterItem<DumboOctopus, GoldDumboOctopus>();
		API.RegisterItem<PurpleDumboOctopus, GoldDumboOctopus>();
		API.RegisterItem<DumboOctopusCage, GoldDumboOctopusCage>();
		API.RegisterItem<PurpleDumboOctopusCage, GoldDumboOctopusCage>();

		API.RegisterNPC<ThoriumMod.NPCs.Depths.DumboOctopus, ThoriumMod.NPCs.Depths.GoldDumboOctopus>();
		API.RegisterNPC<ThoriumMod.NPCs.Depths.PurpleDumboOctopus, ThoriumMod.NPCs.Depths.GoldDumboOctopus>();

		// Lobsters
		API.RegisterItem<Lobster, GoldLobster>();
		API.RegisterItem<BlueLobster, GoldLobster>();
		API.RegisterItem<LobsterCage, GoldLobsterCage>();
		API.RegisterItem<BlueLobsterCage, GoldLobsterCage>();

		API.RegisterNPC<ThoriumMod.NPCs.Depths.Lobster, ThoriumMod.NPCs.Depths.GoldLobster>();
		API.RegisterNPC<ThoriumMod.NPCs.Depths.BlueLobster, ThoriumMod.NPCs.Depths.GoldLobster>();

		// Slimes
		API.RegisterNPC(ModContent.NPCType<ThoriumMod.NPCs.Clot>(), NPCID.GoldenSlime);
		API.RegisterNPC(ModContent.NPCType<ThoriumMod.NPCs.GildedSlime>(), NPCID.GoldenSlime);
		API.RegisterNPC(ModContent.NPCType<ThoriumMod.NPCs.GildedSlimeMini>(), NPCID.GoldenSlime);
		API.RegisterNPC(ModContent.NPCType<ThoriumMod.NPCs.GraniteFusedSlime>(), NPCID.GoldenSlime);
		API.RegisterNPC(ModContent.NPCType<ThoriumMod.NPCs.LivingHemorrhage>(), NPCID.GoldenSlime);
		API.RegisterNPC(ModContent.NPCType<ThoriumMod.NPCs.SpaceSlime>(), NPCID.GoldenSlime);
		API.RegisterNPC(ModContent.NPCType<ThoriumMod.NPCs.BloodMoon.BloodDrop>(), NPCID.GoldenSlime);

		// Misc. NPCs
		API.RegisterNPC<ThoriumMod.NPCs.CoinBagCopper, ThoriumMod.NPCs.CoinBagGold>();
		API.RegisterNPC<ThoriumMod.NPCs.CoinBagSilver, ThoriumMod.NPCs.CoinBagGold>();
		API.RegisterNPC(ModContent.NPCType<ThoriumMod.NPCs.Myna>(), NPCID.GoldBird);
		API.RegisterNPC(NPCID.GiantFlyingFox, ModContent.NPCType<ThoriumMod.NPCs.GildedBat>());
		API.RegisterNPC(NPCID.Werewolf, ModContent.NPCType<ThoriumMod.NPCs.GildedLycan>());

		// Misc. Items
		API.RegisterItem<CopperBuckler, GoldAegis,
			PlatinumAegis>();
		API.RegisterItem<TinBuckler, GoldAegis, PlatinumAegis>();
		API.RegisterItem<IronShield, GoldAegis, PlatinumAegis>();
		API.RegisterItem<LeadShield, GoldAegis, PlatinumAegis>();
		API.RegisterItem<SilverBulwark, GoldAegis, PlatinumAegis>();
		API.RegisterItem<TungstenBulwark, GoldAegis, PlatinumAegis>();

		API.RegisterItem(ItemID.PlumbersHat, ModContent.ItemType<GreedyHat>());
		API.RegisterItem(ItemID.PlumbersShirt, ModContent.ItemType<GreedyShirt>());
		API.RegisterItem(ItemID.PlumbersPants, ModContent.ItemType<GreedyPants>());
	}
}