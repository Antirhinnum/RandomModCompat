using PrimeRework.Items;
using RandomModCompat.Common.APIs;
using RandomModCompat.Content.ExplicitSupport;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class PrimeReworkBossesAsNPCsModule : CrossModModule<BossesAsNPCsAPI>
{
	public override string CrossModName => ModNames.PrimeRework;

	protected internal override void PostSetupContent()
	{
		// Master Mode weapons
		API.AddToShop<Exitium>(BossesAsNPCsAPI.SellingTownNPC.TheDestroyer, API.ShouldSellMasterMode);
		API.AddToShop<DoubleTrouble>(BossesAsNPCsAPI.SellingTownNPC.Retinazer, 1f, 5f, API.ShouldSellMasterMode);
		API.AddToShop<DoubleTrouble>(BossesAsNPCsAPI.SellingTownNPC.Spazmatism, 1f, 5f, API.ShouldSellMasterMode);
		API.AddToShop<HandPrime>(BossesAsNPCsAPI.SellingTownNPC.SkeletronPrime, 1f, 5f, API.ShouldSellMasterMode);

		// Terminator drops
		API.AddToShop<BrainRemote>(BossesAsNPCsAPI.SellingTownNPC.TheDestroyer, 250000);
		API.AddToShop<SoulofPlight>(BossesAsNPCsAPI.SellingTownNPC.TheDestroyer, 10f, 5f); // For some reason, these are sold for x10 as much without the 10f div?
		API.AddToShop<TerminatorMask>(BossesAsNPCsAPI.SellingTownNPC.TheDestroyer, 0.14f);
		API.AddToShop<TerminatorTrophy>(BossesAsNPCsAPI.SellingTownNPC.TheDestroyer, 0.1f, 5f);
		API.AddToShop<MechanicalCockpitPiece>(BossesAsNPCsAPI.SellingTownNPC.TheDestroyer, 1f, 5f, API.SellExpertMode);
		API.AddToShop<Killswitch>(BossesAsNPCsAPI.SellingTownNPC.TheDestroyer, 0.25f, API.SellMasterMode);
		API.AddToShop<TerminatorRelic>(BossesAsNPCsAPI.SellingTownNPC.TheDestroyer, 1f, 5f, API.SellMasterMode);
		API.AddToShop<Finis>(BossesAsNPCsAPI.SellingTownNPC.TheDestroyer, 1f, 5f, API.SellMasterMode);

		// Caretaker drops
		static bool CaretakerDowned() => PrimeReworkCaretakerDownedSystem.caretakerDowned;

		API.AddToShop<BeeRemote>(BossesAsNPCsAPI.SellingTownNPC.QueenBee, 250000, CaretakerDowned);
		API.AddToShop<SoulofDight>(BossesAsNPCsAPI.SellingTownNPC.QueenBee, 10f, 5f, CaretakerDowned);
		API.AddToShop<CaretakerMask>(BossesAsNPCsAPI.SellingTownNPC.QueenBee, 0.14f, CaretakerDowned);
		API.AddToShop<CaretakerTrophy>(BossesAsNPCsAPI.SellingTownNPC.QueenBee, 0.1f, 5f, CaretakerDowned);
		API.AddToShop<MechanicalWings>(BossesAsNPCsAPI.SellingTownNPC.QueenBee, 1f, 5f, () => CaretakerDowned() && API.SellExpertMode());
		API.AddToShop<BeeController>(BossesAsNPCsAPI.SellingTownNPC.QueenBee, 0.25f, () => CaretakerDowned() && API.SellMasterMode());
		API.AddToShop<CaretakerRelic>(BossesAsNPCsAPI.SellingTownNPC.QueenBee, 1f, 5f, () => CaretakerDowned() && API.SellMasterMode());
		API.AddToShop<ArtificialStinger>(BossesAsNPCsAPI.SellingTownNPC.QueenBee, 1f, 5f, () => CaretakerDowned() && API.SellMasterMode());
	}
}