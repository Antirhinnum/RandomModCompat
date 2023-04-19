using RandomModCompat.Core;
using System.Linq;
using Terraria.ModLoader;

namespace RandomModCompat.Common.Callers;

internal sealed class ROR2HealthBarsCaller : ModWithCalls
{
	protected override string ModName => "ROR2HealthBars";

	internal void HPPool(params int[] npcIds)
	{
		CalledMod.Call(nameof(HPPool), npcIds.ToList());
	}

	internal void CustomName(int npcId, string localizationKey)
	{
		CalledMod.Call(nameof(CustomName), npcId, localizationKey);
	}

	internal void CustomName<T>(string localizationKey) where T : ModNPC
	{
		CustomName(ModContent.NPCType<T>(), localizationKey);
	}

	internal void BossDesc(int npcId, string localizationKey)
	{
		CalledMod.Call(nameof(BossDesc), npcId, localizationKey);
	}

	internal void BossDesc<T>(string localizationKey) where T : ModNPC
	{
		BossDesc(ModContent.NPCType<T>(), localizationKey);
	}

	internal void AddDesc<T>(string header, string bossKey) where T : ModNPC
	{
		BossDesc<T>(header + "ROR2." + bossKey + ".Description");
	}
}