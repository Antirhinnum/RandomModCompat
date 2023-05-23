using Redemption.Globals;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace RandomModCompat.Common.ExplicitSupport;

[JITWhenModsEnabled(ModNames.Redemption)]
internal sealed class RedemptionListsSupportSystem : ModSystem
{
	private static bool[] _empty = Array.Empty<bool>();

	public override bool IsLoadingEnabled(Mod mod)
	{
		return ModLoader.HasMod(ModNames.Redemption);
	}

	#region Elements

	private static ref bool[] GetSetForElement(RedemptionElement element, RedemptionElementHost host)
	{
		switch ((element, host))
		{
			case (RedemptionElement.Arcane, RedemptionElementHost.Proj):
				return ref ElementID.ProjArcane;

			case (RedemptionElement.Fire, RedemptionElementHost.Proj):
				return ref ElementID.ProjFire;

			case (RedemptionElement.Water, RedemptionElementHost.Proj):
				return ref ElementID.ProjWater;

			case (RedemptionElement.Ice, RedemptionElementHost.Proj):
				return ref ElementID.ProjIce;

			case (RedemptionElement.Earth, RedemptionElementHost.Proj):
				return ref ElementID.ProjEarth;

			case (RedemptionElement.Wind, RedemptionElementHost.Proj):
				return ref ElementID.ProjWind;

			case (RedemptionElement.Thunder, RedemptionElementHost.Proj):
				return ref ElementID.ProjThunder;

			case (RedemptionElement.Holy, RedemptionElementHost.Proj):
				return ref ElementID.ProjHoly;

			case (RedemptionElement.Shadow, RedemptionElementHost.Proj):
				return ref ElementID.ProjShadow;

			case (RedemptionElement.Nature, RedemptionElementHost.Proj):
				return ref ElementID.ProjNature;

			case (RedemptionElement.Poison, RedemptionElementHost.Proj):
				return ref ElementID.ProjPoison;

			case (RedemptionElement.Blood, RedemptionElementHost.Proj):
				return ref ElementID.ProjBlood;

			case (RedemptionElement.Psychic, RedemptionElementHost.Proj):
				return ref ElementID.ProjPsychic;

			case (RedemptionElement.Celestial, RedemptionElementHost.Proj):
				return ref ElementID.ProjCelestial;

			case (RedemptionElement.Explosive, RedemptionElementHost.Proj):
				return ref ElementID.ProjExplosive;

			case (RedemptionElement.Arcane, RedemptionElementHost.Item):
				return ref ElementID.ItemArcane;

			case (RedemptionElement.Fire, RedemptionElementHost.Item):
				return ref ElementID.ItemFire;

			case (RedemptionElement.Water, RedemptionElementHost.Item):
				return ref ElementID.ItemWater;

			case (RedemptionElement.Ice, RedemptionElementHost.Item):
				return ref ElementID.ItemIce;

			case (RedemptionElement.Earth, RedemptionElementHost.Item):
				return ref ElementID.ItemEarth;

			case (RedemptionElement.Wind, RedemptionElementHost.Item):
				return ref ElementID.ItemWind;

			case (RedemptionElement.Thunder, RedemptionElementHost.Item):
				return ref ElementID.ItemThunder;

			case (RedemptionElement.Holy, RedemptionElementHost.Item):
				return ref ElementID.ItemHoly;

			case (RedemptionElement.Shadow, RedemptionElementHost.Item):
				return ref ElementID.ItemShadow;

			case (RedemptionElement.Nature, RedemptionElementHost.Item):
				return ref ElementID.ItemNature;

			case (RedemptionElement.Poison, RedemptionElementHost.Item):
				return ref ElementID.ItemPoison;

			case (RedemptionElement.Blood, RedemptionElementHost.Item):
				return ref ElementID.ItemBlood;

			case (RedemptionElement.Psychic, RedemptionElementHost.Item):
				return ref ElementID.ItemPsychic;

			case (RedemptionElement.Celestial, RedemptionElementHost.Item):
				return ref ElementID.ItemCelestial;

			case (RedemptionElement.Explosive, RedemptionElementHost.Item):
				return ref ElementID.ItemExplosive;

			case (RedemptionElement.Arcane, RedemptionElementHost.NPC):
				return ref ElementID.NPCArcane;

			case (RedemptionElement.Fire, RedemptionElementHost.NPC):
				return ref ElementID.NPCFire;

			case (RedemptionElement.Water, RedemptionElementHost.NPC):
				return ref ElementID.NPCWater;

			case (RedemptionElement.Ice, RedemptionElementHost.NPC):
				return ref ElementID.NPCIce;

			case (RedemptionElement.Earth, RedemptionElementHost.NPC):
				return ref ElementID.NPCEarth;

			case (RedemptionElement.Wind, RedemptionElementHost.NPC):
				return ref ElementID.NPCWind;

			case (RedemptionElement.Thunder, RedemptionElementHost.NPC):
				return ref ElementID.NPCThunder;

			case (RedemptionElement.Holy, RedemptionElementHost.NPC):
				return ref ElementID.NPCHoly;

			case (RedemptionElement.Shadow, RedemptionElementHost.NPC):
				return ref ElementID.NPCShadow;

			case (RedemptionElement.Nature, RedemptionElementHost.NPC):
				return ref ElementID.NPCNature;

			case (RedemptionElement.Poison, RedemptionElementHost.NPC):
				return ref ElementID.NPCPoison;

			case (RedemptionElement.Blood, RedemptionElementHost.NPC):
				return ref ElementID.NPCBlood;

			case (RedemptionElement.Psychic, RedemptionElementHost.NPC):
				return ref ElementID.NPCPsychic;

			case (RedemptionElement.Celestial, RedemptionElementHost.NPC):
				return ref ElementID.NPCCelestial;

			case (RedemptionElement.Explosive, RedemptionElementHost.NPC):
				return ref ElementID.NPCExplosive;

			default:
				return ref _empty;
		}
	}

	internal static void AddItemElements<T>(params RedemptionElement[] elements) where T : ModItem
	{
		int type = ModContent.ItemType<T>();
		foreach (RedemptionElement element in elements)
		{
			GetSetForElement(element, RedemptionElementHost.Item)[type] = true;
		}
	}

	internal static void AddProjElements<T>(params RedemptionElement[] elements) where T : ModProjectile
	{
		int type = ModContent.ProjectileType<T>();
		foreach (RedemptionElement element in elements)
		{
			GetSetForElement(element, RedemptionElementHost.Proj)[type] = true;
		}
	}

	internal static void AddNPCElements<T>(params RedemptionElement[] elements) where T : ModNPC
	{
		int type = ModContent.NPCType<T>();
		foreach (RedemptionElement element in elements)
		{
			GetSetForElement(element, RedemptionElementHost.NPC)[type] = true;
		}
	}

	#endregion Elements

	#region Lists

	private static List<int> GetNPCList(RedemptionNPCList list)
	{
		return list switch
		{
			RedemptionNPCList.HasLostSoul => NPCLists.HasLostSoul,
			RedemptionNPCList.Skeleton => NPCLists.Skeleton,
			RedemptionNPCList.SkeletonHumanoid => NPCLists.SkeletonHumanoid,
			RedemptionNPCList.Humanoid => NPCLists.Humanoid,
			RedemptionNPCList.Undead => NPCLists.Undead,
			RedemptionNPCList.Spirit => NPCLists.Spirit,
			RedemptionNPCList.Plantlike => NPCLists.Plantlike,
			RedemptionNPCList.Demon => NPCLists.Demon,
			RedemptionNPCList.Cold => NPCLists.Cold,
			RedemptionNPCList.Hot => NPCLists.Hot,
			RedemptionNPCList.Wet => NPCLists.Wet,
			RedemptionNPCList.Dragonlike => NPCLists.Dragonlike,
			RedemptionNPCList.Inorganic => NPCLists.Inorganic,
			RedemptionNPCList.Robotic => NPCLists.Robotic,
			RedemptionNPCList.Infected => NPCLists.Infected,
			RedemptionNPCList.Armed => NPCLists.Armed,
			RedemptionNPCList.Hallowed => NPCLists.Hallowed,
			RedemptionNPCList.Dark => NPCLists.Dark,
			RedemptionNPCList.Blood => NPCLists.Blood,
			RedemptionNPCList.IsSlime => NPCLists.IsSlime,
			RedemptionNPCList.IsBunny => NPCLists.IsBunny,
			_ => throw new NotImplementedException()
		};
	}

	private static List<int> GetProjList(RedemptionProjList list)
	{
		return list switch
		{
			RedemptionProjList.IsTechnicallyMelee => ProjectileLists.IsTechnicallyMelee,
			RedemptionProjList.NoElement => ProjectileLists.NoElement,
			_ => throw new NotImplementedException()
		};
	}

	private static List<int> GetItemList(RedemptionItemList list)
	{
		return list switch
		{
			RedemptionItemList.BluntSwing => ItemLists.BluntSwing,
			RedemptionItemList.NoElement => ItemLists.NoElement,
			RedemptionItemList.AlignmentInterest => ItemLists.AlignmentInterest,
			_ => throw new NotImplementedException()
		};
	}

	internal static void AddToNPCLists<T>(params RedemptionNPCList[] lists) where T : ModNPC
	{
		int type = ModContent.NPCType<T>();
		foreach (RedemptionNPCList list in lists)
		{
			GetNPCList(list).Add(type);
		}
	}

	internal static void AddToProjLists<T>(params RedemptionProjList[] lists) where T : ModProjectile
	{
		int type = ModContent.ProjectileType<T>();
		foreach (RedemptionProjList list in lists)
		{
			GetProjList(list).Add(type);
		}
	}

	internal static void AddToItemLists<T>(params RedemptionItemList[] lists) where T : ModItem
	{
		int type = ModContent.ItemType<T>();
		foreach (RedemptionItemList list in lists)
		{
			GetItemList(list).Add(type);
		}
	}

	#endregion Lists
}

internal enum RedemptionElement
{
	Arcane = 1,
	Fire = 2,
	Water = 3,
	Ice = 4,
	Earth = 5,
	Wind = 6,
	Thunder = 7,
	Holy = 8,
	Shadow = 9,
	Nature = 10,
	Poison = 11,
	Blood = 12,
	Psychic = 13,
	Celestial = 14,
	Explosive = 15
}

internal enum RedemptionElementHost
{
	Proj,
	Item,
	NPC
}

internal enum RedemptionNPCList
{
	HasLostSoul,
	Skeleton,
	SkeletonHumanoid,
	Humanoid,
	Undead,
	Spirit,
	Plantlike,
	Demon,
	Cold,
	Hot,
	Wet,
	Dragonlike,
	Inorganic,
	Robotic,
	Infected,
	Armed,
	Hallowed,
	Dark,
	Blood,
	IsSlime,
	IsBunny
}

internal enum RedemptionProjList
{
	IsTechnicallyMelee,
	NoElement
}

internal enum RedemptionItemList
{
	BluntSwing,
	NoElement,
	AlignmentInterest
}