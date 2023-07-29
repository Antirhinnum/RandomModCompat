using FishingReborn.Common.CustomCatchRules.Conditions;
using FishingReborn.Custom.Interfaces;
using FishingReborn.Custom.Structs;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using TheConfectionRebirth.Biomes;
using TheConfectionRebirth.Items.Placeable;

namespace RandomModCompat.Content.Modules;

internal sealed class TheConfectionRebirthFishingRebornModule : CrossModModule<FishingRebornAPI>
{
	[ExtendsFromMod(ModNames.FishingReborn)]
	private sealed class ConfectionCatchPool : ICatchPool
	{
		List<CatchWeight> ICatchPool.PotentialCatches { get; set; }
		uint ICatchPool.Priority => 2u;
		bool ICatchPool.CompleteOverride => false;

		bool ICatchPool.IsPoolActive(FishingAttempt attempt, Projectile bobber)
		{
			return Main.player[bobber.owner].InModBiome<ConfectionBiomeSurface>() && !attempt.inLava && !attempt.inHoney;
		}
	}

	public override string CrossModName => ModNames.TheConfectionRebirth;

	protected internal override void PostSetupContent()
	{
		API.AddPool(new ConfectionCatchPool());

		// Internal type :(
		int gummyStaffType = CrossMod.Find<ModItem>("GummyStaff").Type;
		API.AddToPool(nameof(ConfectionCatchPool), gummyStaffType, 0.15f, new HardmodeCondition());
		API.AddCatchData(gummyStaffType, 80, FishingRebornAPI.FishType.Dart);

		static int ConfectionCrateSelection(Player player, FishingAttempt attempt) => !Main.hardMode ? ModContent.ItemType<BananaSplitCrate>() : ModContent.ItemType<ConfectionCrate>();
		static bool InConfectionCondition(Player player, FishingAttempt attempt) => player.InModBiome<ConfectionBiomeSurface>() && !attempt.inLava && !attempt.inHoney;

		API.AddTreasureData(
			ConfectionCrateSelection,
			0.25f,
			InConfectionCondition
		);
	}
}