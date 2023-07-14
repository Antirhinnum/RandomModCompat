using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace RandomModCompat.Common.Configs;

public sealed class LevelPlusValuesConfig : ModConfig
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "Must be a public static field to be assigned to.")]
	public static LevelPlusValuesConfig Instance;

	public override ConfigScope Mode => ConfigScope.ServerSide;

	[Label("The Clicker Class: Clicker Damage (DEX)")]
	[Tooltip("How much Clicker Damage the player gets per point")]
	[Slider]
	[Range(0.00f, 0.10f)]
	[Increment(0.01f)]
	[DefaultValue(0.01f)]
	public float ClickerDamagePerPoint;

	[Label("The Clicker Class: Clicker Crit (DEX)")]
	[Tooltip("How many points it takes to gain 1% Clicker Crit")]
	[Range(1, 30)]
	[DefaultValue(15)]
	public int PointsPerClickerCrit;

	[Label("Thorium Mod: Symphonic Damage (CHA)")]
	[Tooltip("How much Symphonic Damage the player gets per point")]
	[Slider]
	[Range(0.00f, 0.10f)]
	[Increment(0.01f)]
	[DefaultValue(0.01f)]
	public float BardDamagePerPoint;

	[Label("Thorium Mod: Symphonic Crit (CHA)")]
	[Tooltip("How many points it takes to gain 1% Symphonic Crit")]
	[Range(1, 30)]
	[DefaultValue(15)]
	public int PointsPerBardCrit;

	[Label("Thorium Mod: Radiant Damage (INT)")]
	[Tooltip("How much Radiant Damage the player gets per point")]
	[Slider]
	[Range(0.00f, 0.10f)]
	[Increment(0.01f)]
	[DefaultValue(0.01f)]
	public float HealerDamagePerPoint;

	[Label("Thorium Mod: Radiant Crit (INT)")]
	[Tooltip("How many points it takes to gain 1% Radiant Crit")]
	[Range(1, 30)]
	[DefaultValue(15)]
	public int PointsPerHealerCrit;
}