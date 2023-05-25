using FishingReborn.Common.CustomCatchRules.Conditions;
using FishingReborn.Common.CustomCatchRules.Pools;
using FishingReborn.Custom.Enums;
using RandomModCompat.Common;
using RandomModCompat.Common.ExplicitSupport;
using RandomModCompat.Core;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.Geode;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.Thorium;
using ThoriumMod.Items.Vanity;
using FB = RandomModCompat.Common.ExplicitSupport.FishingRebornSupportSystem;

namespace RandomModCompat.Content.ThoriumModSupport;

[JITWhenModsEnabled(_modName, ModNames.FishingReborn)]
internal class ThoriumModFishingRebornSystem : CrossModHandler
{
	private const string _modName = ModNames.ThoriumMod;
	public override string ModName => _modName;

	internal override void PostSetupContent()
	{
		if (!ModContent.GetInstance<ModSupportConfig>().SupportEnabled(_modName, ModNames.FishingReborn))
		{
			return;
		}

		AddNormalFish();
		AddSpecialFish();
		AddTreasures();
	}

	private static void AddNormalFish()
	{
		FB.AddToPool<LavaCatchPool>(new(ModContent.ItemType<MagmaGill>(), 0.67f));
		FB.AddCatchData(ModContent.ItemType<MagmaGill>(), new(50, FishMovementType.Sinker));

		FB.AddToPool<LavaCatchPool>(new(ModContent.ItemType<FlamingCrackGut>(), 0.67f));
		FB.AddCatchData(ModContent.ItemType<FlamingCrackGut>(), new(55, FishMovementType.Sinker));
	}

	private static void AddSpecialFish()
	{
		FB.AddToPool<JungleCatchPool>(new(ModContent.ItemType<RivetingTadpole>(), 0.15f));
		FB.AddCatchData(ModContent.ItemType<RivetingTadpole>(), new(70, FishMovementType.Dart));

		static bool HeartstrikerCondition(FishingAttempt attempt, Projectile proj) => ThoriumConfigServer.Instance.donatorWeapons.toggleAmphibiousWhip;
		FB.AddToPool<JungleCatchPool>(new(ModContent.ItemType<Heartstriker>(), 0.15f, new ArbitraryCondition(HeartstrikerCondition)));
		FB.AddCatchData(ModContent.ItemType<Heartstriker>(), new(70, FishMovementType.Mixed));

		FB.AddToPool<CorruptionCatchPool>(new(ModContent.ItemType<RottenCod>(), 0.15f));
		FB.AddCatchData(ModContent.ItemType<RottenCod>(), new(70, FishMovementType.Floater));

		FB.AddToPool<CrimsonCatchPool>(new(ModContent.ItemType<BrainCoral>(), 0.15f));
		FB.AddCatchData(ModContent.ItemType<BrainCoral>(), new(70, FishMovementType.Floater));

		FB.AddToPool<OceanCatchPool>(new(ModContent.ItemType<LilGuppy>(), 0.65f, new QuestFishCondition(ModContent.ItemType<LilGuppy>())));
		FB.AddCatchData(ModContent.ItemType<LilGuppy>(), new(40, FishMovementType.Dart));

		FB.AddToPool<OceanCatchPool>(new(ModContent.ItemType<HatFish>(), 0.05f, new DayCondition()));
		FB.AddCatchData(ModContent.ItemType<HatFish>(), new(100, FishMovementType.Floater));

		static bool SubterraneanBulbCondition(FishingAttempt attempt, Projectile proj) => ThoriumConfigServer.Instance.donatorOther.toggleSubterraneanBulb;
		FB.AddToPool<OceanCatchPool>(new(ModContent.ItemType<SubterraneanBulb>(), 0.05f, new ArbitraryCondition(SubterraneanBulbCondition)));
		FB.AddCatchData(ModContent.ItemType<SubterraneanBulb>(), new(70, FishMovementType.Sinker));
	}

	private static void AddTreasures()
	{
		static int ScarletCrateSelection(Player player, FishingAttempt attempt) => !Main.hardMode ? ModContent.ItemType<ScarletCrate>() : ModContent.ItemType<SinisterCrate>();

		FB.AddTreasureData(new(
			ScarletCrateSelection,
			0.25f,
			FB.LavaCondition
		));

		static int StrangeCrateSelection(Player player, FishingAttempt attempt) => !Main.hardMode ? ModContent.ItemType<StrangeCrate>() : ModContent.ItemType<WondrousCrate>();

		FB.AddTreasureData(new(
			StrangeCrateSelection,
			0.6f,
			(_, _) => NPC.downedBoss1
		));

		static int AquaticDepthsCrateSelection(Player player, FishingAttempt attempt) => !Main.hardMode ? ModContent.ItemType<AquaticDepthsCrate>() : ModContent.ItemType<AbyssalCrate>();

		FB.AddTreasureData(new(
			AquaticDepthsCrateSelection,
			0.6f,
			(player, attempt) =>
			{
				int tileX = (int)(player.position.X / 16f);
				bool ocean = attempt.waterTilesCount >= 300 && (tileX < WorldGen.beachDistance || tileX > Main.maxTilesX - WorldGen.beachDistance);
				return NPC.downedBoss2 && ocean;
			}
		));

		static bool GeodeGathererCondition(Player player, FishingAttempt attempt) => attempt.playerFishingConditions.PoleItemType == ModContent.ItemType<GeodeGatherer>();
		FB.AddTreasureData(new(
			(_, _) => ItemID.Geode,
			0.5f,
			GeodeGathererCondition
		));
	}
}