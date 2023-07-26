using RandomModCompat.Core;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

internal sealed class MagicStorageAPI : ModAPI
{
	protected internal override string ModName => ModNames.MagicStorage;

	internal void SetShadowDiamondDropRule(int npcId, IItemDropRule rule)
	{
		WrappedMod.Call("Set Shadow Diamond Drop Rule", npcId, rule);
	}

	internal IItemDropRule GetShadowDiamondDropRule(int normalDrop, int expertDrop = -1)
	{
		return (IItemDropRule)WrappedMod.Call("Get Shadow Diamond Drop Rule", normalDrop, expertDrop);
	}

	internal void RegisterShadowDiamondDrop(int npcType, int normal, int expert = -1)
	{
		SetShadowDiamondDropRule(npcType, GetShadowDiamondDropRule(normal, expert));
	}

	internal void RegisterShadowDiamondDrop<T>(int normal, int expert = -1) where T : ModNPC
	{
		RegisterShadowDiamondDrop(ModContent.NPCType<T>(), normal, expert);
	}

	internal void RegisterShadowDiamondDropNormalOnly<T>(int amount) where T : ModNPC
	{
		IItemDropRule diamondDropRule = GetShadowDiamondDropRule(amount);
		IItemDropRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
		notExpertRule.OnSuccess(diamondDropRule);
		SetShadowDiamondDropRule(ModContent.NPCType<T>(), notExpertRule);
	}
}