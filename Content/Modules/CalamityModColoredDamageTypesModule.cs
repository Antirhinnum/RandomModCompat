using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class CalamityModColoredDamageTypesModule : CrossModModule<ColoredDamageTypesAPI>
{
	public override string CrossModName => ModNames.CalamityMod;

	protected internal override void PostSetupContent()
	{
		// TODO: This mysteriously doesn't work?

		//// HSL: (240, 0.15, 0.7) | (240, 0.1, 0.8) | (240, 0.2, 0.6)
		//API.AddDamageType<CalamityMod.AverageDamageClass>((167, 167, 190), (199, 199, 209), (133, 133, 173));
		//// HSL: (60, 0.4, 0.5) | (60, 0.3, 0.55) | (60, 0.65, 0.45)
		//API.AddDamageType<CalamityMod.RogueDamageClass>((178, 179, 77), (175, 175, 106), (189, 189, 40));

		//// Adapted from default Melee colors.
		//// Melee RGB: (235, 25, 25) | (170, 0, 0) | (255, 10, 50)
		//(int, int, int) trueMeleeTooltipColor = (230, 20, 20);
		//(int, int, int) trueMeleeDamageColor = (160, 0, 0);
		//(int, int, int) trueMeleeCritDamageColor = (250, 5, 45);
		//API.AddDamageType<CalamityMod.TrueMeleeDamageClass>(trueMeleeTooltipColor, trueMeleeDamageColor, trueMeleeCritDamageColor);
		//API.AddDamageType<CalamityMod.TrueMeleeNoSpeedDamageClass>(trueMeleeTooltipColor, trueMeleeDamageColor, trueMeleeCritDamageColor);
	}
}