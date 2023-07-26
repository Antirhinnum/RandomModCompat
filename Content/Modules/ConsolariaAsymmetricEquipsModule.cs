using Consolaria.Content.Items.Accessories;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class ConsolariaAsymmetricEquipsModule : CrossModModule<AsymmetricEquipsAPI>
{
	public override string CrossModName => ModNames.Consolaria;

	protected internal override void PostSetupContent()
	{
		// Hidden
		API.AddHiddenEquip<ValentineRing>(EquipType.HandsOn);

		// TODO: Flipped equips for the following:
		// - PhantasmalRobe
		// - AlpineHat
		// - FabulousRibbon (?)
		// - GeorgesTuxedo (Cane)
		// - MonokumaBody, MonokumaHead, MonokumaLegs
		// - MonomiBody, MonomiHead, MonomiLegs
		// - MythicalRobe
		// - ShirenShirt
	}
}