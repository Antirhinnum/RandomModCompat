using Microsoft.Xna.Framework;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

internal sealed class ColoredDamageTypesAPI : ModAPI
{
	protected internal override string ModName => ModNames.ColoredDamageTypes;

	internal void AddDamageType(DamageClass damage, Color tooltipColor, Color damageColor, Color critDamageColor)
	{
		WrappedMod.Call(nameof(AddDamageType), damage, tooltipColor, damage, critDamageColor);
	}

	internal void AddDamageType<T>(Color tooltipColor, Color damageColor, Color critDamageColor) where T : DamageClass
	{
		AddDamageType(ModContent.GetInstance<T>(), tooltipColor, damageColor, critDamageColor);
	}

	internal void AddDamageType(DamageClass damage, (int, int, int) tooltipColor, (int, int, int) damageColor, (int, int, int) critDamageColor)
	{
		WrappedMod.Call(nameof(AddDamageType), damage, tooltipColor, damage, critDamageColor);
	}

	internal void AddDamageType<T>((int, int, int) tooltipColor, (int, int, int) damageColor, (int, int, int) critDamageColor) where T : DamageClass
	{
		AddDamageType(ModContent.GetInstance<T>(), tooltipColor, damageColor, critDamageColor);
	}
}