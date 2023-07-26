using RandomModCompat.Core;
using System.Linq;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

internal sealed partial class BangarangAPI : ModAPI
{
	protected internal override string ModName => ModNames.Bangarang;

	internal void RegisterBoomerang(int itemId, int[] projectileIds, int boomerangs = 1, BangarangUseCheck useCheck = null)
	{
		// VS really doesn't like nullable delegates, does it?
		// Explicit cast so VS shuts up about "did you mean to call this".
		// Ternary because you can't convert a method group into a nullable object.
		WrappedMod.Call(itemId, projectileIds, boomerangs, useCheck is null ? null : (object)useCheck.Invoke);
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

	internal static BangarangUseCheck MakeTotalSumOfProjectilesCheck(int max, params int[] projectileIds) =>
		(p, i, n) => projectileIds.Select(type => p.ownedProjectileCounts[type]).Sum() < max + n;
}