using RandomModCompat.Common.Configs;
using RandomModCompat.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace RandomModCompat.Common;

/// <summary>
/// A simple checker for mod support.
/// </summary>
internal sealed class Supportinator : ModSystem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return Debugger.IsAttached || ModContent.GetInstance<RandomModCompatConfig>().SupportinatorEnabled;
	}

	// Runs after PostSetupContent().
	public override void PostSetupRecipes()
	{
		static bool IsBoss(NPC npc)
		{
			return npc.boss || NPCID.Sets.DangerThatPreventsOtherDangers[npc.type] || NPCID.Sets.ShouldBeCountedAsBoss[npc.type] || npc.type is NPCID.EaterofWorldsHead or NPCID.EaterofWorldsBody or NPCID.EaterofWorldsTail;
		}

		static bool ItemMightBeAsymmetricEquip(ModItem item)
		{
			HashSet<EquipType> possiblyAsymmetricEquips = new()
			{
				EquipType.HandsOn,
				EquipType.HandsOff,
				EquipType.Waist,
				EquipType.Face,
				EquipType.Balloon,
			};

			return item.GetType().GetCustomAttribute<AutoloadEquip>() is AutoloadEquip equips
				&& equips.equipTypes.Any(possiblyAsymmetricEquips.Contains);
		}

		static bool ItemMightBeBoomerang(Item item)
		{
			return item.shoot != ProjectileID.None && ContentSamples.ProjectilesByType[item.shoot].aiStyle == ProjAIStyleID.Boomerang;
		}

		static IEnumerable<int> ItemDropsFromNPC(Func<int, bool> condition)
		{
			IEnumerable<int> npcIds = Enumerable.Range(NPCID.NegativeIDCount + 1, NPCLoader.NPCCount - NPCID.NegativeIDCount - 1) // All NPC IDs
				.Where(condition.Invoke); // Filtered

			IEnumerable<IItemDropRule> rules = npcIds.SelectMany(id => Main.ItemDropsDB.GetRulesForNPCID(id, includeGlobalDrops: false));
			List<DropRateInfo> list = new();
			DropRateInfoChainFeed ratesInfo = new(1f);
			foreach (IItemDropRule item in rules)
			{
				item.ReportDroprates(list, ratesInfo);
			}

			return list.Select(info => info.itemId).Distinct();
		}

		GenericChecker<ModItem>("asymmetric equips", ItemMightBeAsymmetricEquip, ModNames.AsymmetricEquips);
		GenericChecker<ModItem>("boomerangs", i => ItemMightBeBoomerang(i.Item), ModNames.Bangarang);
		GenericChecker<ModNPC>("bosses", n => IsBoss(n.NPC), ModNames.MagicStorage, ModNames.ROR2HealthBars);
		GenericChecker<ModTile>("crafting stations",t => Main.recipe.Any(r => r.HasTile(t)), ModNames.UniversalCraft);
		GenericChecker<DamageClass>("damage classes", null, ModNames.ColoredDamageTypes, ModNames.levelplus);
		GenericChecker<ModPlayer>("fishing methods", p => LoaderUtils.HasOverride(p.GetType(), typeof(ModPlayer).GetMethod(nameof(ModPlayer.CatchFish))), ModNames.FishingReborn);
		GenericChecker<ModItem>("flails", i => i.Item.shoot != ProjectileID.None && ContentSamples.ProjectilesByType[i.Item.shoot].aiStyle == ProjAIStyleID.Flail, ModNames.ThoriumMod);
		GenericChecker<ModItem>("golden critters", i => Main.recipe.Where(r => r.createItem.type == ItemID.GoldenDelight).Any(r => r.requiredItem.Any(ri => ri.type == i.Type)), ModNames.OverpoweredGoldDust);
		GenericChecker<ModItem>("martian items", i => ItemDropsFromNPC(id => NPC.GetNPCInvasionGroup(id) == InvasionID.MartianMadness).Contains(i.Type), ModNames.ThoriumMod);
		GenericChecker<ModProjectile>("partial minions", p => p.Projectile.minionSlots > 0f && p.Projectile.minionSlots != 1f, ModNames.SummonersAssociation);
		GenericChecker<ModTile>("torches", t => TileID.Sets.Torch[t.Type], ModNames.ImprovedTorches);
		GenericChecker<ModNPC>("town NPCs", n => n.NPC.isLikeATownNPC && !n.TownNPCStayingHomeless, ModNames.Census, ModNames.DialogueTweak);
		GenericChecker<ModItem>("vanilla boss drops", i => ItemDropsFromNPC(id => id < NPCID.Count && IsBoss(ContentSamples.NpcsByNetId[id])).Contains(i.Type), ModNames.BossesAsNPCs);

	}

	private void GenericChecker<T>(string checkDescription, Predicate<T> condition = null, params string[] supportMods) where T : ModType
	{
		Mod.Logger.Debug($"Checking {checkDescription}...");
		foreach (Mod mod in ModLoader.Mods)
		{
			string modName = mod.Name;
			if (supportMods.All(m => RandomModCompat.SupportEnabled(modName, m, ignoreEnabledMod: true)))
			{
				continue;
			}

			bool foundAny = false;
			foreach (T t in mod.GetContent<T>().Where(o => condition?.Invoke(o) ?? true))
			{
				if (!foundAny)
				{
					foundAny = true; // This line only runs if any content satisfies the condition.
					Mod.Logger.Debug($"\tMod name: {modName}");
				}

				Mod.Logger.Debug($"\t\tHit! {typeof(T).Name}: \"{t.Name}\"");
			}

			if (foundAny)
			{
				foreach (string supportMod in supportMods)
				{
					if (!RandomModCompat.SupportEnabled(modName, supportMod, ignoreEnabledMod: true))
					{
						Mod.Logger.Debug($"\t\tMissing {supportMod} support!");
					}
				}
			}
		}
	}
}