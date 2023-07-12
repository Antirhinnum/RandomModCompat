using RandomModCompat.Core;
using System;
using Terraria;
using Terraria.ModLoader;

namespace RandomModCompat.Common.Callers;

internal sealed class BossesAsNPCsCaller : ModWithCalls
{
	internal enum DownedBoss
	{
		Betsy,
		DungeonGuardian,
		DarkMage,
		Ogre,
		GoblinSummoner,
		Dreadnautilus,
		Mothron,
		EoW,
		BoC
	}

	internal enum SpawnableTownNPC
	{
		KingSlime,
		EyeOfCthulhu,
		EaterOfWorlds,
		BrainOfCthulhu,
		QueenBee,
		Skeletron,
		Deerclops,
		WallOfFlesh,
		QueenSlime,
		Destroyer,
		Twins,
		SkeletronPrime,
		Plantera,
		Golem,
		EmpressOfLight,
		DukeFishron,
		Betsy,
		LunaticCultist,
		MoonLord,
		Dreadnautilus,
		Mothron,
		Pumpking,
		IceQueen,
		MartianSaucer,
		TorchGod
	}

	internal enum SellingTownNPC
	{
		KingSlime,
		EyeOfCthulhu,
		EaterOfWorlds,
		BrainOfCthulhu,
		QueenBee,
		Skeletron,
		Deerclops,
		WallOfFlesh,
		QueenSlime,
		TheDestroyer,
		Retinazer,
		Spazmatism,
		SkeletronPrime,
		Plantera,
		Golem,
		EmpressOfLight,
		DukeFishron,
		Betsy,
		LunaticCultist,
		MoonLord,
		Dreadnautilus,
		Mothron,
		Pumpking,
		IceQueen,
		MartianSaucer,
		GoblinTinkerer,
		Pirate
	}

	internal enum PriceType
	{
		DefaultPrice,
		CustomPrice,
		WithDiv,
		WithDivAndMulti
	}

	private static readonly Func<bool> _alwaysTrueCondition = () => true;

	protected internal override string ModName => ModNames.BossesAsNPCs;

	internal bool CheckDownedBoss(DownedBoss boss)
	{
		return (bool)CalledMod.Call("downed" + boss.ToString());
	}

	internal bool DaytimeEoLDefeated()
	{
		return (bool)CalledMod.Call("daytimeEoLDefeated");
	}

	internal bool SellExpertMode()
	{
		return (bool)CalledMod.Call(nameof(SellExpertMode));
	}

	internal bool ShouldSellExpertMode()
	{
		return Main.expertMode || SellExpertMode();
	}

	internal bool SellMasterMode()
	{
		return (bool)CalledMod.Call(nameof(SellMasterMode));
	}

	internal bool ShouldSellMasterMode()
	{
		return Main.masterMode || SellMasterMode();
	}

	internal bool SellExtraItems()
	{
		return (bool)CalledMod.Call(nameof(SellExtraItems));
	}

	internal bool ShopPriceScaling()
	{
		return (bool)CalledMod.Call("shopMulti");
	}

	internal bool TownNPCsCrossModSupport()
	{
		return (bool)CalledMod.Call(nameof(TownNPCsCrossModSupport));
	}

	internal bool CatchNPCs()
	{
		return (bool)CalledMod.Call(nameof(CatchNPCs));
	}

	internal bool AllInOneNPCMode()
	{
		return (bool)CalledMod.Call(nameof(AllInOneNPCMode));
	}

	internal bool GoblinSellInvasionItems()
	{
		return (bool)CalledMod.Call(nameof(GoblinSellInvasionItems));
	}

	internal bool PirateSellInvasionItems()
	{
		return (bool)CalledMod.Call(nameof(PirateSellInvasionItems));
	}

	internal bool CanNPCSpawn(SpawnableTownNPC npc)
	{
		return (bool)CalledMod.Call("CanSpawn", npc.ToString());
	}

	internal bool AddToShop(SellingTownNPC npc, int itemId, Func<bool> condition = null)
	{
		return (bool)CalledMod.Call(nameof(AddToShop), PriceType.DefaultPrice.ToString(), npc.ToString(), itemId, condition ?? _alwaysTrueCondition);
	}

	internal bool AddToShop<T>(SellingTownNPC npc, Func<bool> condition = null) where T : ModItem
	{
		return AddToShop(npc, ModContent.ItemType<T>(), condition);
	}

	internal bool AddToShop(SellingTownNPC npc, int itemId, int customPrice, Func<bool> condition = null)
	{
		return (bool)CalledMod.Call(nameof(AddToShop), PriceType.CustomPrice.ToString(), npc.ToString(), itemId, condition ?? _alwaysTrueCondition, customPrice);
	}

	internal bool AddToShop<T>(SellingTownNPC npc, int customPrice, Func<bool> condition = null) where T : ModItem
	{
		return AddToShop(npc, ModContent.ItemType<T>(), customPrice, condition);
	}

	internal bool AddToShop(SellingTownNPC npc, int itemId, float divisor, Func<bool> condition = null)
	{
		return (bool)CalledMod.Call(nameof(AddToShop), PriceType.WithDiv.ToString(), npc.ToString(), itemId, condition ?? _alwaysTrueCondition, divisor);
	}

	internal bool AddToShop<T>(SellingTownNPC npc, float divisor, Func<bool> condition = null) where T : ModItem
	{
		return AddToShop(npc, ModContent.ItemType<T>(), divisor, condition);
	}

	internal bool AddToShop(SellingTownNPC npc, int itemId, float divisor, float mult, Func<bool> condition = null)
	{
		return (bool)CalledMod.Call(nameof(AddToShop), PriceType.WithDivAndMulti.ToString(), npc.ToString(), itemId, condition ?? _alwaysTrueCondition, divisor, mult);
	}

	internal bool AddToShop<T>(SellingTownNPC npc, float divisor, float mult, Func<bool> condition = null) where T : ModItem
	{
		// divisor * 5f since this overload doesn't apply that by default, so prices get super inflated.
		return AddToShop(npc, ModContent.ItemType<T>(), divisor * 5f, mult, condition);
	}
}