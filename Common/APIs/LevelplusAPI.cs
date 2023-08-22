using RandomModCompat.Common.Configs;
using RandomModCompat.Core;
using System;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

internal sealed partial class LevelplusAPI : ModAPI
{
	protected internal override string ModName => ModNames.levelplus;

	protected override bool SupportEnabled()
	{
		return !ModContent.GetInstance<RandomModCompatConfig>().DisableIL;
	}

	#region Custom

	internal void AddEffect(Stat stat, ApplyBonus effect, GetDescription description)
	{
		LevelplusSupportSystem.AddEffect(stat, effect, description);
	}

	internal void AddDamageAndCritEffects(Stat stat, DamageClass damageClass, Func<float> damagePerPoint = null, Func<int> pointsPerCrit = null)
	{
		damagePerPoint ??= () => 0.01f;
		pointsPerCrit ??= () => 15;

		// TODO: Check if this should be += instead.
		AddEffect(stat,
			(player, statValue) => player.GetDamage(damageClass) *= 1f + (damagePerPoint() * statValue),
			statValue => Language.GetTextValueWith("Mods.RandomModCompat.LevelPlus.AddDamage",
				new { Amount = (int)(statValue * (damagePerPoint() * 100)), DamageType = damageClass.DisplayName }));
		AddEffect(stat,
			(player, statValue) => player.GetCritChance(damageClass) += statValue / pointsPerCrit(),
			statValue => Language.GetTextValueWith("Mods.RandomModCompat.LevelPlus.AddCrit",
				new { Amount = statValue / pointsPerCrit(), DamageTypeNoDamage = StripDamageFromClassName(damageClass) }));
	}

	#endregion Custom

	private static string StripDamageFromClassName(DamageClass damageClass)
	{
#if TML_2022_09
		if (Language.ActiveCulture == GameCulture.FromCultureName(GameCulture.CultureName.English))
		{
			// Remove the word "damage".
			return damageClass.DisplayName.Replace("damage", null).Trim();
		}
		else
		{
			// No idea how to handle this.
			return damageClass.DisplayName;
		}
#else
		return damageClass.DisplayName.ToString().Replace("damage", null).Trim();
#endif
	}
}