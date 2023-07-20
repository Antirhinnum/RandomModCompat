using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace RandomModCompat.Common.Configs;

[Label(_localizationPrefix + "Name")]
public sealed class LevelPlusValuesConfig : ModConfig
{
	private const string _localizationPrefix = "$Mods.RandomModCompat.Configs.LevelPlus.";

	[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "Must be a public static field to be assigned to.")]
	public static LevelPlusValuesConfig Instance;

	public override ConfigScope Mode => ConfigScope.ServerSide;

	[Label(_localizationPrefix + nameof(KiDamagePerPoint) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(KiDamagePerPoint) + ".Tooltip")]
	[Slider]
	[Range(0.00f, 0.10f)]
	[Increment(0.01f)]
	[DefaultValue(0.01f)]
	public float KiDamagePerPoint;

	[Label(_localizationPrefix + nameof(PointsPerKiCrit) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(PointsPerKiCrit) + ".Tooltip")]
	[Range(1, 30)]
	[DefaultValue(15)]
	public int PointsPerKiCrit;

	[Label(_localizationPrefix + nameof(PointsPerKiRegen) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(PointsPerKiRegen) + ".Tooltip")]
	[Range(1, 50)]
	[DefaultValue(30)]
	public int PointsPerKiRegen;

	[Label(_localizationPrefix + nameof(ClickDamagePerPoint) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(ClickDamagePerPoint) + ".Tooltip")]
	[Slider]
	[Range(0.00f, 0.10f)]
	[Increment(0.01f)]
	[DefaultValue(0.01f)]
	public float ClickDamagePerPoint;

	[Label(_localizationPrefix + nameof(PointsPerClickCrit) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(PointsPerClickCrit) + ".Tooltip")]
	[Range(1, 30)]
	[DefaultValue(15)]
	public int PointsPerClickCrit;

	[Label(_localizationPrefix + nameof(BardDamagePerPoint) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(BardDamagePerPoint) + ".Tooltip")]
	[Slider]
	[Range(0.00f, 0.10f)]
	[Increment(0.01f)]
	[DefaultValue(0.01f)]
	public float BardDamagePerPoint;

	[Label(_localizationPrefix + nameof(PointsPerBardCrit) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(PointsPerBardCrit) + ".Tooltip")]
	[Range(1, 30)]
	[DefaultValue(15)]
	public int PointsPerBardCrit;

	[Label(_localizationPrefix + nameof(HealerDamagePerPoint) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(HealerDamagePerPoint) + ".Tooltip")]
	[Slider]
	[Range(0.00f, 0.10f)]
	[Increment(0.01f)]
	[DefaultValue(0.01f)]
	public float HealerDamagePerPoint;

	[Label(_localizationPrefix + nameof(PointsPerHealerCrit) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(PointsPerHealerCrit) + ".Tooltip")]
	[Range(1, 30)]
	[DefaultValue(15)]
	public int PointsPerHealerCrit;
}