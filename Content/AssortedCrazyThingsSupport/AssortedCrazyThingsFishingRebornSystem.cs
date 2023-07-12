using AssortedCrazyThings;
using AssortedCrazyThings.Items.Pets;
using FishingReborn.Common.CustomCatchRules.Pools;
using RandomModCompat.Common.ExplicitSupport;
using RandomModCompat.Core;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using FB = RandomModCompat.Common.ExplicitSupport.FishingRebornSupportSystem;

namespace RandomModCompat.Content.AssortedCrazyThingsSupport;

[JITWhenModsEnabled(_modName, ModNames.FishingReborn)]
internal sealed class AssortedCrazyThingsFishingRebornSystem : CrossModHandler
{
	private const string _modName = ModNames.AssortedCrazyThings;
	public override string ModName => _modName;

	internal override void PostSetupContent()
	{
		// ACT has a lot of toggleable content, so make sure the player with CatchFish is actually loaded.
		if (!RandomModCompat.SupportEnabled(_modName, ModNames.FishingReborn) || !CrossMod.TryFind<ModPlayer>(nameof(AssPlayer), out _))
		{
			return;
		}

		FishingRebornSupport();
	}

	private static void FishingRebornSupport()
	{
		if (ModContent.GetInstance<ContentConfig>().OtherPets)
		{
			static bool AnomalocarisCondition(FishingAttempt attempt, Projectile projectile) => attempt.X / 16 < Main.maxTilesX * 0.08f || attempt.X / 16 > Main.maxTilesX * 0.92f;
			FB.AddToPool<GeneralCatchPool>(new(ModContent.ItemType<AnomalocarisItem>(), 0.075f, new ArbitraryCondition(AnomalocarisCondition)));
			FB.AddCatchData(ModContent.ItemType<AnomalocarisItem>(), new(120, FishingReborn.Custom.Enums.FishMovementType.Sinker));
		}
	}
}