using CalamityMod.Items.Accessories;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class CalamityModAsymmetricEquipsModule : CrossModModule<AsymmetricEquipsAPI>
{
	public override string CrossModName => ModNames.CalamityMod;

	protected internal override void PostSetupContent()
	{
		// Hidden
		API.AddHiddenEquip<DepthCharm>(EquipType.Waist);

		// Gloves
		API.AddGlove<BloodstainedGlove>();
		API.AddGlove<ElectriciansGlove>();
		API.AddGlove<ElementalGauntlet>();
		API.AddGlove<FilthyGlove>();
		API.AddGlove<GloveOfPrecision>();
		API.AddGlove<GloveOfRecklessness>();
	}
}