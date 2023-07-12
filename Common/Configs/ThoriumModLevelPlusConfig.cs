using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace RandomModCompat.Common.Configs;

internal sealed class ThoriumModLevelPlusConfig : ModConfig
{
	public override ConfigScope Mode => ConfigScope.ServerSide;

	[Label("Charisma: Symphonic Damage")]
	[Tooltip("How much Symphonic Damage the player gets per point")]
	[Slider]
	[Range(0.00f, 0.10f)]
	[Increment(0.01f)]
	[DefaultValue(0.01f)]
	public float BardDamagePerPoint;

	[Label("Charisma: Symphonic Crit")]
	[Tooltip("How many points it takes to gain 1% Symphonic Crit")]
	[Range(1, 30)]
	[DefaultValue(15)]
	public int PointsPerBardCrit;

	[Label("Intelligence: Radiant Damage")]
	[Tooltip("How much Radiant Damage the player gets per point")]
	[Slider]
	[Range(0.00f, 0.10f)]
	[Increment(0.01f)]
	[DefaultValue(0.01f)]
	public float HealerDamagePerPoint;

	[Label("Intelligence: Radiant Crit")]
	[Tooltip("How many points it takes to gain 1% Radiant Crit")]
	[Range(1, 30)]
	[DefaultValue(15)]
	public int PointsPerHealerCrit;
}