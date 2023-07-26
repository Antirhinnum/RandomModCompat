using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

internal sealed partial class OverpoweredGoldDustAPI : ModAPI
{
	protected internal override string ModName => ModNames.OverpoweredGoldDust;

	#region Custom

	internal void RegisterItem(int itemId, int goldItemId, int platinumItemId = -1)
	{
		OverpoweredGoldDustSupportSystem._itemToGoldItem[itemId] = (goldItemId, platinumItemId);
	}

	internal void RegisterItem<TNormal, TGold>()
		where TNormal : ModItem
		where TGold : ModItem
	{
		RegisterItem(ModContent.ItemType<TNormal>(), ModContent.ItemType<TGold>());
	}

	internal void RegisterItem<TNormal, TGold, TPlatinum>()
		where TNormal : ModItem
		where TGold : ModItem
		where TPlatinum : ModItem
	{
		RegisterItem(ModContent.ItemType<TNormal>(), ModContent.ItemType<TGold>(), ModContent.ItemType<TPlatinum>());
	}

	internal void RegisterNPC(int npcId, int goldNpcId, int platinumId = -1)
	{
		OverpoweredGoldDustSupportSystem._npcToGoldNPC[npcId] = (goldNpcId, platinumId);
	}

	internal void RegisterNPC<TNormal, TGold>()
		where TNormal : ModNPC
		where TGold : ModNPC
	{
		RegisterNPC(ModContent.NPCType<TNormal>(), ModContent.NPCType<TGold>());
	}

	internal void RegisterNPC<TNormal, TGold, TPlatinum>()
		where TNormal : ModNPC
		where TGold : ModNPC
		where TPlatinum : ModNPC
	{
		RegisterNPC(ModContent.NPCType<TNormal>(), ModContent.NPCType<TGold>(), ModContent.NPCType<TPlatinum>());
	}

	#endregion Custom
}