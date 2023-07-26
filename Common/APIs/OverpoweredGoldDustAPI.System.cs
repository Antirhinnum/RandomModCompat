using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

internal sealed partial class OverpoweredGoldDustAPI
{
	/// <summary>
	/// Support for the mod "Gold Dust Turns Everything Into Gold".
	/// </summary>
	private sealed class OverpoweredGoldDustSupportSystem : GlobalProjectile
	{
		// TODO: Tile support.
		internal const string ModName = ModNames.OverpoweredGoldDust;

		public static readonly Dictionary<int, (int goldId, int platinumId)> _npcToGoldNPC = new();
		public static readonly Dictionary<int, (int goldId, int platinumId)> _itemToGoldItem = new();

		private static bool ShouldTransformPlatinum => WorldGen.SavedOreTiers.Gold == TileID.Platinum;

		public override bool IsLoadingEnabled(Mod mod)
		{
			return ModLoader.HasMod(ModName);
		}

		public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
		{
			return entity.type == ModContent.Find<ModProjectile>(ModName, "GoldDust").Type;
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
					int oldStack = item.stack;
					item.SetDefaults((ShouldTransformPlatinum && ids.platinumId != -1) ? ids.platinumId : ids.goldId);
					item.stack = oldStack;
				}
			}
		}
	}
}