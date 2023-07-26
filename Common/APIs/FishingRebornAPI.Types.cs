using FishingReborn.Custom.Interfaces;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

internal sealed partial class FishingRebornAPI
{
	// Dummy version of FishMovementType.
	internal enum FishType
	{
		Mixed,
		Dart,
		Smooth,
		Sinker,
		Floater
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
}