using FishingReborn.Common.Systems;
using FishingReborn.Custom.Enums;
using FishingReborn.Custom.Interfaces;
using FishingReborn.Custom.Structs;
using RandomModCompat.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

internal sealed partial class FishingRebornAPI
{
	[ExtendsFromMod(ModNames.FishingReborn)]
	[JITWhenModsEnabled(ModNames.FishingReborn)]
	private sealed class FishingRebornSupportSystem : ModSystem
	{
		// All of FishingReborn's data fields are private :(
		private static readonly Dictionary<int, CatchData> _catchData = (Dictionary<int, CatchData>)typeof(MinigameFishDataSystem)
			.GetField(nameof(_catchData), ReflectionHelper.AllFlags)
			.GetValue(ModContent.GetInstance<MinigameFishDataSystem>());

		private static readonly List<ICatchPool> _possiblePools = (List<ICatchPool>)typeof(CatchDeterminationSystem)
			.GetField(nameof(_possiblePools), ReflectionHelper.AllFlags)
			.GetValue(ModContent.GetInstance<CatchDeterminationSystem>());

		private static readonly List<TreasureData> _possibleTreasure = (List<TreasureData>)typeof(TreasureDeterminationSystem)
			.GetField(nameof(_possibleTreasure), ReflectionHelper.AllFlags)
			.GetValue(ModContent.GetInstance<TreasureDeterminationSystem>());

		internal static void TryAddPool_Internal(object pool)
		{
			if (pool is ICatchPool catchPool)
			{
				AddPool_Internal(catchPool);
			}
		}

		internal static void AddPool_Internal(ICatchPool pool)
		{
			pool.PotentialCatches ??= new();
			_possiblePools.Add(pool);
		}

		internal static void AddToPool_Internal(string poolName, int itemId, float catchWeight, params object[] conditions)
		{
			// JIT issues if this is a lambda
			static bool CatchConditionFilter(object o) => o is ICatchCondition;
			IEnumerable<ICatchCondition> conditionsCasted = conditions.Where(CatchConditionFilter).Cast<ICatchCondition>();
			CatchWeight weight = new(itemId, catchWeight, conditionsCasted.ToArray());

			foreach (ICatchPool pool in _possiblePools)
			{
				if (pool.GetType().Name == poolName)
				{
					pool!.PotentialCatches.Add(weight);
				}
			}
		}

		internal static void AddCatchData_Internal(int itemId, int fishDifficulty, FishType movementType)
		{
			_catchData[itemId] = new(fishDifficulty, (FishMovementType)movementType);
		}

		internal static void AddTreasureData_Internal(Func<Player, FishingAttempt, int> itemSelection, float treasureWeight, Func<Player, FishingAttempt, bool> catchRequirement)
		{
			_possibleTreasure.Add(new(itemSelection, treasureWeight, catchRequirement));
		}
	}
}