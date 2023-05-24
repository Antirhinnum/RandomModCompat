using Consolaria.Content.Buffs;
using Consolaria.Content.Items.Accessories;
using Consolaria.Content.Items.Consumables;
using Consolaria.Content.Items.Weapons.Summon;
using Consolaria.Content.NPCs;
using Consolaria.Content.NPCs.Bosses.Ocram;
using Consolaria.Content.NPCs.Bosses.Turkor;
using Consolaria.Content.Projectiles.Friendly;
using RandomModCompat.Common.Callers;
using RandomModCompat.Core;
using System;
using Terraria.ModLoader;

namespace RandomModCompat.Content;

[JITWhenModsEnabled(_modName)]
internal sealed class ConsolariaSystem : CrossModHandler
{
	private int _lepusType;
	private const string _consolariaLocalization = $"Mods.RandomModCompat.{ModNames.Consolaria}.";

	private const string _modName = ModNames.Consolaria;
	public override string ModName => _modName;

	/*
	 * Consolaria adds support for the following mods:
	 * - Boss Checklist
	 * - Fargo's Mutant Mod
	 *
	 * This file adds support for:
	 * - Asymmetric Equips
	 * - Item Check Blacklist Lib
	 * - Magic Storage
	 * - RoR 2 Health Bars
	 * - Summoner's Association
	 * - TerraTyping
	 * - Thorium Mod
	 */
	// TODO: Bosses as NPCs?
	// TODO: MoR typing
	// TODO: AoMM support

	internal override void PostSetupContent()
	{
		// Internal class :(
		_lepusType = ModContent.Find<ModNPC>(ModNames.Consolaria, "Lepus").Type;

		AsymmetricEquipsSupport();
		ItemCheckBlacklistSupport();
		MagicStorageSupport();
		ROR2HealthBarsSupport();
		SummonersAssociationSupport();
		TerraTypingSupport();
		ThoriumModSupport();
	}

	private void AsymmetricEquipsSupport()
	{
		if (!TryGetCaller(out AsymmetricEquipsCaller caller))
		{
			return;
		}

		// Hidden
		caller.AddHiddenEquip<ValentineRing>(EquipType.HandsOn);

		// TODO: Flipped equips for the following:
		// - PhantasmalRobe
		// - AlpineHat
		// - FabulousRibbon (?)
		// - GeorgesTuxedo (Cane)
		// - MonokumaBody, MonokumaHead, MonokumaLegs
		// - MonomiBody, MonomiHead, MonomiLegs
		// - MythicalRobe
		// - ShirenShirt
	}

	private void ItemCheckBlacklistSupport()
	{
		if (!TryGetCaller(out ItemCheckBlacklistCaller caller))
		{
			return;
		}

		// UPDATE: See if Consolaria ever fixes this. It's impossible to get since the required condition (Main.bloodMoon) is set to a readonly field on mod load, so it's always false.
		caller.Add<Consolaria.Content.Items.Consumables.HolyHandgrenade2>();
	}

	private void MagicStorageSupport()
	{
		if (!TryGetCaller(out MagicStorageCaller caller))
		{
			return;
		}

		caller.RegisterShadowDiamondDrop(_lepusType, 1);
		caller.RegisterShadowDiamondDrop<Ocram>(1);
		caller.RegisterShadowDiamondDrop<TurkortheUngrateful>(1);
	}

	private void ROR2HealthBarsSupport()
	{
		if (!TryGetCaller(out ROR2HealthBarsCaller caller))
		{
			return;
		}

		caller.AddDesc(_lepusType, _consolariaLocalization, "Lepus");
		caller.AddDesc<Ocram>(_consolariaLocalization, "Ocram");
		caller.AddDesc<TurkortheUngrateful>(_consolariaLocalization, "Turkor");
	}

	private void SummonersAssociationSupport()
	{
		if (!TryGetCaller(out SummonersAssociationCaller caller))
		{
			return;
		}

		caller.AddMinionInfo(ModContent.ItemType<EternityStaff>(), ModContent.BuffType<Consolaria.Content.Buffs.EyeOfEternity>(), ModContent.ProjectileType<Consolaria.Content.Projectiles.Friendly.EyeOfEternity>());
		caller.AddMinionInfo(ModContent.ItemType<TurkeyStuff>(), ModContent.BuffType<WeirdTurkey>(), ModContent.ProjectileType<TurkeyHead>());
	}

	private void TerraTypingSupport()
	{
		if (!TryGetCaller(out TerraTypingCaller caller))
		{
			return;
		}

		caller.AddTypes(TerraTypingCaller.TypeToAdd.Ammo, CrossMod);
		caller.AddTypes(TerraTypingCaller.TypeToAdd.Armor, CrossMod);
		caller.AddTypes(TerraTypingCaller.TypeToAdd.NPC, CrossMod);
		caller.AddTypes(TerraTypingCaller.TypeToAdd.Projectile, CrossMod);
		caller.AddTypes(TerraTypingCaller.TypeToAdd.Weapon, CrossMod);
	}

	private void ThoriumModSupport()
	{
		if (!TryGetCaller(out ThoriumModCaller caller))
		{
			return;
		}

		caller.AddRepelledEnemy<AlbinoAntlion>(ThoriumModCaller.ThoriumRepellentType.Bug);
		caller.AddRepelledEnemy<AlbinoCharger>(ThoriumModCaller.ThoriumRepellentType.Bug);
		caller.AddRepelledEnemy<DragonHornet>(ThoriumModCaller.ThoriumRepellentType.Bug);
		caller.AddRepelledEnemy<GiantAlbinoCharger>(ThoriumModCaller.ThoriumRepellentType.Bug);

		caller.AddUndeadEnemy<VampireMiner>();
	}
}