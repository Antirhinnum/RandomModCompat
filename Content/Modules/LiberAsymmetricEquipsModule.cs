using Liber.Content.Items.Accessories;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class LiberAsymmetricEquipsModule : CrossModModule<AsymmetricEquipsAPI>
{
	public override string CrossModName => ModNames.Liber;

	protected internal override void OnModLoad()
	{
		API.AddFlippedEquipTexture(CrossModName, nameof(VikingShield), EquipType.Shield);
	}

	protected internal override void PostSetupContent()
	{
		// Hidden
		API.AddHiddenEquip<SkullRing>(EquipType.HandsOn);
		API.AddHiddenEquip<WristBand>(EquipType.HandsOn);

		// Glove
		API.AddGlove<Gauntlet>();

		// Flipped
		API.AddFlippedEquip<VikingShield>(EquipType.Shield);
	}
}