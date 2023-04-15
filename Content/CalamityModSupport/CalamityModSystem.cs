﻿using Microsoft.Xna.Framework;
using RandomModCompat.Common.Callers;
using RandomModCompat.Core;
using Terraria;
using Terraria.ModLoader;

namespace RandomModCompat.Content.CalamityModSupport;

[JITWhenModsEnabled(_modName)]
internal sealed class CalamityModSystem : CrossModHandler
{
	private const string _modName = "CalamityMod";
	protected override string ModName => _modName;

	/*
	 * Calamity adds support for the following mods:
	 * - Boss Checklist
	 * - Census
	 * - Dialogue Panel Rework
	 * - Fargo's Mutant Mod
	 * - Summoner's Association
	 * - Wikithis
	 *
	 * Additionally, the following mods either have intrinsic Calamity support or an existing compatibility mod:
	 * - Bosses as NPCs
	 *
	 * This file adds support for:
	 * - Asymmetric Equips (partial)
	 * - Bangarang (partial)
	 * - Colored Damage Types (TODO: Debug)
	 * - Enhanced Buff Display
	 * - Item Check Blacklist
	 * - Thorium Mod
	 * - Universal Crafter
	 */

	internal override void PostSetupContent()
	{
		AsymmetricEquipsSupport();
		BangarangSupport();
		BuffDisplaySupport();
		//ColoredDamageTypesSupport();
		ItemCheckBlacklistSupport();
		ThoriumModSupport();
		UniversalCraftSupport();
	}

	private static void AsymmetricEquipsSupport()
	{
		if (!ModWithCalls.TryGetCaller(out AsymmetricEquipsCaller caller))
		{
			return;
		}

		// Hidden
		caller.AddHiddenEquip<CalamityMod.Items.Accessories.DepthCharm>(EquipType.Waist);

		// Gloves
		caller.AddGlove<CalamityMod.Items.Accessories.BloodstainedGlove>();
		caller.AddGlove<CalamityMod.Items.Accessories.ElectriciansGlove>();
		caller.AddGlove<CalamityMod.Items.Accessories.ElementalGauntlet>();
		caller.AddGlove<CalamityMod.Items.Accessories.FilthyGlove>();
		caller.AddGlove<CalamityMod.Items.Accessories.GloveOfPrecision>();
		caller.AddGlove<CalamityMod.Items.Accessories.GloveOfRecklessness>();
	}

	private static void BangarangSupport()
	{
		if (!ModWithCalls.TryGetCaller(out BangarangCaller caller))
		{
			return;
		}

		// Need to figure out some way to "or" this condition.
		// Calamity boomerangs can be thrown if:
		// a) There's less than the cap out.
		// b) A stealth strike is possible.
		//static bool StealthStrikeAvailable(Player player, Item item, int boomerangs) => player.Calamity().StealthStrikeAvailable();

		// Melee
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Melee.FallenPaladinsHammer>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Melee.GalaxySmasher>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Melee.SeekingScorcher>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Melee.StellarContempt>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Melee.TriactisTruePaladinianMageHammerofMightMelee>(-1);

		// Rogue
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.BlazingStar>(3);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.Brimblade>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.Celestus>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.DynamicPursuer>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.ElementalDisk>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.EnchantedAxe>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.EpidemicShredder>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.Equanimity>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.Eradicator>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.FishboneBoomerang>(1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.FrostcrushValari>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.GhoulishGouger>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.Glaive>(3);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.Icebreaker>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.InfestedClawmerang>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.KelvinCatalyst>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.Kylie>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.MangroveChakram>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.MoltenAmputator>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.NanoblackReaper>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.SandDollar>(2/*, StealthStrikeAvailable*/);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.SubductionSlicer>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.TerraDisk>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.ToxicantTwister>(-1);
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.Rogue.Valediction>(-1);

		// Draedon
		caller.RegisterSimpleBoomerang<CalamityMod.Items.Weapons.DraedonsArsenal.TrackingDisk>(-1);

		// Complex

		// Bangarang handles this really weirdly, where it compares the number of allowed boomerangs to the highest amnount of any active projectiles, instead of the sum.
		// In short, this may be broken with Bangarang support.
		caller.RegisterBoomerang<CalamityMod.Items.Weapons.Rogue.DefectiveSphere>(new int[]
		{
			ModContent.ProjectileType<CalamityMod.Projectiles.Rogue.SphereSpiked>(),
			ModContent.ProjectileType<CalamityMod.Projectiles.Rogue.SphereBladed>(),
			ModContent.ProjectileType<CalamityMod.Projectiles.Rogue.SphereYellow>(),
			ModContent.ProjectileType<CalamityMod.Projectiles.Rogue.SphereBlue>()
		}, 5/*, StealthStrikeAvailable*/);
	}

	private static void BuffDisplaySupport()
	{
		if (!ModWithCalls.TryGetCaller(out BuffDisplayCaller caller))
		{
			return;
		}

		caller.SetCountAs(ModContent.BuffType<CalamityMod.Buffs.PopoNoselessBuff>(), ModContent.BuffType<CalamityMod.Buffs.PopoBuff>());
		caller.SetCountAs(ModContent.BuffType<CalamityMod.Buffs.Mounts.AndromedaSmallBuff>(), ModContent.BuffType<CalamityMod.Buffs.Mounts.AndromedaBuff>());

		caller.SetCountAs(ModContent.BuffType<CalamityMod.Buffs.StatBuffs.HallowedRuneDefense>(), ModContent.BuffType<CalamityMod.Buffs.StatBuffs.HallowedRuneRegeneration>());
		caller.SetCountAs(ModContent.BuffType<CalamityMod.Buffs.StatBuffs.HallowedRunePower>(), ModContent.BuffType<CalamityMod.Buffs.StatBuffs.HallowedRuneRegeneration>());

		caller.SetCountAs(ModContent.BuffType<CalamityMod.Buffs.StatBuffs.PhantomicEmpowerment>(), ModContent.BuffType<CalamityMod.Buffs.StatBuffs.PhantomicShield>());
		caller.SetCountAs(ModContent.BuffType<CalamityMod.Buffs.StatBuffs.PhantomicRegen>(), ModContent.BuffType<CalamityMod.Buffs.StatBuffs.PhantomicShield>());

		caller.SetCountAs(ModContent.BuffType<CalamityMod.Buffs.StatBuffs.SpiritDefense>(), ModContent.BuffType<CalamityMod.Buffs.StatBuffs.SpiritRegen>());
		caller.SetCountAs(ModContent.BuffType<CalamityMod.Buffs.StatBuffs.SpiritPower>(), ModContent.BuffType<CalamityMod.Buffs.StatBuffs.SpiritRegen>());
	}

	private static void ColoredDamageTypesSupport()
	{
		if (!ModWithCalls.TryGetCaller(out ColoredDamageTypesCaller caller))
		{
			return;
		}

		// TODO: This mysteriously doesn't work?

		//// HSL: (240, 0.15, 0.7) | (240, 0.1, 0.8) | (240, 0.2, 0.6)
		//caller.AddDamageType<CalamityMod.AverageDamageClass>((167, 167, 190), (199, 199, 209), (133, 133, 173));
		//// HSL: (60, 0.4, 0.5) | (60, 0.3, 0.55) | (60, 0.65, 0.45)
		//caller.AddDamageType<CalamityMod.RogueDamageClass>((178, 179, 77), (175, 175, 106), (189, 189, 40));

		//// Adapted from default Melee colors.
		//// Melee RGB: (235, 25, 25) | (170, 0, 0) | (255, 10, 50)
		//(int, int, int) trueMeleeTooltipColor = (230, 20, 20);
		//(int, int, int) trueMeleeDamageColor = (160, 0, 0);
		//(int, int, int) trueMeleeCritDamageColor = (250, 5, 45);
		//caller.AddDamageType<CalamityMod.TrueMeleeDamageClass>(trueMeleeTooltipColor, trueMeleeDamageColor, trueMeleeCritDamageColor);
		//caller.AddDamageType<CalamityMod.TrueMeleeNoSpeedDamageClass>(trueMeleeTooltipColor, trueMeleeDamageColor, trueMeleeCritDamageColor);
	}

	private static void ItemCheckBlacklistSupport()
	{
		if (!ModWithCalls.TryGetCaller(out ItemCheckBlacklistCaller caller))
		{
			return;
		}

		caller.Add<CalamityMod.Items.SulphurousSeaWorldSideChanger>();
	}

	private static void ThoriumModSupport()
	{
		if (!ModWithCalls.TryGetCaller(out ThoriumModCaller caller))
		{
			return;
		}

		// Flails
		caller.AddFlailProjectileID<CalamityMod.Projectiles.Melee.BallOFuguProj>();
		caller.AddFlailProjectileID<CalamityMod.Projectiles.Melee.ClamCrusherFlail>();
		caller.AddFlailProjectileID<CalamityMod.Projectiles.Melee.CosmicDischargeFlail>();
		caller.AddFlailProjectileID<CalamityMod.Projectiles.Melee.CrescentMoonFlail>();
		caller.AddFlailProjectileID<CalamityMod.Projectiles.Melee.DragonPowFlail>();
		caller.AddFlailProjectileID<CalamityMod.Projectiles.Melee.MourningstarFlail>();
		caller.AddFlailProjectileID<CalamityMod.Projectiles.Melee.NebulashFlail>();
		caller.AddFlailProjectileID<CalamityMod.Projectiles.DraedonsArsenal.PulseDragonProjectile>();
		caller.AddFlailProjectileID<CalamityMod.Projectiles.Melee.RemsRevengeProj>();
		caller.AddFlailProjectileID<CalamityMod.Projectiles.Melee.SpineOfThanatosProjectile>();
		caller.AddFlailProjectileID<CalamityMod.Projectiles.Melee.TumbleweedFlail>();
		caller.AddFlailProjectileID<CalamityMod.Projectiles.Melee.UrchinBall>();
		caller.AddFlailProjectileID<CalamityMod.Projectiles.Melee.UrchinMaceProjectile>();
		caller.AddFlailProjectileID<CalamityMod.Projectiles.Melee.YateveoBloomProj>();

		// Martian
		caller.AddMartianItemID<CalamityMod.Items.Weapons.Ranged.NullificationRifle>();
		caller.AddMartianItemID<CalamityMod.Items.Weapons.Magic.Wingman>();
		caller.AddMartianItemID<CalamityMod.Items.Weapons.Rogue.ShockGrenade>();
		
		// TODO: DoT debuffs?
		// TODO: Status debuffs?
	}

	private static void UniversalCraftSupport()
	{
		if (!ModWithCalls.TryGetCaller(out UniversalCraftCaller caller))
		{
			return;
		}

		static bool PostDesertScourge() => CalamityMod.DownedBossSystem.downedDesertScourge;
		static bool PostSlimeGod() => CalamityMod.DownedBossSystem.downedSlimeGod;
		static bool PostDragonfolly() => CalamityMod.DownedBossSystem.downedDragonfolly;
		static bool PostProvidence() => CalamityMod.DownedBossSystem.downedProvidence;
		static bool PostDoG() => CalamityMod.DownedBossSystem.downedDoG;
		static bool PostYharon() => CalamityMod.DownedBossSystem.downedYharon;
		static bool DraedonsForgeCondition()
		{
			return CalamityMod.DownedBossSystem.downedDoG // Cosmic Anvil, Phantoplasm
				&& CalamityMod.DownedBossSystem.downedYharon // Auric bars
				&& CalamityMod.DownedBossSystem.downedExoMechs; // Exo Prism
		}

		caller.AddStation<CalamityMod.Tiles.Furniture.CraftingStations.WulfrumLabstation>();
		caller.AddStation<CalamityMod.Tiles.Furniture.CraftingStations.EutrophicShelf>(PostDesertScourge);
		caller.AddStation<CalamityMod.Tiles.Furniture.CraftingStations.StaticRefiner>(PostSlimeGod);
		caller.AddStation<CalamityMod.Tiles.Furniture.CraftingStations.AncientAltar>(() => Main.hardMode);
		caller.AddStation<CalamityMod.Tiles.Furniture.CraftingStations.AshenAltar>(() => Main.hardMode);
		caller.AddStation<CalamityMod.Tiles.Furniture.CraftingStations.MonolithAmalgam>(() => Main.hardMode);
		caller.AddStation<CalamityMod.Tiles.Furniture.CraftingStations.PlagueInfuser>(() => NPC.downedGolemBoss);
		caller.AddStation<CalamityMod.Tiles.Furniture.CraftingStations.VoidCondenser>(() => Main.hardMode);
		caller.AddStation<CalamityMod.Tiles.Furniture.CraftingStations.ProfanedCrucible>(() => NPC.downedMoonlord);
		caller.AddStation<CalamityMod.Tiles.Furniture.CraftingStations.BotanicPlanter>(PostProvidence);
		caller.AddStation<CalamityMod.Tiles.Furniture.CraftingStations.CosmicAnvil>(PostDoG);
		caller.AddStation<CalamityMod.Tiles.Furniture.CraftingStations.SilvaBasin>(PostDragonfolly);
		caller.AddStation<CalamityMod.Tiles.Furniture.CraftingStations.DraedonsForge>(DraedonsForgeCondition);
		caller.AddStation<CalamityMod.Tiles.Furniture.CraftingStations.SCalAltar>(PostYharon);
	}
}