using RandomModCompat.Core;
using System;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

// Call info from: https://github.com/Antirhinnum/UniversalCraft/blob/master/Common/Systems/CallSystem.cs
internal sealed class UniversalCraftAPI : ModAPI
{
	protected internal override string ModName => ModNames.UniversalCraft;

	#region Default

	internal void AddStation(int tileId, Func<bool> condition = null)
	{
		WrappedMod.Call(nameof(AddStation), tileId, condition);
	}

	#endregion Default

	#region Utility

	internal void AddStation<T>(Func<bool> condition = null) where T : ModTile
	{
		AddStation(ModContent.TileType<T>(), condition);
	}

	#endregion Utility
}