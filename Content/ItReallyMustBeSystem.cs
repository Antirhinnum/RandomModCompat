using ItReallyMustBe;
using RandomModCompat.Common.Callers;
using RandomModCompat.Core;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomModCompat.Content;

[JITWhenModsEnabled(_modName)]
internal sealed class ItReallyMustBeSystem : CrossModHandler
{
	private const string _modName = "ItReallyMustBe";
	protected override string ModName => _modName;

	/*
	 * Dreadnautilus is a Boss adds support for the following mods:
	 * - Boss Checklist
	 *
	 * The file adds support for:
	 * - Bosses as NPCs
	 * - Item Check Blacklist Lib
	 * - RoR 2 Health Bars
	 * - TerraTyping
	 * - W1K's Weapon Scaling
	 *
	 * The file *will not* add support for:
	 * - Magic Storage: This mod registers the Dreadnautilus as a miniboss for Boss Checklist, so it shouldn't drop any Diamonds. Plus, it's a pain to add the rule to a vanilla NPC.
	 */

	internal override void PostSetupContent()
	{
		BossesAsNPCsSupport();
		ItemCheckBlacklistSupport();
		ROR2HealthBarsSupport();
		TerraTypingSupport();
		WWeaponScalingSupport();
	}

	private static void BossesAsNPCsSupport()
	{
		if (!ModWithCalls.TryGetCaller(out BossesAsNPCsCaller caller))
		{
			return;
		}

		caller.AddToShop<ItReallyMustBe.Items.FunnyBait>(BossesAsNPCsCaller.SellingTownNPC.Dreadnautilus, 1f, 5f, () => !ModContent.GetInstance<MustConfig>().ByeBait);

		caller.AddToShop<ItReallyMustBe.Items.DreadPistol>(BossesAsNPCsCaller.SellingTownNPC.Dreadnautilus, 0.5f);
		caller.AddToShop<ItReallyMustBe.Items.DreadnautilusMask>(BossesAsNPCsCaller.SellingTownNPC.Dreadnautilus, 0.14f);
		caller.AddToShop<ItReallyMustBe.Items.DreadnautilusTrophy>(BossesAsNPCsCaller.SellingTownNPC.Dreadnautilus, 0.1f);

		caller.AddToShop<ItReallyMustBe.Items.DreadnautilusRelic>(BossesAsNPCsCaller.SellingTownNPC.Dreadnautilus, 1f, 5f, caller.ShouldSellMasterMode);
		caller.AddToShop<ItReallyMustBe.Items.BloodyCarKey>(BossesAsNPCsCaller.SellingTownNPC.Dreadnautilus, 1f, 5f, caller.ShouldSellMasterMode);
	}

	private static void ItemCheckBlacklistSupport()
	{
		if (!ModWithCalls.TryGetCaller(out ItemCheckBlacklistCaller caller))
		{
			return;
		}

		if (ModContent.GetInstance<MustConfig>().ByeBait)
		{
			caller.Add<ItReallyMustBe.Items.FunnyBait>();
		}
	}

	private static void ROR2HealthBarsSupport()
	{
		if (!ModWithCalls.TryGetCaller(out ROR2HealthBarsCaller caller))
		{
			return;
		}

		caller.BossDesc(NPCID.BloodNautilus, "Mods.RandomModCompat.ItReallyMustBe.ROR2.Dreadnautilus.Description");
	}

	private void TerraTypingSupport()
	{
		if (!ModWithCalls.TryGetCaller(out TerraTypingCaller caller))
		{
			return;
		}

		caller.AddTypes(TerraTypingCaller.TypeToAdd.Weapon, CrossMod);
	}

	private static void WWeaponScalingSupport()
	{
		if (!ModWithCalls.TryGetCaller(out WWeaponScalingCaller caller))
		{
			return;
		}

		caller.AddScaling<ItReallyMustBe.Items.DreadPistol>(WWeaponScalingCaller.Tier.Adamantite);
	}
}