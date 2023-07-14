using ClickerClass.Items.Accessories;
using FishingReborn.Common.CustomCatchRules.Pools;
using RandomModCompat.Core;
using Terraria.ModLoader;
using FB = RandomModCompat.Common.ExplicitSupport.FishingRebornSupportSystem;

namespace RandomModCompat.Content.ClickerClassSupport;

[JITWhenModsEnabled(_modName, ModNames.FishingReborn)]
internal sealed class ClickerClassFishingRebornSystem : CrossModHandler
{
	private const string _modName = ModNames.ClickerClass;
	public override string ModName => _modName;

	internal override void PostSetupContent()
	{
		if (!RandomModCompat.SupportEnabled(_modName, ModNames.FishingReborn))
		{
			return;
		}

		AddFish();
	}

	private static void AddFish()
	{
		FB.AddToPool<LavaCatchPool>(new(ModContent.ItemType<HotKeychain>(), 0.05f));
	}
}