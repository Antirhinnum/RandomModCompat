using RandomModCompat.Core;
using System;
using Terraria;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

internal sealed class RescueFairiesAPI : ModAPI
{
	protected internal override string ModName => ModNames.RescueFairies;

	internal void AddTrackingCondition(int npcId)
	{
		WrappedMod.Call(nameof(AddTrackingCondition), npcId);
	}

	internal void AddTrackingCondition<T>() where T : ModNPC
	{
		AddTrackingCondition(ModContent.NPCType<T>());
	}

	internal void AddTrackingCondition(Func<NPC, bool> condition)
	{
		WrappedMod.Call(nameof(AddTrackingCondition), condition);
	}
}