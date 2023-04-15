using RandomModCompat.Core;
using Terraria.GameContent.ItemDropRules;

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
}