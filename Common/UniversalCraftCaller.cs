using RandomModCompat.Core;
using System;
using Terraria.ModLoader;

namespace RandomModCompat.Common;

internal sealed class UniversalCraftCaller : ModWithCalls
{
	protected override string ModName => "UniversalCraft";

	internal void AddStation(int tileId, Func<bool> condition = null)
	{
		CalledMod.Call(nameof(AddStation), tileId, condition);
	}

	internal void AddStation<T>(Func<bool> condition = null) where T : ModTile
	{
		AddStation(ModContent.TileType<T>(), condition);
	}
}