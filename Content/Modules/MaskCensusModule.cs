using Mask.MaskItem.MaskMerchant;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ID;

namespace RandomModCompat.Content.Modules;

internal sealed class MaskCensusModule : CrossModModule<CensusAPI>
{
	public override string CrossModName => ModNames.Mask;

	protected internal override void PostSetupContent()
	{
		API.TownNPCCondition<面具商人>($"When the Eye of Cthulhu has been defeated and with [i/s1:{ItemID.GoldCoin}] in your inventory");
	}
}