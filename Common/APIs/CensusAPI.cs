using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

// TODO: API changed in 1.4.4 to use LocalizedText.
// Look into how it autoloads translations and see if there's a way to bypass it.
internal sealed class CensusAPI : ModAPI
{
	protected internal override string ModName => ModNames.Census;

	internal void TownNPCCondition(int npcId, string condition)
	{
		WrappedMod.Call(nameof(TownNPCCondition), npcId, condition);
	}

	internal void TownNPCCondition<T>(string condition) where T : ModNPC
	{
		TownNPCCondition(ModContent.NPCType<T>(), condition);
	}
}