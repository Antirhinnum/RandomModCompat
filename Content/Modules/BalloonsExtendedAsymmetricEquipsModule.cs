using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class BalloonsExtendedAsymmetricEquipsModule : CrossModModule<AsymmetricEquipsAPI>
{
	public override string CrossModName => ModNames.BalloonsExtended;

	protected internal override void PostSetupContent()
	{
		// Every single accessory in this mod has a Balloon equip.
		foreach (ModItem item in CrossMod.GetContent<ModItem>())
		{
			if (item.Item.balloonSlot > -1)
			{
				API.AddBalloon(item.Type);
			}
		}
	}
}