using FishingReborn.Common.CustomCatchRules.Conditions;
using RandomModCompat.Common.APIs;
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

namespace RandomModCompat.Content.Modules;

internal sealed class ThoriumModFishingRebornModule : CrossModModule<FishingRebornAPI>
{
	public override string CrossModName => ModNames.ThoriumMod;

	protected internal override void PostSetupContent()
	{
		#region Fish

		API.AddToPool("LavaCatchPool", ModContent.ItemType<MagmaGill>(), 0.67f);
		API.AddCatchData(ModContent.ItemType<MagmaGill>(), 50, FishingRebornAPI.FishType.Sinker);

		API.AddToPool("LavaCatchPool", ModContent.ItemType<FlamingCrackGut>(), 0.67f);
		API.AddCatchData(ModContent.ItemType<FlamingCrackGut>(), 55, FishingRebornAPI.FishType.Sinker);

		API.AddToPool("JungleCatchPool", ModContent.ItemType<RivetingTadpole>(), 0.15f);
		API.AddCatchData(ModContent.ItemType<RivetingTadpole>(), 70, FishingRebornAPI.FishType.Dart);

		static bool HeartstrikerCondition(FishingAttempt attempt, Projectile proj) => ThoriumConfigServer.Instance.donatorWeapons.toggleAmphibiousWhip;
		API.AddToPool("JungleCatchPool", ModContent.ItemType<Heartstriker>(), 0.15f, new FishingRebornAPI.ArbitraryCondition(HeartstrikerCondition));
		API.AddCatchData(ModContent.ItemType<Heartstriker>(), 70, FishingRebornAPI.FishType.Mixed);

		API.AddToPool("CorruptionCatchPool", ModContent.ItemType<RottenCod>(), 0.15f);
		API.AddCatchData(ModContent.ItemType<RottenCod>(), 70, FishingRebornAPI.FishType.Floater);

		API.AddToPool("CrimsonCatchPool", ModContent.ItemType<BrainCoral>(), 0.15f);
		API.AddCatchData(ModContent.ItemType<BrainCoral>(), 70, FishingRebornAPI.FishType.Floater);

		API.AddToPool("OceanCatchPool", ModContent.ItemType<LilGuppy>(), 0.65f, new QuestFishCondition(ModContent.ItemType<LilGuppy>()));
		API.AddCatchData(ModContent.ItemType<LilGuppy>(), 40, FishingRebornAPI.FishType.Dart);

		API.AddToPool("OceanCatchPool", ModContent.ItemType<HatFish>(), 0.05f, new FishingRebornAPI.DayCondition());
		API.AddCatchData(ModContent.ItemType<HatFish>(), 100, FishingRebornAPI.FishType.Floater);

		static bool SubterraneanBulbCondition(FishingAttempt attempt, Projectile proj) => ThoriumConfigServer.Instance.donatorOther.toggleSubterraneanBulb;
		API.AddToPool("OceanCatchPool", ModContent.ItemType<SubterraneanBulb>(), 0.05f, new FishingRebornAPI.ArbitraryCondition(SubterraneanBulbCondition));
		API.AddCatchData(ModContent.ItemType<SubterraneanBulb>(), 70, FishingRebornAPI.FishType.Sinker);

		#endregion Fish

		#region Treasure

		static int ScarletCrateSelection(Player player, FishingAttempt attempt) => !Main.hardMode ? ModContent.ItemType<ScarletCrate>() : ModContent.ItemType<SinisterCrate>();

		API.AddTreasureData(
			ScarletCrateSelection,
			0.25f,
			FishingRebornAPI.LavaCondition
		);

		static int StrangeCrateSelection(Player player, FishingAttempt attempt) => !Main.hardMode ? ModContent.ItemType<StrangeCrate>() : ModContent.ItemType<WondrousCrate>();

		API.AddTreasureData(
			StrangeCrateSelection,
			0.6f,
			(_, _) => NPC.downedBoss1
		);

		static int AquaticDepthsCrateSelection(Player player, FishingAttempt attempt) => !Main.hardMode ? ModContent.ItemType<AquaticDepthsCrate>() : ModContent.ItemType<AbyssalCrate>();

		API.AddTreasureData(
			AquaticDepthsCrateSelection,
			0.6f,
			(player, attempt) =>
			{
				int tileX = (int)(player.position.X / 16f);
				bool ocean = attempt.waterTilesCount >= 300 && (tileX < WorldGen.beachDistance || tileX > Main.maxTilesX - WorldGen.beachDistance);
				return NPC.downedBoss2 && ocean;
			}
		);

		static bool GeodeGathererCondition(Player player, FishingAttempt attempt) => attempt.playerFishingConditions.PoleItemType == ModContent.ItemType<GeodeGatherer>();
		API.AddTreasureData(
			(_, _) => ItemID.Geode,
			0.5f,
			GeodeGathererCondition
		);

		#endregion Treasure
	}
}