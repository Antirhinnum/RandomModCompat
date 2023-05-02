using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RandomModCompat.Common.Callers;
using RandomModCompat.Common.ExplicitSupport;
using RandomModCompat.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace RandomModCompat.Content.ThoriumModSupport;

[JITWhenModsEnabled(_modName)]
internal sealed class ThoriumModSystem : CrossModHandler
{
	private const string _thoriumLocalization = "Mods.RandomModCompat.Thorium.";

	private const string _modName = "ThoriumMod";
	protected override string ModName => _modName;

	/*
	 * Thorium Mod adds support for the following mods:
	 * - Bangarang
	 * - Boss Checklist
	 * - Calamity (minibosses)
	 * - Census
	 * - Colored Damage Types
	 * - Fargo's Mutant Mod
	 * - HERO's Mod
	 * - Recipe Browser
	 * - Summoner's Association
	 * - Yet Another Boss Health Bar
	 * - Wikithis
	 *
	 * Additionally, the following mods either have intrinsic Thorium support or an existing compatibility mod:
	 * - Better Taxes
	 * - Bosses as NPCs
	 * - W1K's Weapon Scaling
	 *
	 * This file adds support for:
	 * - Asymmetric Equips (partial)
	 * - Enhanced Buff Display
	 * - Dialogue Panel Rework
	 * - Gold Dust Turns Everything Into Gold (partial)
	 * - Item Check Blacklist Lib
	 * - Magic Storage
	 * - Rescue Fairies (TODO: check for exceptions)
	 * - RoR 2 Health Bars
	 * - Universal Crafter
	 */

	internal override void OnModLoad()
	{
		AsymmetricEquipsLoadTextures();
	}

	private void AsymmetricEquipsLoadTextures()
	{
		if (!ModWithCalls.TryGetCaller(out AsymmetricEquipsCaller caller))
		{
			return;
		}

		caller.AddFlippedEquipTexture(_modName, nameof(ThoriumMod.Items.HealerItems.SupportSash), EquipType.Waist);
	}

	internal override void PostSetupContent()
	{
		AsymmetricEquipsSupport();
		BuffDisplaySupport();
		DialogueTweakSupport();
		ItemCheckBlacklistSupport();
		MagicStorageSupport();
		OverpoweredGoldDustSupport();
		RescueFairiesSupport();
		ROR2HealthBarsSupport();
		UniversalCraftSupport();
	}

	private static void AsymmetricEquipsSupport()
	{
		if (!ModWithCalls.TryGetCaller(out AsymmetricEquipsCaller caller))
		{
			return;
		}

		// Basic hidden equips
		caller.AddHiddenEquip<ThoriumMod.Items.BasicAccessories.AmberRing>(EquipType.HandsOn);
		caller.AddHiddenEquip<ThoriumMod.Items.BasicAccessories.AmethystRing>(EquipType.HandsOn);
		caller.AddHiddenEquip<ThoriumMod.Items.BasicAccessories.AquamarineRing>(EquipType.HandsOn);
		caller.AddHiddenEquip<ThoriumMod.Items.BasicAccessories.DiamondRing>(EquipType.HandsOn);
		caller.AddHiddenEquip<ThoriumMod.Items.BasicAccessories.EmeraldRing>(EquipType.HandsOn);
		caller.AddHiddenEquip<ThoriumMod.Items.BasicAccessories.OpalRing>(EquipType.HandsOn);
		caller.AddHiddenEquip<ThoriumMod.Items.BasicAccessories.RubyRing>(EquipType.HandsOn);
		caller.AddHiddenEquip<ThoriumMod.Items.BasicAccessories.SapphireRing>(EquipType.HandsOn);
		caller.AddHiddenEquip<ThoriumMod.Items.BasicAccessories.TopazRing>(EquipType.HandsOn);
		caller.AddHiddenEquip<ThoriumMod.Items.BasicAccessories.TheRing>(EquipType.HandsOn);
		caller.AddHiddenEquip<ThoriumMod.Items.Depths.VampireGland>(EquipType.Waist);
		caller.AddHiddenEquip<ThoriumMod.Items.Donate.HungeringBlossom>(EquipType.Face);
		caller.AddHiddenEquip<ThoriumMod.Items.Donate.MetabolicPills>(EquipType.Waist);
		caller.AddHiddenEquip<ThoriumMod.Items.Donate.PlagueLordFlask>(EquipType.Waist);
		caller.AddHiddenEquip<ThoriumMod.Items.EarlyMagic.ManaBauble>(EquipType.Waist);
		caller.AddHiddenEquip<ThoriumMod.Items.HealerItems.ApothecaryLife>(EquipType.Waist);
		caller.AddHiddenEquip<ThoriumMod.Items.HealerItems.ApothecaryMana>(EquipType.Waist);
		caller.AddHiddenEquip<ThoriumMod.Items.HealerItems.CrystalArcanum>(EquipType.Waist);
		caller.AddHiddenEquip<ThoriumMod.Items.HealerItems.CrystalHoney>(EquipType.Waist);
		caller.AddHiddenEquip<ThoriumMod.Items.HealerItems.DarkGlaze>(EquipType.Waist);
		caller.AddHiddenEquip<ThoriumMod.Items.HealerItems.LifeQuartzGem>(EquipType.Waist);
		caller.AddHiddenEquip<ThoriumMod.Items.Lich.TheLostCross>(EquipType.Waist);
		caller.AddHiddenEquip<ThoriumMod.Items.Misc.RingofUnity>(EquipType.HandsOn);
		caller.AddHiddenEquip<ThoriumMod.Items.NPCItems.NightShadePetal>(EquipType.Face);
		caller.AddHiddenEquip<ThoriumMod.Items.NPCItems.PotionChaser>(EquipType.Waist);
		caller.AddHiddenEquip<ThoriumMod.Items.SummonItems.NecroticSkull>(EquipType.Waist);
		caller.AddHiddenEquip<ThoriumMod.Items.SummonItems.SoulStone>(EquipType.Waist);
		caller.AddHiddenEquip<ThoriumMod.Items.ThrownItems.Canteen>(EquipType.Waist);
		caller.AddHiddenEquip<ThoriumMod.Items.ThrownItems.DeadEyePatch>(EquipType.Face);
		caller.AddHiddenEquip<ThoriumMod.Items.ThrownItems.MermaidCanteen>(EquipType.Waist);

		// Hairpin, needs hair override.
		caller.AddSmallHead<ThoriumMod.Items.Vanity.NoteHairpin>();

		// Balloons
		caller.AddBalloon<ThoriumMod.Items.BardItems.AutoTuner>();
		caller.AddBalloon<ThoriumMod.Items.BardItems.DevilsSubwoofer>();
		caller.AddBalloon<ThoriumMod.Items.BardItems.Subwoofer>();
		caller.AddBalloon<ThoriumMod.Items.BardItems.TerrariumSurroundSound>();
		caller.AddBalloon<ThoriumMod.Items.Donate.HeartOfTheJungle>();
		caller.AddBalloon<ThoriumMod.Items.Donate.IncandescentSpark>();
		caller.AddBalloon<ThoriumMod.Items.Donate.UpDownBalloon>();
		caller.AddBalloon<ThoriumMod.Items.EnergyStorm.EyeoftheStorm>();
		caller.AddBalloon<ThoriumMod.Items.FallenBeholder.MirroroftheBeholder>();
		caller.AddBalloon<ThoriumMod.Items.HealerItems.DarkHeart>();
		caller.AddBalloon<ThoriumMod.Items.HealerItems.SpiritFlame>();
		caller.AddBalloon<ThoriumMod.Items.SummonItems.Phylactery>();
		caller.AddBalloon<ThoriumMod.Items.ThrownItems.OlympicTorch>();

		// Sheaths
		caller.AddSpecialItem(ModContent.ItemType<ThoriumMod.Items.Donate.GardenersSheath>(), AsymmetricEquipsCaller.PlayerSide.Left);
		caller.AddSpecialItem(ModContent.ItemType<ThoriumMod.Items.MeleeItems.LeatherSheath>(), AsymmetricEquipsCaller.PlayerSide.Left);
		caller.AddSpecialItem(ModContent.ItemType<ThoriumMod.Items.MeleeItems.LeechingSheath>(), AsymmetricEquipsCaller.PlayerSide.Left);
		caller.AddSpecialItem(ModContent.ItemType<ThoriumMod.Items.MeleeItems.TitanSlayerSheath>(), AsymmetricEquipsCaller.PlayerSide.Left);
		caller.AddSpecialItem(ModContent.ItemType<ThoriumMod.Items.MeleeItems.WrithingSheath>(), AsymmetricEquipsCaller.PlayerSide.Left);

		// Custom textures
		caller.AddFlippedEquip<ThoriumMod.Items.HealerItems.SupportSash>(EquipType.Waist);

		// Sprites will be needed for:
		// - Some HandsOn equips
		// - Some pouch/bag equips
	}

	private static void BuffDisplaySupport()
	{
		if (!ModWithCalls.TryGetCaller(out BuffDisplayCaller caller))
		{
			return;
		}

		int grimShadowBuff = ModContent.BuffType<ThoriumMod.Buffs.Grimshadow>();
		caller.SetCountAs(ModContent.BuffType<ThoriumMod.Buffs.Grimshadow2>(), grimShadowBuff);
		caller.SetCountAs(ModContent.BuffType<ThoriumMod.Buffs.Grimshadow3>(), grimShadowBuff);
	}

	private static void DialogueTweakSupport()
	{
		if (!ModWithCalls.TryGetCaller<DialogueTweakCaller>(out var caller))
		{
			return;
		}

		const string BestiaryIconsPath = "Terraria/Images/UI/Bestiary/Icon_Tags_Shadow";
		static Func<Rectangle> BestiaryIconFrame(int index) => () => ModContent.Request<Texture2D>(BestiaryIconsPath).Frame(16, 5, index % 16, index / 16);

		caller.ReplaceExtraButtonIcon(new List<int> { ModContent.NPCType<ThoriumMod.NPCs.Cobbler>() }, "Terraria/Images/Item_" + ItemID.ArmorPolish);
		caller.ReplaceExtraButtonIcon(new List<int> { ModContent.NPCType<ThoriumMod.NPCs.ConfusedZombie>() }, ModContent.GetInstance<ThoriumMod.Items.Consumable.ZombieRepellent>().Texture);
		caller.ReplaceExtraButtonIcon(new List<int> { ModContent.NPCType<ThoriumMod.NPCs.Cook>() }, "Terraria/Images/Item_" + ItemID.ChefHat);
		caller.ReplaceExtraButtonIcon(new List<int> { ModContent.NPCType<ThoriumMod.NPCs.DesertAcolyte>() }, BestiaryIconsPath, null, BestiaryIconFrame(43));
		caller.ReplaceExtraButtonIcon(new List<int> { ModContent.NPCType<ThoriumMod.NPCs.Diverman>() }, "Terraria/Images/Bubble");
		caller.ReplaceExtraButtonIcon(new List<int> { ModContent.NPCType<ThoriumMod.NPCs.Spiritualist>() }, ModContent.GetInstance<ThoriumMod.Items.HealerItems.PurityShards>().Texture);
		caller.ReplaceShopButtonIcon(new List<int> { ModContent.NPCType<ThoriumMod.NPCs.Tracker>() }, ModContent.GetInstance<ThoriumMod.Items.Tracker.VanquisherMedal>().Texture);
		caller.ReplaceExtraButtonIcon(new List<int> { ModContent.NPCType<ThoriumMod.NPCs.WeaponMaster>() }, ModContent.GetInstance<ThoriumMod.Items.NPCItems.ExileHelmet>().Texture);
	}

	private static void ItemCheckBlacklistSupport()
	{
		if (!ModWithCalls.TryGetCaller(out ItemCheckBlacklistCaller caller))
		{
			return;
		}
		// List from wiki: https://thoriummod.wiki.gg/wiki/List_of_items#Unobtainable_items

		caller.Add(
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.AngryStatue>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.WeirdMud>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.ArcaneSpike>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.ArtificersExtractor>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.BasicPickaxe>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.DyingRealityWhisper>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.DreamPotion>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.TesterEmpowerment>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.GodMode>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.TesterGore>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.HealingDummyStatue>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.LodestoneBuckshot>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.PenguinWand>(),
			ModContent.ItemType<ThoriumMod.Items.Misc.PixelDye>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.TesterProjectile>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.TesterPurity>(),
			ModContent.ItemType<ThoriumMod.Items.Tracker.ShameMedal>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.TesterStats>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.SuppressionBullet>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.TesterText>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.TheBareGauntlet>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.TheGauntlet>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.PenguinWandTrue>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.TesterTile>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.StoneBlue>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.StoneGreen>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.StoneOrange>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.StonePurple>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.StoneRed>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.StoneYellow>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.ViscountRequirement>(),

			// ... Plus some others.
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.LichRequirement>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.LichRequirement2>(),
			ModContent.ItemType<ThoriumMod.Items.ZRemoved.LichRequirement3>()
			);
	}

	private static void MagicStorageSupport()
	{
		if (!ModWithCalls.TryGetCaller(out MagicStorageCaller caller))
		{
			return;
		}

		caller.RegisterShadowDiamondDrop<ThoriumMod.NPCs.Thunder.TheGrandThunderBirdv2>(1);
		caller.RegisterShadowDiamondDrop<ThoriumMod.NPCs.QueenJelly.QueenJelly>(1);
		caller.RegisterShadowDiamondDrop<ThoriumMod.NPCs.Bat.Viscount>(1);
		caller.RegisterShadowDiamondDrop<ThoriumMod.NPCs.Granite.GraniteEnergyStorm>(1);
		caller.RegisterShadowDiamondDrop<ThoriumMod.NPCs.Buried.TheBuriedWarrior>(1);
		caller.RegisterShadowDiamondDrop<ThoriumMod.NPCs.Scouter.ThePrimeScouter>(1);

		// Drop for both phases. A player could theoretically get more Diamonds because of this, but that would require them to kill the Borean Strider or Fallen Beholder before either had a chance to change phases, which is unlikely.
		caller.RegisterShadowDiamondDrop<ThoriumMod.NPCs.Blizzard.BoreanStrider>(1);
		caller.RegisterShadowDiamondDrop<ThoriumMod.NPCs.Blizzard.BoreanStriderPopped>(1);

		caller.RegisterShadowDiamondDropNormalOnly<ThoriumMod.NPCs.Beholder.FallenDeathBeholder>(1);
		caller.RegisterShadowDiamondDrop<ThoriumMod.NPCs.Beholder.FallenDeathBeholder2>(1);

		caller.RegisterShadowDiamondDropNormalOnly<ThoriumMod.NPCs.Lich.Lich>(1);
		caller.RegisterShadowDiamondDrop<ThoriumMod.NPCs.Lich.LichHeadless>(1);

		caller.RegisterShadowDiamondDropNormalOnly<ThoriumMod.NPCs.Depths.Abyssion>(1);
		caller.RegisterShadowDiamondDropNormalOnly<ThoriumMod.NPCs.Depths.AbyssionCracked>(1);
		caller.RegisterShadowDiamondDrop<ThoriumMod.NPCs.Depths.AbyssionReleased>(1);

		// The Primordials need an Expert Mode check to drop Diamonds, as well as a "last Primordial alive" check like the Twins.
		// Thankfully, Thorium handles the "last Primordial alive" check.
		IItemDropRule primordialsDropRule = caller.GetShadowDiamondDropRule(2);
		IItemDropRule lastPrimordialRule = new LeadingConditionRule(new ThoriumMod.Core.ItemDropRules.DropConditions.LastPrimordialDefeatedCondition());
		IItemDropRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
		lastPrimordialRule.OnSuccess(notExpertRule);
		notExpertRule.OnSuccess(primordialsDropRule);
		caller.SetShadowDiamondDropRule(ModContent.NPCType<ThoriumMod.NPCs.Primordials.Aquaius>(), lastPrimordialRule);
		caller.SetShadowDiamondDropRule(ModContent.NPCType<ThoriumMod.NPCs.Primordials.Omnicide>(), lastPrimordialRule);
		caller.SetShadowDiamondDropRule(ModContent.NPCType<ThoriumMod.NPCs.Primordials.SlagFury>(), lastPrimordialRule);

		caller.RegisterShadowDiamondDrop<ThoriumMod.NPCs.Primordials.RealityBreaker>(3);
	}

	private static void OverpoweredGoldDustSupport()
	{
		// Ducks
		OverpoweredGoldDustSupportSystem.RegisterItem(ItemID.Duck, ModContent.ItemType<ThoriumMod.Items.Misc.GoldDuck>());
		OverpoweredGoldDustSupportSystem.RegisterItem(ItemID.MallardDuck, ModContent.ItemType<ThoriumMod.Items.Misc.GoldDuck>());
		OverpoweredGoldDustSupportSystem.RegisterItem(ItemID.DuckCage, ModContent.ItemType<ThoriumMod.Items.Placeable.GoldDuckCage>());
		OverpoweredGoldDustSupportSystem.RegisterItem(ItemID.MallardDuckCage, ModContent.ItemType<ThoriumMod.Items.Placeable.GoldDuckCage>());

		OverpoweredGoldDustSupportSystem.RegisterNPC(NPCID.Duck, ModContent.NPCType<ThoriumMod.NPCs.GoldDuck>());
		OverpoweredGoldDustSupportSystem.RegisterNPC(NPCID.DuckWhite, ModContent.NPCType<ThoriumMod.NPCs.GoldDuck>());
		OverpoweredGoldDustSupportSystem.RegisterNPC(NPCID.Duck2, ModContent.NPCType<ThoriumMod.NPCs.GoldDuckFlying>());
		OverpoweredGoldDustSupportSystem.RegisterNPC(NPCID.DuckWhite2, ModContent.NPCType<ThoriumMod.NPCs.GoldDuckFlying>());

		// Dumbo Octopi
		OverpoweredGoldDustSupportSystem.RegisterItem<ThoriumMod.Items.Depths.DumboOctopus, ThoriumMod.Items.Depths.GoldDumboOctopus>();
		OverpoweredGoldDustSupportSystem.RegisterItem<ThoriumMod.Items.Depths.PurpleDumboOctopus, ThoriumMod.Items.Depths.GoldDumboOctopus>();
		OverpoweredGoldDustSupportSystem.RegisterItem<ThoriumMod.Items.Placeable.DumboOctopusCage, ThoriumMod.Items.Placeable.GoldDumboOctopusCage>();
		OverpoweredGoldDustSupportSystem.RegisterItem<ThoriumMod.Items.Placeable.PurpleDumboOctopusCage, ThoriumMod.Items.Placeable.GoldDumboOctopusCage>();

		OverpoweredGoldDustSupportSystem.RegisterNPC<ThoriumMod.NPCs.Depths.DumboOctopus, ThoriumMod.NPCs.Depths.GoldDumboOctopus>();
		OverpoweredGoldDustSupportSystem.RegisterNPC<ThoriumMod.NPCs.Depths.PurpleDumboOctopus, ThoriumMod.NPCs.Depths.GoldDumboOctopus>();

		// Lobsters
		OverpoweredGoldDustSupportSystem.RegisterItem<ThoriumMod.Items.Depths.Lobster, ThoriumMod.Items.Depths.GoldLobster>();
		OverpoweredGoldDustSupportSystem.RegisterItem<ThoriumMod.Items.Depths.BlueLobster, ThoriumMod.Items.Depths.GoldLobster>();
		OverpoweredGoldDustSupportSystem.RegisterItem<ThoriumMod.Items.Placeable.LobsterCage, ThoriumMod.Items.Placeable.GoldLobsterCage>();
		OverpoweredGoldDustSupportSystem.RegisterItem<ThoriumMod.Items.Placeable.BlueLobsterCage, ThoriumMod.Items.Placeable.GoldLobsterCage>();

		OverpoweredGoldDustSupportSystem.RegisterNPC<ThoriumMod.NPCs.Depths.Lobster, ThoriumMod.NPCs.Depths.GoldLobster>();
		OverpoweredGoldDustSupportSystem.RegisterNPC<ThoriumMod.NPCs.Depths.BlueLobster, ThoriumMod.NPCs.Depths.GoldLobster>();

		// Slimes
		OverpoweredGoldDustSupportSystem.RegisterNPC(ModContent.NPCType<ThoriumMod.NPCs.Clot>(), NPCID.GoldenSlime);
		OverpoweredGoldDustSupportSystem.RegisterNPC(ModContent.NPCType<ThoriumMod.NPCs.GildedSlime>(), NPCID.GoldenSlime);
		OverpoweredGoldDustSupportSystem.RegisterNPC(ModContent.NPCType<ThoriumMod.NPCs.GildedSlimeMini>(), NPCID.GoldenSlime);
		OverpoweredGoldDustSupportSystem.RegisterNPC(ModContent.NPCType<ThoriumMod.NPCs.GraniteFusedSlime>(), NPCID.GoldenSlime);
		OverpoweredGoldDustSupportSystem.RegisterNPC(ModContent.NPCType<ThoriumMod.NPCs.LivingHemorrhage>(), NPCID.GoldenSlime);
		OverpoweredGoldDustSupportSystem.RegisterNPC(ModContent.NPCType<ThoriumMod.NPCs.SpaceSlime>(), NPCID.GoldenSlime);
		OverpoweredGoldDustSupportSystem.RegisterNPC(ModContent.NPCType<ThoriumMod.NPCs.BloodMoon.BloodDrop>(), NPCID.GoldenSlime);

		// Misc. NPCs
		OverpoweredGoldDustSupportSystem.RegisterNPC<ThoriumMod.NPCs.CoinBagCopper, ThoriumMod.NPCs.CoinBagGold>();
		OverpoweredGoldDustSupportSystem.RegisterNPC<ThoriumMod.NPCs.CoinBagSilver, ThoriumMod.NPCs.CoinBagGold>();
		OverpoweredGoldDustSupportSystem.RegisterNPC(ModContent.NPCType<ThoriumMod.NPCs.Myna>(), NPCID.GoldBird);
		OverpoweredGoldDustSupportSystem.RegisterNPC(NPCID.GiantFlyingFox, ModContent.NPCType<ThoriumMod.NPCs.GildedBat>());
		OverpoweredGoldDustSupportSystem.RegisterNPC(NPCID.Werewolf, ModContent.NPCType<ThoriumMod.NPCs.GildedLycan>());

		// Misc. Items
		OverpoweredGoldDustSupportSystem.RegisterItem<ThoriumMod.Items.BasicAccessories.CopperBuckler, ThoriumMod.Items.BasicAccessories.GoldAegis, ThoriumMod.Items.BasicAccessories.PlatinumAegis>();
		OverpoweredGoldDustSupportSystem.RegisterItem<ThoriumMod.Items.BasicAccessories.TinBuckler, ThoriumMod.Items.BasicAccessories.GoldAegis, ThoriumMod.Items.BasicAccessories.PlatinumAegis>();
		OverpoweredGoldDustSupportSystem.RegisterItem<ThoriumMod.Items.BasicAccessories.IronShield, ThoriumMod.Items.BasicAccessories.GoldAegis, ThoriumMod.Items.BasicAccessories.PlatinumAegis>();
		OverpoweredGoldDustSupportSystem.RegisterItem<ThoriumMod.Items.BasicAccessories.LeadShield, ThoriumMod.Items.BasicAccessories.GoldAegis, ThoriumMod.Items.BasicAccessories.PlatinumAegis>();
		OverpoweredGoldDustSupportSystem.RegisterItem<ThoriumMod.Items.BasicAccessories.SilverBulwark, ThoriumMod.Items.BasicAccessories.GoldAegis, ThoriumMod.Items.BasicAccessories.PlatinumAegis>();
		OverpoweredGoldDustSupportSystem.RegisterItem<ThoriumMod.Items.BasicAccessories.TungstenBulwark, ThoriumMod.Items.BasicAccessories.GoldAegis, ThoriumMod.Items.BasicAccessories.PlatinumAegis>();

		OverpoweredGoldDustSupportSystem.RegisterItem(ItemID.PlumbersHat, ModContent.ItemType<ThoriumMod.Items.Donate.GreedyHat>());
		OverpoweredGoldDustSupportSystem.RegisterItem(ItemID.PlumbersShirt, ModContent.ItemType<ThoriumMod.Items.Donate.GreedyShirt>());
		OverpoweredGoldDustSupportSystem.RegisterItem(ItemID.PlumbersPants, ModContent.ItemType<ThoriumMod.Items.Donate.GreedyPants>());
	}

	private static void RescueFairiesSupport()
	{
		if (!ModWithCalls.TryGetCaller(out RescueFairiesCaller caller))
		{
			return;
		}

		static bool CoinBagCondition(NPC npc) => npc.ModNPC is ThoriumMod.NPCs.CoinBagCopper; // All three coin bags inherit from copper.

		// The normal mimics and biome mimics are already handled via Rescue Fairies' default settings.
		// Pot mimics are not added.
		caller.AddTrackingCondition(CoinBagCondition);
		caller.AddTrackingCondition<ThoriumMod.NPCs.LifeCrystalMimic>();
		caller.AddTrackingCondition<ThoriumMod.NPCs.Buried.BuriedSpawn>();
		caller.AddTrackingCondition<ThoriumMod.NPCs.Granite.GraniteSpawn>();
	}

	private static void ROR2HealthBarsSupport()
	{
		if (!ModWithCalls.TryGetCaller(out ROR2HealthBarsCaller caller))
		{
			return;
		}

		caller.AddDesc<ThoriumMod.NPCs.Thunder.TheGrandThunderBirdv2>(_thoriumLocalization, "GrandThunderBird");
		caller.AddDesc<ThoriumMod.NPCs.QueenJelly.QueenJelly>(_thoriumLocalization, "QueenJelly");
		caller.AddDesc<ThoriumMod.NPCs.Bat.Viscount>(_thoriumLocalization, "Viscount");
		caller.AddDesc<ThoriumMod.NPCs.Granite.GraniteEnergyStorm>(_thoriumLocalization, "GraniteEnergyStorm");
		caller.AddDesc<ThoriumMod.NPCs.Buried.TheBuriedWarrior>(_thoriumLocalization, "BuriedChampion");
		caller.AddDesc<ThoriumMod.NPCs.Scouter.ThePrimeScouter>(_thoriumLocalization, "StarScouter");
		caller.AddDesc<ThoriumMod.NPCs.Blizzard.BoreanStrider>(_thoriumLocalization, "BoreanStrider");
		caller.AddDesc<ThoriumMod.NPCs.Blizzard.BoreanStriderPopped>(_thoriumLocalization, "BoreanStrider");
		caller.AddDesc<ThoriumMod.NPCs.Beholder.FallenDeathBeholder>(_thoriumLocalization, "FallenBeholder");
		caller.AddDesc<ThoriumMod.NPCs.Beholder.FallenDeathBeholder2>(_thoriumLocalization, "FallenBeholder");
		caller.AddDesc<ThoriumMod.NPCs.Lich.Lich>(_thoriumLocalization, "Lich");
		caller.AddDesc<ThoriumMod.NPCs.Lich.LichHeadless>(_thoriumLocalization, "Lich");
		caller.AddDesc<ThoriumMod.NPCs.Depths.Abyssion>(_thoriumLocalization, "ForgottenOne");
		caller.AddDesc<ThoriumMod.NPCs.Depths.AbyssionCracked>(_thoriumLocalization, "ForgottenOne");
		caller.AddDesc<ThoriumMod.NPCs.Depths.AbyssionReleased>(_thoriumLocalization, "ForgottenOne");
		caller.AddDesc<ThoriumMod.NPCs.Primordials.Aquaius>(_thoriumLocalization, "Aquais");
		caller.AddDesc<ThoriumMod.NPCs.Primordials.Omnicide>(_thoriumLocalization, "Omnicide");
		caller.AddDesc<ThoriumMod.NPCs.Primordials.SlagFury>(_thoriumLocalization, "SlagFury");
		caller.AddDesc<ThoriumMod.NPCs.Primordials.RealityBreaker>(_thoriumLocalization, "DreamEater");
	}

	private static void UniversalCraftSupport()
	{
		if (!ModWithCalls.TryGetCaller(out UniversalCraftCaller caller))
		{
			return;
		}

		static bool GuidesFinalGiftCondition() => NPC.downedMoonlord && ModContent.GetInstance<ThoriumConfigServer>().donatorOther.toggleGuidesFinalGift;

		caller.AddStation<ThoriumMod.Tiles.ArcaneArmorFabricator>(() => NPC.downedBoss1);
		// Removed since it's just a movable Demon/Crimson Altar.
		//caller.AddStation<ThoriumMod.Tiles.GrimPedestal>(() => NPC.downedBoss1);
		caller.AddStation<ThoriumMod.Tiles.GuidesFinalGiftTile>(GuidesFinalGiftCondition);
		caller.AddStation<ThoriumMod.Tiles.SoulForgeNew>(() => NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3);
		caller.AddStation<ThoriumMod.Tiles.ThoriumAnvil>();
	}
}