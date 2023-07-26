using RandomModCompat.Core;

namespace RandomModCompat.Common.APIs;

internal sealed partial class RedemptionAPI : ModAPI
{
	internal enum Element
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

	internal enum NPCList
	{
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
		Slime
	}
}