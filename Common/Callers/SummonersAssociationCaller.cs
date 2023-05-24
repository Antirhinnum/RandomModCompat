using RandomModCompat.Core;
using System;
using System.Collections.Generic;
using Terraria;

namespace RandomModCompat.Common.Callers;

internal sealed class SummonersAssociationCaller : ModWithCalls
{
	protected internal override string ModName => ModNames.SummonersAssociation;

	internal void AddMinionInfo(int itemId, int buffId, int projectileId, float? slotWeight = null)
	{
		if (slotWeight == null)
		{
			CalledMod.Call(nameof(AddMinionInfo), itemId, buffId, projectileId);
		}
		else
		{
			CalledMod.Call(nameof(AddMinionInfo), itemId, buffId, projectileId, slotWeight);
		}
	}

	internal void AddMinionInfo(int itemId, int buffId, List<int> projectileIds, List<float> slotWeights = null)
	{
		if (slotWeights == null)
		{
			CalledMod.Call(nameof(AddMinionInfo), itemId, buffId, projectileIds);
		}
		else
		{
			CalledMod.Call(nameof(AddMinionInfo), itemId, buffId, projectileIds, slotWeights);
		}
	}

	internal void AddTeleportConditionMinion(int projectileId, Func<Projectile, bool> condition = null)
	{
		if (condition == null)
		{
			CalledMod.Call(nameof(AddTeleportConditionMinion), projectileId);
		}
		else
		{
			CalledMod.Call(nameof(AddTeleportConditionMinion), projectileId, condition);
		}
	}
}