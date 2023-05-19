using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Common.Callers;

internal sealed class CensusCaller : ModWithCalls
{
	protected internal override string ModName => ModNames.Census;

	internal void TownNPCCondition(int npcId, string condition)
	{
		CalledMod.Call(nameof(TownNPCCondition), npcId, condition);
	}

	internal void TownNPCCondition<T>(string condition) where T : ModNPC
	{
		TownNPCCondition(ModContent.NPCType<T>(), condition);
	}
}