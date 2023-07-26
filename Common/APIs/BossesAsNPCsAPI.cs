using RandomModCompat.Core;
using System;
using Terraria;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

internal sealed partial class BossesAsNPCsAPI : ModAPI
{
	private static readonly Func<bool> _alwaysTrueCondition = () => true;

	protected internal override string ModName => ModNames.BossesAsNPCs;

	internal bool CheckDownedBoss(DownedBoss boss)
	{
		return (bool)WrappedMod.Call("downed" + boss.ToString());
	}

	internal bool DaytimeEoLDefeated()
	{
		return (bool)WrappedMod.Call("daytimeEoLDefeated");
	}

	internal bool SellExpertMode()
	{
		return (bool)WrappedMod.Call(nameof(SellExpertMode));
	}

	internal bool ShouldSellExpertMode()
	{
		return Main.expertMode || SellExpertMode();
	}

	internal bool SellMasterMode()
	{
		return (bool)WrappedMod.Call(nameof(SellMasterMode));
	}

	internal bool ShouldSellMasterMode()
	{
		return Main.masterMode || SellMasterMode();
	}

	internal bool SellExtraItems()
	{
		return (bool)WrappedMod.Call(nameof(SellExtraItems));
	}

	internal bool ShopPriceScaling()
	{
		return (bool)WrappedMod.Call("shopMulti");
	}

	internal bool TownNPCsCrossModSupport()
	{
		return (bool)WrappedMod.Call(nameof(TownNPCsCrossModSupport));
	}

	internal bool CatchNPCs()
	{
		return (bool)WrappedMod.Call(nameof(CatchNPCs));
	}

	internal bool AllInOneNPCMode()
	{
		return (bool)WrappedMod.Call(nameof(AllInOneNPCMode));
	}

	internal bool GoblinSellInvasionItems()
	{
		return (bool)WrappedMod.Call(nameof(GoblinSellInvasionItems));
	}

	internal bool PirateSellInvasionItems()
	{
		return (bool)WrappedMod.Call(nameof(PirateSellInvasionItems));
	}

	internal bool CanNPCSpawn(SpawnableTownNPC npc)
	{
		return (bool)WrappedMod.Call("CanSpawn", npc.ToString());
	}

	internal bool AddToShop(SellingTownNPC npc, int itemId, Func<bool> condition = null)
	{
		return (bool)WrappedMod.Call(nameof(AddToShop), PriceType.DefaultPrice.ToString(), npc.ToString(), itemId, condition ?? _alwaysTrueCondition);
	}

	internal bool AddToShop<T>(SellingTownNPC npc, Func<bool> condition = null) where T : ModItem
	{
		return AddToShop(npc, ModContent.ItemType<T>(), condition);
	}

	internal bool AddToShop(SellingTownNPC npc, int itemId, int customPrice, Func<bool> condition = null)
	{
		return (bool)WrappedMod.Call(nameof(AddToShop), PriceType.CustomPrice.ToString(), npc.ToString(), itemId, condition ?? _alwaysTrueCondition, customPrice);
	}

	internal bool AddToShop<T>(SellingTownNPC npc, int customPrice, Func<bool> condition = null) where T : ModItem
	{
		return AddToShop(npc, ModContent.ItemType<T>(), customPrice, condition);
	}

	internal bool AddToShop(SellingTownNPC npc, int itemId, float divisor, Func<bool> condition = null)
	{
		return (bool)WrappedMod.Call(nameof(AddToShop), PriceType.WithDiv.ToString(), npc.ToString(), itemId, condition ?? _alwaysTrueCondition, divisor);
	}

	internal bool AddToShop<T>(SellingTownNPC npc, float divisor, Func<bool> condition = null) where T : ModItem
	{
		return AddToShop(npc, ModContent.ItemType<T>(), divisor, condition);
	}

	internal bool AddToShop(SellingTownNPC npc, int itemId, float divisor, float mult, Func<bool> condition = null)
	{
		return (bool)WrappedMod.Call(nameof(AddToShop), PriceType.WithDivAndMulti.ToString(), npc.ToString(), itemId, condition ?? _alwaysTrueCondition, divisor, mult);
	}

	internal bool AddToShop<T>(SellingTownNPC npc, float divisor, float mult, Func<bool> condition = null) where T : ModItem
	{
		// divisor * 5f since this overload doesn't apply that by default, so prices get super inflated.
		return AddToShop(npc, ModContent.ItemType<T>(), divisor * 5f, mult, condition);
	}
}