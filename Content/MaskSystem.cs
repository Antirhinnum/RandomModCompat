using RandomModCompat.Common.Callers;
using RandomModCompat.Core;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomModCompat.Content;

[JITWhenModsEnabled(_modName)]
internal sealed class MaskSystem : CrossModHandler
{
	private const string _modName = "Mask";
	protected override string ModName => _modName;

	/*
	 * This file adds support for:
	 * - Asymmetric Equips
	 * - Census
	 */

	internal override void OnModLoad()
	{
		AsymmetricEquipsLoadTextures();
	}

	private void AsymmetricEquipsLoadTextures()
	{
		if (!ModWithCalls.TryGetCaller(out AsymmetricEquipsCaller caller))
		{
			return;
		}

		caller.AddFlippedEquipTexture(_modName, nameof(Mask.MaskItem.电视面具), EquipType.Head);
	}

	internal override void PostSetupContent()
	{
		AsymmetricEquipsSupport();
		CensusSupport();
	}

	private static void AsymmetricEquipsSupport()
	{
		if (!ModWithCalls.TryGetCaller(out AsymmetricEquipsCaller caller))
		{
			return;
		}

		caller.AddFlippedEquip<Mask.MaskItem.电视面具>(EquipType.Head);
	}

	private static void CensusSupport()
	{
		if (!ModWithCalls.TryGetCaller(out CensusCaller caller))
		{
			return;
		}

		caller.TownNPCCondition<Mask.MaskItem.MaskMerchant.面具商人>($"When the Eye of Cthulhu has been defeated and with [i/s1:{ItemID.GoldCoin}] in your inventory");
	}
}