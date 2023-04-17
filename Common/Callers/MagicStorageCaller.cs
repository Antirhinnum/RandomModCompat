using RandomModCompat.Core;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace RandomModCompat.Common.Callers;

internal sealed class MagicStorageCaller : ModWithCalls
{
	protected override string ModName => "MagicStorage";

	internal void SetShadowDiamondDropRule(int npcId, IItemDropRule rule)
	{
		CalledMod.Call("Set Shadow Diamond Drop Rule", npcId, rule);
	}

	internal IItemDropRule GetShadowDiamondDropRule(int normalDrop, int expertDrop = -1)
	{
		return (IItemDropRule)CalledMod.Call("Get Shadow Diamond Drop Rule", normalDrop, expertDrop);
	}

	internal void RegisterShadowDiamondDrop<T>(int normal, int expert = -1) where T : ModNPC
	{
		SetShadowDiamondDropRule(ModContent.NPCType<T>(), GetShadowDiamondDropRule(normal, expert));
	}

	internal void RegisterShadowDiamondDropNormalOnly<T>(int amount) where T : ModNPC
	{
		IItemDropRule diamondDropRule = GetShadowDiamondDropRule(amount);
		IItemDropRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
		notExpertRule.OnSuccess(diamondDropRule);
		SetShadowDiamondDropRule(ModContent.NPCType<T>(), notExpertRule);
	}
}