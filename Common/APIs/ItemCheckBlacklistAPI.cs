using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

internal sealed class ItemCheckBlacklistAPI : ModAPI
{
	protected internal override string ModName => ModNames.ItemCheckBlacklist;

	internal void Add(params int[] items)
	{
		WrappedMod.Call(nameof(Add), items);
	}

	internal void Add<T>() where T : ModItem
	{
		Add(ModContent.ItemType<T>());
	}
}