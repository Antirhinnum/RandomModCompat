using PrimeRework.Items;
using RandomModCompat.Common.Callers;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.PrimeReworkSupport;

[JITWhenModsEnabled(_modName)]
internal sealed class PrimeReworkSystem : CrossModHandler
{
	private const string _primeReworkLocalization = "Mods.RandomModCompat.PrimeRework.";

	private const string _modName = "PrimeRework";
	protected override string ModName => _modName;

	/*
	 * Mech Boss Rework adds support for the following mods:
	 * - Boss Checklist
	 * - Calamity Mod
	 * - Fargo's Souls Mod
	 *
	 * This file adds support for:
	 * - Bangarang
	 * - Bosses as NPCs
	 * - Item Check Blacklist Lib
	 * - Magic Storage
	 * - RoR 2 Health Bars
	 * - TerraTyping
	 */

	internal override void PostSetupContent()
	{
		BangarangSupport();
		BossesAsNPCsSupport();
		ItemCheckBlacklistSupport();
		MagicStorageSupport();
		ROR2HealthBarsSupport();
		TerraTypingSupport();
		WWeaponScalingSupport();
	}

	private static void BangarangSupport()
	{
		if (!ModWithCalls.TryGetCaller(out BangarangCaller caller))
		{
			return;
		}

		caller.RegisterSimpleBoomerang<LaserStar>(3, BangarangCaller.MakeSimpleHeldItemStackCheck());
	}

	private static void BossesAsNPCsSupport()
	{
		if (!ModWithCalls.TryGetCaller(out BossesAsNPCsCaller caller))
		{
			return;
		}

		// Master Mode weapons
		caller.AddToShop<Exitium>(BossesAsNPCsCaller.SellingTownNPC.TheDestroyer, caller.ShouldSellMasterMode);
		caller.AddToShop<DoubleTrouble>(BossesAsNPCsCaller.SellingTownNPC.Retinazer, 1f, 5f, caller.ShouldSellMasterMode);
		caller.AddToShop<DoubleTrouble>(BossesAsNPCsCaller.SellingTownNPC.Spazmatism, 1f, 5f, caller.ShouldSellMasterMode);
		caller.AddToShop<HandPrime>(BossesAsNPCsCaller.SellingTownNPC.SkeletronPrime, 1f, 5f, caller.ShouldSellMasterMode);

		// Terminator drops
		caller.AddToShop<BrainRemote>(BossesAsNPCsCaller.SellingTownNPC.TheDestroyer, 250000);
		caller.AddToShop<SoulofPlight>(BossesAsNPCsCaller.SellingTownNPC.TheDestroyer, 10f, 5f); // For some reason, these are sold for x10 as much without the 10f div?
		caller.AddToShop<TerminatorMask>(BossesAsNPCsCaller.SellingTownNPC.TheDestroyer, 0.14f);
		caller.AddToShop<TerminatorTrophy>(BossesAsNPCsCaller.SellingTownNPC.TheDestroyer, 0.1f, 5f);
		caller.AddToShop<MechanicalCockpitPiece>(BossesAsNPCsCaller.SellingTownNPC.TheDestroyer, 1f, 5f, caller.SellExpertMode);
		caller.AddToShop<Killswitch>(BossesAsNPCsCaller.SellingTownNPC.TheDestroyer, 0.25f, caller.SellMasterMode);
		caller.AddToShop<TerminatorRelic>(BossesAsNPCsCaller.SellingTownNPC.TheDestroyer, 1f, 5f, caller.SellMasterMode);
		caller.AddToShop<Finis>(BossesAsNPCsCaller.SellingTownNPC.TheDestroyer, 1f, 5f, caller.SellMasterMode);

		// Caretaker drops
		static bool CaretakerDowned() => PrimeReworkCaretakerDownedSystem.caretakerDowned;

		caller.AddToShop<BeeRemote>(BossesAsNPCsCaller.SellingTownNPC.QueenBee, 250000, CaretakerDowned);
		caller.AddToShop<SoulofDight>(BossesAsNPCsCaller.SellingTownNPC.QueenBee, 10f, 5f, CaretakerDowned);
		caller.AddToShop<CaretakerMask>(BossesAsNPCsCaller.SellingTownNPC.QueenBee, 0.14f, CaretakerDowned);
		caller.AddToShop<CaretakerTrophy>(BossesAsNPCsCaller.SellingTownNPC.QueenBee, 0.1f, 5f, CaretakerDowned);
		caller.AddToShop<MechanicalWings>(BossesAsNPCsCaller.SellingTownNPC.QueenBee, 1f, 5f, () => CaretakerDowned() && caller.SellExpertMode());
		caller.AddToShop<BeeController>(BossesAsNPCsCaller.SellingTownNPC.QueenBee, 0.25f, () => CaretakerDowned() && caller.SellMasterMode());
		caller.AddToShop<CaretakerRelic>(BossesAsNPCsCaller.SellingTownNPC.QueenBee, 1f, 5f, () => CaretakerDowned() && caller.SellMasterMode());
		caller.AddToShop<ArtificialStinger>(BossesAsNPCsCaller.SellingTownNPC.QueenBee, 1f, 5f, () => CaretakerDowned() && caller.SellMasterMode());
	}

	private static void ItemCheckBlacklistSupport()
	{
		if (!ModWithCalls.TryGetCaller(out ItemCheckBlacklistCaller caller))
		{
			return;
		}

		caller.Add<RabbitRune>();
	}

	private static void MagicStorageSupport()
	{
		if (!ModWithCalls.TryGetCaller(out MagicStorageCaller caller))
		{
			return;
		}

		caller.RegisterShadowDiamondDrop<PrimeRework.NPCs.TheTerminator>(1);
		caller.RegisterShadowDiamondDrop<PrimeRework.NPCs.Caretaker>(1);
	}

	private static void ROR2HealthBarsSupport()
	{
		if (!ModWithCalls.TryGetCaller(out ROR2HealthBarsCaller caller))
		{
			return;
		}

		caller.AddDesc<PrimeRework.NPCs.TheTerminator>(_primeReworkLocalization, "Terminator");
		caller.AddDesc<PrimeRework.NPCs.Caretaker>(_primeReworkLocalization, "Caretaker");
	}

	private void TerraTypingSupport()
	{
		if (!ModWithCalls.TryGetCaller(out TerraTypingCaller caller))
		{
			return;
		}

		caller.AddTypes(TerraTypingCaller.TypeToAdd.NPC, CrossMod);
		caller.AddTypes(TerraTypingCaller.TypeToAdd.Projectile, CrossMod);
		caller.AddTypes(TerraTypingCaller.TypeToAdd.SpecialItem, CrossMod);
		caller.AddTypes(TerraTypingCaller.TypeToAdd.Weapon, CrossMod);
	}

	private static void WWeaponScalingSupport()
	{
		if (!ModWithCalls.TryGetCaller(out WWeaponScalingCaller caller))
		{
			return;
		}

		caller.AddScaling<Bloodshed>(WWeaponScalingCaller.Tier.Molten);
		caller.AddScaling<BloodStainedPocketWatch>(WWeaponScalingCaller.Tier.Hallowed);
		caller.AddScaling<ClockworkWrench>(WWeaponScalingCaller.Tier.Hallowed);
		caller.AddScaling<DoubleTrouble>(WWeaponScalingCaller.Tier.Hallowed);
		caller.AddScaling<Exitium>(WWeaponScalingCaller.Tier.Hallowed);
		caller.AddScaling<Finis>(WWeaponScalingCaller.Tier.Hallowed);
		caller.AddScaling<HandPrime>(WWeaponScalingCaller.Tier.Hallowed);
		caller.AddScaling<Jumboshark>(WWeaponScalingCaller.Tier.Hallowed);
		//caller.AddScaling<LaserStar>(WWeaponScalingCaller.Tier.Hallowed); // Doesn't work since the Laser Star stacks.
		caller.AddScaling<RepurposedBrainRemote>(WWeaponScalingCaller.Tier.Hallowed);
		caller.AddScaling<RepurposedEyeRemote>(WWeaponScalingCaller.Tier.Hallowed);
		caller.AddScaling<RepurposedWormRemote>(WWeaponScalingCaller.Tier.Hallowed);
		caller.AddScaling<SublimeStellarSling>(WWeaponScalingCaller.Tier.Hallowed);
		caller.AddScaling<TheSnake>(WWeaponScalingCaller.Tier.Hallowed);
		caller.AddScaling<TheSpur>(WWeaponScalingCaller.Tier.Hallowed);
		caller.AddScaling<TrueBloodshed>(WWeaponScalingCaller.Tier.Hallowed);

		// Caretaker
		caller.AddScaling<ArtificialStinger>(WWeaponScalingCaller.Tier.Hallowed);
		caller.AddScaling<BreachCutter>(WWeaponScalingCaller.Tier.Hallowed);
		caller.AddScaling<RepurposedBeeRemote>(WWeaponScalingCaller.Tier.Hallowed);
		caller.AddScaling<SwarmGrenade>(WWeaponScalingCaller.Tier.Hallowed);
	}
}