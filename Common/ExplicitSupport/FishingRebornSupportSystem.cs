using FishingReborn.Common.Systems;
using FishingReborn.Custom.Interfaces;
using FishingReborn.Custom.Structs;
using RandomModCompat.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace RandomModCompat.Common.ExplicitSupport;

[JITWhenModsEnabled(ModNames.FishingReborn)]
internal sealed class FishingRebornSupportSystem : ModSystem
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

	public override bool IsLoadingEnabled(Mod mod)
	{
		return ModLoader.HasMod(ModNames.FishingReborn);
	}

	internal static void AddCatchData(int itemId, CatchData data)
	{
		_catchData[itemId] = data;
	}

	internal static void AddPool<T>()
		where T : ICatchPool, new()
	{
		_possiblePools.Add(new T()
		{
			PotentialCatches = new()
		});
	}

	internal static void AddToPool<T>(CatchWeight weight)
		where T : ICatchPool, new()
	{
		ICatchPool pool = _possiblePools.FirstOrDefault(p => p.GetType() == typeof(T));
		if (pool == null)
		{
			pool = new T()
			{
				PotentialCatches = new()
			};
			_possiblePools.Add(pool);
		}

		pool!.PotentialCatches.Add(weight);
	}

	internal static void AddTreasureData(TreasureData data)
	{
		_possibleTreasure.Add(data);
	}
}

[ExtendsFromMod(ModNames.FishingReborn)]
internal sealed class ArbitraryCondition : ICatchCondition
{
	private readonly Func<FishingAttempt, Projectile, bool> _condition;

	internal ArbitraryCondition(Func<FishingAttempt, Projectile, bool> condition)
	{
		_condition = condition;
	}

	bool ICatchCondition.IsConditionMet(FishingAttempt attempt, Projectile bobber) => _condition(attempt, bobber);
}