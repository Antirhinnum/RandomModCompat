using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace RandomModCompat.Common.Configs;

#if TML_2022_09
[Label(_localizationPrefix + "DisplayName")]
#endif

public sealed class LevelPlusValuesConfig : ModConfig
{
	private const string _localizationPrefix = $"$Mods.RandomModCompat.Configs.{nameof(LevelPlusValuesConfig)}.";

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "Must be a public static field to be assigned to.")]
	public static LevelPlusValuesConfig Instance;

	public override ConfigScope Mode => ConfigScope.ServerSide;

#if TML_2022_09
	[Label(_localizationPrefix + nameof(KiDamagePerPoint) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(KiDamagePerPoint) + ".Tooltip")]
#endif

	[Slider]
	[Range(0.00f, 0.10f)]
	[Increment(0.01f)]
	[DefaultValue(0.01f)]
	public float KiDamagePerPoint;

#if TML_2022_09
	[Label(_localizationPrefix + nameof(PointsPerKiCrit) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(PointsPerKiCrit) + ".Tooltip")]
#endif

	[Range(1, 30)]
	[DefaultValue(15)]
	public int PointsPerKiCrit;

#if TML_2022_09
	[Label(_localizationPrefix + nameof(PointsPerKiRegen) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(PointsPerKiRegen) + ".Tooltip")]
#endif

	[Range(1, 50)]
	[DefaultValue(30)]
	public int PointsPerKiRegen;

#if TML_2022_09
	[Label(_localizationPrefix + nameof(ClickDamagePerPoint) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(ClickDamagePerPoint) + ".Tooltip")]
#endif

	[Slider]
	[Range(0.00f, 0.10f)]
	[Increment(0.01f)]
	[DefaultValue(0.01f)]
	public float ClickDamagePerPoint;

#if TML_2022_09
	[Label(_localizationPrefix + nameof(PointsPerClickCrit) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(PointsPerClickCrit) + ".Tooltip")]
#endif

	[Range(1, 30)]
	[DefaultValue(15)]
	public int PointsPerClickCrit;

#if TML_2022_09
	[Label(_localizationPrefix + nameof(BardDamagePerPoint) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(BardDamagePerPoint) + ".Tooltip")]
#endif

	[Slider]
	[Range(0.00f, 0.10f)]
	[Increment(0.01f)]
	[DefaultValue(0.01f)]
	public float BardDamagePerPoint;

#if TML_2022_09
	[Label(_localizationPrefix + nameof(PointsPerBardCrit) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(PointsPerBardCrit) + ".Tooltip")]
#endif

	[Range(1, 30)]
	[DefaultValue(15)]
	public int PointsPerBardCrit;

#if TML_2022_09
	[Label(_localizationPrefix + nameof(HealerDamagePerPoint) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(HealerDamagePerPoint) + ".Tooltip")]
#endif

	[Slider]
	[Range(0.00f, 0.10f)]
	[Increment(0.01f)]
	[DefaultValue(0.01f)]
	public float HealerDamagePerPoint;

#if TML_2022_09
	[Label(_localizationPrefix + nameof(PointsPerHealerCrit) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(PointsPerHealerCrit) + ".Tooltip")]
#endif

	[Range(1, 30)]
	[DefaultValue(15)]
	public int PointsPerHealerCrit;
}