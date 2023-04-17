using PrimeRework.Items;
using RandomModCompat.Common.Callers;
using RandomModCompat.Core;
using Terraria;
using Terraria.ID;
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
	}

	private static void ROR2HealthBarsSupport()
	{
		if (!ModWithCalls.TryGetCaller(out ROR2HealthBarsCaller caller))
		{
			return;
		}

		caller.AddDesc<PrimeRework.NPCs.TheTerminator>(_primeReworkLocalization, "Terminator");
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
}