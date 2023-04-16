using RandomModCompat.Common.Callers;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content;

[JITWhenModsEnabled(_modName)]
internal sealed class BalloonsExtendedSystem : CrossModHandler
{
	private const string _modName = "BalloonsExtended";
	protected override string ModName => _modName;

	/*
	 * This file adds support for:
	 * - Asymmetric Equips
	 */

	internal override void PostSetupContent()
	{
		AsymmetricEquipsSupport();
	}

	private void AsymmetricEquipsSupport()
	{
		if (!ModWithCalls.TryGetCaller(out AsymmetricEquipsCaller caller))
		{
			return;
		}

		// Every single accessory in this mod has a Balloon equip.
		foreach (ModItem item in CrossMod.GetContent<ModItem>())
		{
			if (item.Item.balloonSlot > -1)
			{
				caller.AddBalloon(item.Type);
			}
		}
	}
}