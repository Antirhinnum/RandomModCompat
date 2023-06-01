using FishingReborn.Common.Systems;
using FishingReborn.Custom.Interfaces;
using FishingReborn.Custom.Structs;
using RandomModCompat.Utilities;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace RandomModCompat.Common.ExplicitSupport;

// JITWhenModsAreEnabled doesn't work here.
// Apparently, using a generic type constraint counts as extending from a mod. :)
// Thus, "AddPool<T> where T : ICatchPool" breaks unless I have ExtendsFromMod.
// It has to be applied to the class since that's the only thing the attribute can target.
[ExtendsFromMod(ModNames.FishingReborn)]
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
		// Can't use LINQ here because of mysterious ExtendsFromMod issues. :) (I am losing it)
		foreach (ICatchPool pool in _possiblePools)
		{
			if (pool!.GetType() != typeof(T))
			{
				continue;
			}

			pool!.PotentialCatches!.Add(weight);
			return;
		}

		// No pool of type T found, so add one.
		_possiblePools.Add(new T()
		{
			PotentialCatches = new() { weight }
		});
	}

	internal static void AddTreasureData(TreasureData data)
	{
		_possibleTreasure.Add(data);
	}

	internal static bool LavaCondition(Player player, FishingAttempt attempt) => attempt.inLava && attempt.CanFishInLava;
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

[ExtendsFromMod(ModNames.FishingReborn)]
internal sealed class DayCondition : ICatchCondition
{
	bool ICatchCondition.IsConditionMet(FishingAttempt attempt, Projectile bobber) => Main.dayTime;
}