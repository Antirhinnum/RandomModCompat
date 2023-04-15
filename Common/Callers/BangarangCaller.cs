using RandomModCompat.Core;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomModCompat.Common.Callers;

internal sealed class BangarangCaller : ModWithCalls
{
	protected override string ModName => "Bangarang";

	internal void RegisterBoomerang(int itemId, int[] projectileIds, int boomerangs = 1, Func<Player, Item, int, bool> callback = null)
	{
		CalledMod.Call(itemId, projectileIds, boomerangs, callback);
	}

	internal void RegisterBoomerang<T>(int[] projectileIds, int boomerangs = 1, Func<Player, Item, int, bool> callback = null) where T : ModItem
	{
		RegisterBoomerang(ModContent.ItemType<T>(), projectileIds, boomerangs, callback);
	}

	internal void RegisterSimpleBoomerang<T>(int boomerangs = 1, Func<Player, Item, int, bool> callback = null) where T : ModItem
	{
		int itemId = ModContent.ItemType<T>();
		RegisterBoomerang(itemId, new int[] { ContentSamples.ItemsByType[itemId].shoot }, boomerangs, callback);
	}
}