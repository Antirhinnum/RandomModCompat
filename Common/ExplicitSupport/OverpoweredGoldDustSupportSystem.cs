using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomModCompat.Common.ExplicitSupport;

/// <summary>
/// Support for the mod "Gold Dust Turns Everything Into Gold".
/// </summary>
internal sealed class OverpoweredGoldDustSupportSystem : GlobalProjectile
{
	// TODO: Tile support.
	private const string _modName = "OverpoweredGoldDust";

	private static readonly Dictionary<int, (int goldId, int platinumId)> _npcToGoldNPC = new();
	private static readonly Dictionary<int, (int goldId, int platinumId)> _itemToGoldItem = new();

	private static bool ShouldTransformPlatinum => WorldGen.SavedOreTiers.Gold == TileID.Platinum;

	public override bool IsLoadingEnabled(Mod mod)
	{
		return ModLoader.HasMod(_modName);
	}

	public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
	{
		return entity.type == ModContent.Find<ModProjectile>(_modName, "GoldDust").Type;
	}

	public override void ModifyDamageHitbox(Projectile projectile, ref Rectangle hitbox)
	{
		if (Main.netMode == NetmodeID.MultiplayerClient)
		{
			return;
		}

		for (int i = 0; i < Main.maxNPCs; i++)
		{
			NPC npc = Main.npc[i];
			if (npc.active && npc.getRect().Intersects(hitbox) && _npcToGoldNPC.TryGetValue(npc.type, out (int goldId, int platinumId) ids))
			{
				npc.Transform((ShouldTransformPlatinum && ids.platinumId != -1) ? ids.platinumId : ids.goldId);
			}
		}

		for (int i = 0; i < Main.maxItems; i++)
		{
			Item item = Main.item[i];
			if (item.active && item.getRect().Intersects(hitbox) && _itemToGoldItem.TryGetValue(item.type, out (int goldId, int platinumId) ids))
			{
				item.SetDefaults((ShouldTransformPlatinum && ids.platinumId != -1) ? ids.platinumId : ids.goldId);
			}
		}
	}

	internal static void RegisterItem(int itemId, int goldItemId, int platinumItemId = -1)
	{
		_itemToGoldItem[itemId] = (goldItemId, platinumItemId);
	}

	internal static void RegisterItem<TNormal, TGold>()
		where TNormal : ModItem
		where TGold : ModItem
	{
		RegisterItem(ModContent.ItemType<TNormal>(), ModContent.ItemType<TGold>());
	}

	internal static void RegisterItem<TNormal, TGold, TPlatinum>()
		where TNormal : ModItem
		where TGold : ModItem
		where TPlatinum : ModItem
	{
		RegisterItem(ModContent.ItemType<TNormal>(), ModContent.ItemType<TGold>(), ModContent.ItemType<TPlatinum>());
	}

	internal static void RegisterNPC(int npcId, int goldNpcId, int platinumId = -1)
	{
		_npcToGoldNPC[npcId] = (goldNpcId, platinumId);
	}

	internal static void RegisterNPC<TNormal, TGold>()
		where TNormal : ModNPC
		where TGold : ModNPC
	{
		RegisterNPC(ModContent.NPCType<TNormal>(), ModContent.NPCType<TGold>());
	}

	internal static void RegisterNPC<TNormal, TGold, TPlatinum>()
		where TNormal : ModNPC
		where TGold : ModNPC
		where TPlatinum : ModNPC
	{
		RegisterNPC(ModContent.NPCType<TNormal>(), ModContent.NPCType<TGold>(), ModContent.NPCType<TPlatinum>());
	}
}