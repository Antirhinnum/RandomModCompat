using Mask.MaskItem;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class MaskAsymmetricEquipsModule : CrossModModule<AsymmetricEquipsAPI>
{
	public override string CrossModName => ModNames.Mask;

	protected internal override void OnModLoad()
	{
		API.AddFlippedEquipTexture(CrossModName, nameof(电视面具), EquipType.Head);
	}

	protected internal override void PostSetupContent()
	{
		API.AddFlippedEquip<电视面具>(EquipType.Head);
	}
}