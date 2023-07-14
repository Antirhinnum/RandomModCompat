using ClickerClass;
using ClickerClass.Items.Accessories;
using RandomModCompat.Common.Callers;
using RandomModCompat.Common.Configs;
using RandomModCompat.Common.ExplicitSupport;
using RandomModCompat.Core;
using System;
using Terraria.ModLoader;

namespace RandomModCompat.Content.ClickerClassSupport;

[JITWhenModsEnabled(_modName)]
internal sealed class ClickerClassSystem : CrossModHandler
{
	private const string _modName = ModNames.ClickerClass;
	public override string ModName => _modName;

	/*
	 * The Clicker Class adds support for the following mods:
	 * - Colored Damage Types
	 * - Recipe Browser
	 * - Thorium Mod
	 * - WikiThis
	 * 
	 * Additionally, the following mods either have intrinsic Thorium support or an existing compatibility mod:
	 * - Bosses as NPCs
	 * 
	 * This file adds support for:
	 * - Asymmetric Equips
	 * - Level+
	 * 
	 * Additionally:
	 * - ClickerClassFishingRebornSystem: Adds support for Fishing Reborn.
	 */

	internal override void PostSetupContent()
	{
		AsymmetricEquipsSupport();
		LevelplusSupport();
	}

	private void AsymmetricEquipsSupport()
	{
		if (!TryGetCaller(out AsymmetricEquipsCaller caller))
		{
			return;
		}

		caller.AddHiddenEquip<AimAssistModule>(EquipType.Face);
		caller.AddHiddenEquip<IcePack>(EquipType.Waist);
		caller.AddHiddenEquip<Milk>(EquipType.Waist);
		caller.AddHiddenEquip<Soda>(EquipType.Waist);

		// TODO: Need flipped sprites for:
		// - Gamer Crate
		// - All three Clicking Gloves (Normal, Ancient, Regal) [Need offhand]
	}

	private void LevelplusSupport()
	{
		if (!RandomModCompat.SupportEnabled(_modName, ModNames.levelplus))
		{
			return;
		}

		if (!CrossMod.TryFind(nameof(ClickerDamage), out DamageClass clickerDamage))
		{
			Mod.Logger.Warn("Cannot find clicker damage for Level+ support!");
			return;
		}

		LevelPlusValuesConfig config = LevelPlusValuesConfig.Instance;
		LevelplusSupportSystem.AddDamageAndCritEffects(LevelplusSupportSystem.Stat.Dexterity, clickerDamage, () => config.ClickerDamagePerPoint, () => config.PointsPerClickerCrit);

		// TODO: More stats.
	}
}