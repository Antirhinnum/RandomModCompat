using RandomModCompat.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomModCompat.Common.Callers;

internal sealed class BangarangCaller : ModWithCalls
{
	internal delegate bool BangarangUseCheck(Player player, Item item, int extraBoomerangs);

	protected override string ModName => "Bangarang";

	internal void RegisterBoomerang(int itemId, int[] projectileIds, int boomerangs = 1, BangarangUseCheck useCheck = null)
	{
		// VS really doesn't like nullable delegates, does it?
		// Explicit cast so VS shuts up about "did you mean to call this".
		// Ternary because you can't convert a method group into a nullable object.
		CalledMod.Call(itemId, projectileIds, boomerangs, useCheck is null ? null : (object)useCheck.Invoke);
	}

	internal void RegisterBoomerang<T>(int[] projectileIds, int boomerangs = 1, BangarangUseCheck useCheck = null) where T : ModItem
	{
		RegisterBoomerang(ModContent.ItemType<T>(), projectileIds, boomerangs, useCheck);
	}

	internal void RegisterSimpleBoomerang<T>(int boomerangs = 1, BangarangUseCheck useCheck = null) where T : ModItem
	{
		int itemId = ModContent.ItemType<T>();
		RegisterBoomerang(itemId, new int[] { ContentSamples.ItemsByType[itemId].shoot }, boomerangs, useCheck);
	}

	internal static BangarangUseCheck MakeSimpleHeldItemStackCheck() => (p, i, n) => p.ownedProjectileCounts[i.shoot] < i.stack + n;
}