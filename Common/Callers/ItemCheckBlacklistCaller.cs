﻿using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Common.Callers;

internal sealed class ItemCheckBlacklistCaller : ModWithCalls
{
	protected internal override string ModName => ModNames.ItemCheckBlacklist;

	internal void Add(params int[] items)
	{
		CalledMod.Call(nameof(Add), items);
	}

	internal void Add<T>() where T : ModItem
	{
		Add(ModContent.ItemType<T>());
	}
}