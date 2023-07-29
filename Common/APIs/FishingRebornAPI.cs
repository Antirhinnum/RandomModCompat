using FishingReborn.Custom.Interfaces;
using RandomModCompat.Core;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

// FishingReborn uses a lot of custom types that need to be wrapped around or avoided.
// If the types are used in the API, then
[JITWhenModsEnabled(ModNames.FishingReborn)]
internal sealed partial class FishingRebornAPI : ModAPI
{
	protected internal override string ModName => ModNames.FishingReborn;

	#region Utility

	internal static bool LavaCondition(Player player, FishingAttempt attempt) => attempt.inLava && attempt.CanFishInLava;

	#endregion Utility

	#region Custom

	/// <param name="catchPool">Should be an <see cref="ICatchPool"/>.</param>
	internal void AddPool(object catchPool)
	{
		FishingRebornSupportSystem.TryAddPool_Internal(catchPool);
	}

	internal void AddToPool(string poolName, int itemId, float catchWeight, params object[] conditions)
	{
		FishingRebornSupportSystem.AddToPool_Internal(poolName, itemId, catchWeight, conditions);
	}

	internal void AddCatchData(int itemId, int fishDifficulty = 15, FishType type = FishType.Mixed)
	{
		FishingRebornSupportSystem.AddCatchData_Internal(itemId, fishDifficulty, type);
	}

	internal void AddTreasureData(Func<Player, FishingAttempt, int> itemSelection, float treasureWeight, Func<Player, FishingAttempt, bool> catchRequirement)
	{
		FishingRebornSupportSystem.AddTreasureData_Internal(itemSelection, treasureWeight, catchRequirement);
	}

	#endregion Custom
}