using RandomModCompat.Core;
using System;
using System.Collections.Generic;
using Terraria;

namespace RandomModCompat.Common.APIs;

internal sealed class SummonersAssociationAPI : ModAPI
{
	protected internal override string ModName => ModNames.SummonersAssociation;

	internal void AddMinionInfo(int itemId, int buffId, int projectileId, float? slotWeight = null)
	{
		if (slotWeight == null)
		{
			WrappedMod.Call(nameof(AddMinionInfo), itemId, buffId, projectileId);
		}
		else
		{
			WrappedMod.Call(nameof(AddMinionInfo), itemId, buffId, projectileId, slotWeight);
		}
	}

	internal void AddMinionInfo(int itemId, int buffId, List<int> projectileIds, List<float> slotWeights = null)
	{
		if (slotWeights == null)
		{
			WrappedMod.Call(nameof(AddMinionInfo), itemId, buffId, projectileIds);
		}
		else
		{
			WrappedMod.Call(nameof(AddMinionInfo), itemId, buffId, projectileIds, slotWeights);
		}
	}

	internal void AddTeleportConditionMinion(int projectileId, Func<Projectile, bool> condition = null)
	{
		if (condition == null)
		{
			WrappedMod.Call(nameof(AddTeleportConditionMinion), projectileId);
		}
		else
		{
			WrappedMod.Call(nameof(AddTeleportConditionMinion), projectileId, condition);
		}
	}
}