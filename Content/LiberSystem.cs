using Liber.Content.Items.Accessories;
using Liber.Content.Items.Weapons.Melee.Shortswords;
using Liber.Content.Items.Weapons.Melee.Spears;
using Liber.Content.Items.Weapons.Melee.Swinging;
using Liber.Content.Items.Weapons.Melee.Swords;
using Liber.Content.Items.Weapons.Summon.Whips;
using Liber.Content.NPCs.HardMode.Undead;
using Liber.Content.NPCs.PreHardMode.Animals;
using Liber.Content.NPCs.PreHardMode.Beasts;
using Liber.Content.NPCs.PreHardMode.Undead;
using Liber.Content.Tiles;
using RandomModCompat.Common.Callers;
using RandomModCompat.Common.ExplicitSupport;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content;

[JITWhenModsEnabled(_modName)]
internal sealed class LiberSystem : CrossModHandler
{
	private const string _modName = ModNames.Liber;
	public override string ModName => _modName;

	/*
	 * This file adds support for:
	 * - Asymmetric Equips
	 * - Atmospheric Torches
	 * - Thorium Mod
	 * - Universal Crafter
	 * - W1K's Weapon Scaling
	 */
	// TODO: AoMM for the Jackalope
	// TODO: MoR typing
	// TODO: TerraTyping

	internal override void OnModLoad()
	{
		AsymmetricEquipsLoadTextures();
	}

	private void AsymmetricEquipsLoadTextures()
	{
		if (!TryGetCaller(out AsymmetricEquipsCaller caller))
		{
			return;
		}

		caller.AddFlippedEquipTexture(_modName, nameof(VikingShield), EquipType.Shield);
	}

	internal override void PostSetupContent()
	{
		AsymmetricEquipsSupport();
		ImprovedTorchesSupport();
		ThoriumModSupport();
		UniversalCraftSupport();
		WWeaponScalingSupport();
	}

	private void AsymmetricEquipsSupport()
	{
		if (!TryGetCaller(out AsymmetricEquipsCaller caller))
		{
			return;
		}

		// Hidden
		caller.AddHiddenEquip<SkullRing>(EquipType.HandsOn);
		caller.AddHiddenEquip<WristBand>(EquipType.HandsOn);

		// Glove
		caller.AddGlove<Gauntlet>();

		// Flipped
		caller.AddFlippedEquip<VikingShield>(EquipType.Shield);
	}

	private static void ImprovedTorchesSupport()
	{
		if (!RandomModCompat.SupportEnabled(_modName, ModNames.ImprovedTorches))
		{
			return;
		}

		ImprovedTorchesSupportSystem.RegisterTorch<BioluminescentTorch>();
	}

	private void ThoriumModSupport()
	{
		if (!TryGetCaller(out ThoriumModCaller caller))
		{
			return;
		}

		caller.AddRepelledEnemy<JiangshiA>(ThoriumModCaller.ThoriumRepellentType.Zombie);
		caller.AddRepelledEnemy<ZombieBlistered>(ThoriumModCaller.ThoriumRepellentType.Zombie);
		caller.AddRepelledEnemy<ZombieBloated>(ThoriumModCaller.ThoriumRepellentType.Zombie);
		caller.AddRepelledEnemy<ZombieFrostbite>(ThoriumModCaller.ThoriumRepellentType.Zombie);
		caller.AddRepelledEnemy<ZombieFrozenPink>(ThoriumModCaller.ThoriumRepellentType.Zombie);
		caller.AddRepelledEnemy<ZombieHive>(ThoriumModCaller.ThoriumRepellentType.Zombie);
		caller.AddRepelledEnemy<ZombieHusk>(ThoriumModCaller.ThoriumRepellentType.Zombie);
		caller.AddRepelledEnemy<ZombiePirate>(ThoriumModCaller.ThoriumRepellentType.Zombie);
		caller.AddRepelledEnemy<ZombieSlimeGreen>(ThoriumModCaller.ThoriumRepellentType.Zombie);
		caller.AddRepelledEnemy<ZombieSlimePurple>(ThoriumModCaller.ThoriumRepellentType.Zombie);
		caller.AddRepelledEnemy<ZombieTunic>(ThoriumModCaller.ThoriumRepellentType.Zombie);
		caller.AddRepelledEnemy<ZombieVile>(ThoriumModCaller.ThoriumRepellentType.Zombie);
		caller.AddRepelledEnemy<ZombieFlying>(ThoriumModCaller.ThoriumRepellentType.Zombie);
		caller.AddRepelledEnemy<ZombieFlyingBody>(ThoriumModCaller.ThoriumRepellentType.Zombie);
		caller.AddRepelledEnemy<ZombieFlyingLegs>(ThoriumModCaller.ThoriumRepellentType.Zombie);

		caller.AddRepelledEnemy<GiantSkeleton>(ThoriumModCaller.ThoriumRepellentType.Skeleton);
		caller.AddRepelledEnemy<SkeletonSlinger>(ThoriumModCaller.ThoriumRepellentType.Skeleton);
		caller.AddRepelledEnemy<SkeletonSoldier>(ThoriumModCaller.ThoriumRepellentType.Skeleton);
		caller.AddRepelledEnemy<GiantSkeletonAncient>(ThoriumModCaller.ThoriumRepellentType.Skeleton);

		caller.AddRepelledEnemy<BatChill>(ThoriumModCaller.ThoriumRepellentType.Bat);
		caller.AddRepelledEnemy<BatSandy>(ThoriumModCaller.ThoriumRepellentType.Bat);

		caller.AddRepelledEnemy<Mudfly>(ThoriumModCaller.ThoriumRepellentType.Bug);
		// TODO: Centipede (internal class)
	}

	private void UniversalCraftSupport()
	{
		if (!TryGetCaller(out UniversalCraftCaller caller))
		{
			return;
		}

		caller.AddStation<TanningRack>();
		caller.AddStation<TransmutationTable>();
	}

	private void WWeaponScalingSupport()
	{
		if (!TryGetCaller(out WWeaponScalingCaller caller))
		{
			return;
		}

		caller.AddScaling<Baselard>(WWeaponScalingCaller.Tier.Iron);
		caller.AddScaling<CombatKnife>(WWeaponScalingCaller.Tier.Gold);
		caller.AddScaling<Rapier>(WWeaponScalingCaller.Tier.Gold);
		caller.AddScaling<Icicle>(WWeaponScalingCaller.Tier.Gold);
		caller.AddScaling<PalmAxe>(WWeaponScalingCaller.Tier.Iron);
		caller.AddScaling<BambooSword>(WWeaponScalingCaller.Tier.Iron);
		caller.AddScaling<BastardSword>(WWeaponScalingCaller.Tier.Gold);
		caller.AddScaling<RustedCutlass>(WWeaponScalingCaller.Tier.Iron);
		caller.AddScaling<VikingSword>(WWeaponScalingCaller.Tier.Gold);
		caller.AddScaling<Transendence>(WWeaponScalingCaller.Tier.Iron);
	}
}