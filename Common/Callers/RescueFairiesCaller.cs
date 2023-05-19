using RandomModCompat.Core;
using System;
using Terraria;
using Terraria.ModLoader;

namespace RandomModCompat.Common.Callers;

internal sealed class RescueFairiesCaller : ModWithCalls
{
	protected internal override string ModName => ModNames.RescueFairies;

	internal void AddTrackingCondition(int npcId)
	{
		CalledMod.Call(nameof(AddTrackingCondition), npcId);
	}

	internal void AddTrackingCondition<T>() where T : ModNPC
	{
		AddTrackingCondition(ModContent.NPCType<T>());
	}

	internal void AddTrackingCondition(Func<NPC, bool> condition)
	{
		CalledMod.Call(nameof(AddTrackingCondition), condition);
	}
}