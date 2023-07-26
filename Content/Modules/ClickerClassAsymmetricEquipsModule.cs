using ClickerClass.Items.Accessories;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class ClickerClassAsymmetricEquipsModule : CrossModModule<AsymmetricEquipsAPI>
{
	public override string CrossModName => ModNames.ClickerClass;

	protected internal override void PostSetupContent()
	{
		API.AddHiddenEquip<AimAssistModule>(EquipType.Face);
		API.AddHiddenEquip<IcePack>(EquipType.Waist);
		API.AddHiddenEquip<Milk>(EquipType.Waist);
		API.AddHiddenEquip<Soda>(EquipType.Waist);

		// TODO: Need flipped sprites for:
		// - Gamer Crate
		// - All three Clicking Gloves (Normal, Ancient, Regal) [Need offhand]
	}
}