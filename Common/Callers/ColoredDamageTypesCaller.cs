using Microsoft.Xna.Framework;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Common.Callers
{
	internal sealed class ColoredDamageTypesCaller : ModWithCalls
	{
		protected internal override string ModName => "ColoredDamageTypes";

		internal void AddDamageType(DamageClass damage, Color tooltipColor, Color damageColor, Color critDamageColor)
		{
			CalledMod.Call(nameof(AddDamageType), damage, tooltipColor, damage, critDamageColor);
		}

		internal void AddDamageType<T>(Color tooltipColor, Color damageColor, Color critDamageColor) where T : DamageClass
		{
			AddDamageType(ModContent.GetInstance<T>(), tooltipColor, damageColor, critDamageColor);
		}

		internal void AddDamageType(DamageClass damage, (int, int, int) tooltipColor, (int, int, int) damageColor, (int, int, int) critDamageColor)
		{
			CalledMod.Call(nameof(AddDamageType), damage, tooltipColor, damage, critDamageColor);
		}

		internal void AddDamageType<T>((int, int, int) tooltipColor, (int, int, int) damageColor, (int, int, int) critDamageColor) where T : DamageClass
		{
			AddDamageType(ModContent.GetInstance<T>(), tooltipColor, damageColor, critDamageColor);
		}
	}
}