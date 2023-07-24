using DBZMODPORT;
using DBZMODPORT.Items.Accessories;
using DBZMODPORT.Items.Accessories.Vanity;
using DBZMODPORT.Tiles;
using RandomModCompat.Common.Callers;
using RandomModCompat.Common.Configs;
using RandomModCompat.Common.ExplicitSupport;
using RandomModCompat.Core;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RandomModCompat.Content;

[JITWhenModsEnabled(_modName)]
internal sealed class DBZMODPORTSystem : CrossModHandler
{
	private const string _modName = ModNames.DBZMODPORT;
	public override string ModName => _modName;

	/*
	 * This file adds support for:
	 * - Asymmetric Equips
	 * - Level+
	 * - Universal Crafter
	 */

	internal override void PostSetupContent()
	{
		AsymmetricEquipsSupport();
		LevelplusSupport();
		UniversalCraftSupport();
	}

	private void AsymmetricEquipsSupport()
	{
		if (!TryGetCaller(out AsymmetricEquipsCaller caller))
		{
			return;
		}

		caller.AddHiddenEquip<GreenPotara>(EquipType.Face);
		caller.AddHiddenEquip<BattleKit>(EquipType.Face);
		caller.AddGlove<BattleKit>();
		caller.AddGlove<WornGloves>();

		void AddScouter<T>() where T : ModItem
		{
			caller.AddSmallHead<T>();
			caller.AddHiddenEquip<T>(EquipType.Face);
		}

		AddScouter<ScouterT2>();
		AddScouter<ScouterT3>();
		AddScouter<ScouterT4>();
		AddScouter<ScouterT5>();
		AddScouter<ScouterT6>();
	}

	private void LevelplusSupport()
	{
		if (!TryGetCaller(out LevelplusCaller caller))
		{
			return;
		}

		// DBT doesn't use DamageClass. :(
		// Hopefully they fix this in the next update.
		// In the meantime...

		static void AddDamage(Player player, ushort statValue)
		{
			if (player.TryGetModPlayer(out MyPlayer mPlayer))
			{
				mPlayer.KiDamage += LevelPlusValuesConfig.Instance.KiDamagePerPoint * statValue;
			}
		}

		caller.AddEffect(LevelplusSupportSystem.Stat.Strength,
			AddDamage,
			statValue => Language.GetTextValueWith("Mods.RandomModCompat.LevelPlus.AddDamage",
			new { Amount = (int)(statValue * (LevelPlusValuesConfig.Instance.KiDamagePerPoint * 100)), DamageType = "ki damage" }));

		static void AddCrit(Player player, ushort statValue)
		{
			if (player.TryGetModPlayer(out MyPlayer mPlayer))
			{
				mPlayer.kiCrit += statValue / LevelPlusValuesConfig.Instance.PointsPerKiCrit;
			}
		}

		caller.AddEffect(LevelplusSupportSystem.Stat.Strength,
			AddCrit,
			statValue => Language.GetTextValueWith("Mods.RandomModCompat.LevelPlus.AddCrit",
				new { Amount = statValue / LevelPlusValuesConfig.Instance.PointsPerKiCrit, DamageTypeNoDamage = "ki" }));

		static void AddRegen(Player player, ushort statValue)
		{
			if (player.TryGetModPlayer(out MyPlayer mPlayer))
			{
				mPlayer.kiRegen += statValue / LevelPlusValuesConfig.Instance.PointsPerKiRegen;
			}
		}

		caller.AddEffect(LevelplusSupportSystem.Stat.Constitution,
			AddRegen,
			statValue => Language.GetTextValueWith("Mods.RandomModCompat.LevelPlus.ResourceRegen",
			new { Amount = statValue / LevelPlusValuesConfig.Instance.PointsPerKiRegen, Resource = "ki" }));

		// TODO: Max ki? Speed? KB?
	}

	private void UniversalCraftSupport()
	{
		if (!TryGetCaller(out UniversalCraftCaller caller))
		{
			return;
		}

		caller.AddStation<ZTable>();
		caller.AddStation<KaiTable>(() => NPC.downedPlantBoss);
	}
}