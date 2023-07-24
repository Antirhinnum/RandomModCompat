using RandomModCompat.Common.Configs;
using RandomModCompat.Common.ExplicitSupport;
using RandomModCompat.Core;
using System;
using Terraria.Localization;
using Terraria.ModLoader;
using static RandomModCompat.Common.ExplicitSupport.LevelplusSupportSystem;

namespace RandomModCompat.Common.Callers;

/// <summary>
/// Wraps <see cref="LevelplusSupportSystem"/> for use with <see cref="CrossModHandler.TryGetCaller{T}(out T)"/>.
/// </summary>
internal sealed class LevelplusCaller : ModWithCalls
{
	protected internal override string ModName => ModNames.levelplus;

	protected override bool SupportEnabled()
	{
		return !ModContent.GetInstance<RandomModCompatConfig>().DisableIL;
	}

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Should only be called from an instance.")]
	internal void AddEffect(Stat stat, StatBonus.ApplyBonus effect, StatBonus.GetDescription description)
	{
		LevelplusSupportSystem.AddEffect(stat, effect, description);
	}

	internal void AddDamageAndCritEffects(Stat stat, DamageClass damageClass, Func<float> damagePerPoint = null, Func<int> pointsPerCrit = null)
	{
		damagePerPoint ??= () => 0.01f;
		pointsPerCrit ??= () => 15;

		AddEffect(stat,
			(player, statValue) => player.GetDamage(damageClass) *= 1f + (damagePerPoint() * statValue),
			statValue => Language.GetTextValueWith("Mods.RandomModCompat.LevelPlus.AddDamage",
				new { Amount = (int)(statValue * (damagePerPoint() * 100)), DamageType = damageClass.DisplayName }));
		AddEffect(stat,
			(player, statValue) => player.GetCritChance(damageClass) += statValue / pointsPerCrit(),
			statValue => Language.GetTextValueWith("Mods.RandomModCompat.LevelPlus.AddCrit",
				new { Amount = statValue / pointsPerCrit(), DamageTypeNoDamage = StripDamageFromClassName(damageClass) }));
	}

	private static string StripDamageFromClassName(DamageClass damageClass)
	{
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
	}
}