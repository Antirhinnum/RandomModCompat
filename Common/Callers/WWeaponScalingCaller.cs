using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Common.Callers;

// Call info from: https://forums.terraria.org/index.php?threads/w1ks-weapon-scaling.98535/
internal sealed class WWeaponScalingCaller : ModWithCalls
{
	internal enum Tier
	{
		Wooden = 1,
		Iron = 2,
		Gold = 3,
		Evil = 4,
		Jungle = 5,
		Dungeon = 6,
		Molten = 7,
		Adamantite = 8,
		Hallowed = 9,
		Chlorophyte = 10,
		PostPlantera = 11,
		PostGolem = 12,
		Lunar = 13,
		MoonLord = 14
	}

	protected internal override string ModName => "WWeaponScaling";

	#region Default

	internal void AddScaling(int itemId, Tier tier, float scalingFactor = 1f)
	{
		CalledMod.Call(itemId, (int)tier, scalingFactor);
	}

	#endregion Default

	#region Utility

	internal void AddScaling<T>(Tier tier, float scalingFactor = 1f) where T : ModItem
	{
		AddScaling(ModContent.ItemType<T>(), tier, scalingFactor);
	}

	#endregion Utility
}