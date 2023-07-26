using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

// Call info from: https://forums.terraria.org/index.php?threads/w1ks-weapon-scaling.98535/
internal sealed partial class WWeaponScalingAPI : ModAPI
{
	protected internal override string ModName => ModNames.WWeaponScaling;

	#region Default

	internal void AddScaling(int itemId, Tier tier, float scalingFactor = 1f)
	{
		WrappedMod.Call(itemId, (int)tier, scalingFactor);
	}

	#endregion Default

	#region Utility

	internal void AddScaling<T>(Tier tier, float scalingFactor = 1f) where T : ModItem
	{
		AddScaling(ModContent.ItemType<T>(), tier, scalingFactor);
	}

	#endregion Utility
}