using OrchidMod.Shaman.Accessories;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class OrchidModAsymmetricEquipsModule : CrossModModule<AsymmetricEquipsAPI>
{
	public override string CrossModName => ModNames.OrchidMod;

	protected internal override void PostSetupContent()
	{
		API.AddHiddenEquip<AmberIdol>(EquipType.Waist);
		API.AddHiddenEquip<AmethystIdol>(EquipType.Waist);
		API.AddHiddenEquip<DiamondIdol>(EquipType.Waist);
		API.AddHiddenEquip<EmeraldIdol>(EquipType.Waist);
		API.AddHiddenEquip<PrismaticIdol>(EquipType.Waist);
		API.AddHiddenEquip<RubyIdol>(EquipType.Waist);
		API.AddHiddenEquip<RuneOfHorus>(EquipType.Waist);
		API.AddHiddenEquip<SapphireIdol>(EquipType.Waist);
		API.AddHiddenEquip<TopazIdol>(EquipType.Waist);

		API.AddBalloon<MourningTorch>();
		API.AddBalloon<SunPriestTorch>();

		// TODO:
		// - Flipped sprites: Sun Priest Satchel; Meteor Toolbelt
		// - Mysteriously not showing up: Molten Ring; Heavy Braclet
	}
}