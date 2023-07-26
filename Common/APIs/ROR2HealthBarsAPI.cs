using RandomModCompat.Core;
using System.Linq;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

internal sealed class ROR2HealthBarsAPI : ModAPI
{
	protected internal override string ModName => ModNames.ROR2HealthBars;

	internal void HPPool(params int[] npcIds)
	{
		WrappedMod.Call(nameof(HPPool), npcIds.ToList());
	}

	internal void CustomName(int npcId, string localizationKey)
	{
		WrappedMod.Call(nameof(CustomName), npcId, localizationKey);
	}

	internal void CustomName<T>(string localizationKey) where T : ModNPC
	{
		CustomName(ModContent.NPCType<T>(), localizationKey);
	}

	internal void BossDesc(int npcId, string localizationKey)
	{
		WrappedMod.Call(nameof(BossDesc), npcId, localizationKey);
	}

	internal void BossDesc<T>(string localizationKey) where T : ModNPC
	{
		BossDesc(ModContent.NPCType<T>(), localizationKey);
	}

	internal void AddDesc(int npcId, string header, string bossKey)
	{
		BossDesc(npcId, header + "ROR2." + bossKey + ".Description");
	}

	internal void AddDesc<T>(string header, string bossKey) where T : ModNPC
	{
		BossDesc<T>(header + "ROR2." + bossKey + ".Description");
	}
}