using DBZMODPORT;
using RandomModCompat.Common.APIs;
using RandomModCompat.Common.Configs;
using RandomModCompat.Core;
using Terraria;
using Terraria.Localization;

namespace RandomModCompat.Content.Modules;

internal sealed class DBZMODPORTLevelplusModule : CrossModModule<LevelplusAPI>
{
	public override string CrossModName => ModNames.DBZMODPORT;

	protected internal override void PostSetupContent()
	{
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

		API.AddEffect(LevelplusAPI.Stat.Strength,
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

		API.AddEffect(LevelplusAPI.Stat.Strength,
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

		API.AddEffect(LevelplusAPI.Stat.Constitution,
			AddRegen,
			statValue => Language.GetTextValueWith("Mods.RandomModCompat.LevelPlus.ResourceRegen",
			new { Amount = statValue / LevelPlusValuesConfig.Instance.PointsPerKiRegen, Resource = "ki" }));

		// TODO: Max ki? Speed? KB?
	}
}