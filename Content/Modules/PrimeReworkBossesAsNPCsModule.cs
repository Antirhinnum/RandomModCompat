using PrimeRework.Items;
using RandomModCompat.Common.APIs;
using RandomModCompat.Content.ExplicitSupport;
using RandomModCompat.Core;
using System;
using Terraria;

namespace RandomModCompat.Content.Modules;

internal sealed class PrimeReworkBossesAsNPCsModule : CrossModModule<BossesAsNPCsAPI>
{
#if !TML_2022_09
	private static readonly Condition CaretakerDefeated = new("Mods.RandomModCompat.PrimeRework.CaretakerDefeatedCondition", () => PrimeReworkCaretakerDownedSystem.caretakerDowned);
#endif

	public override string CrossModName => ModNames.PrimeRework;

	protected internal override void PostSetupContent()
	{
#if TML_2022_09
		Func<bool> expertCondition = API.ShouldSellExpertMode;
		Func<bool> masterCondition = API.ShouldSellMasterMode;
		Func<bool> caretakerCondition = () => PrimeReworkCaretakerDownedSystem.caretakerDowned;
		Func<bool> caretakerExpertCondition = () => PrimeReworkCaretakerDownedSystem.caretakerDowned && API.ShouldSellExpertMode();
		Func<bool> caretakerMasterCondition = () => PrimeReworkCaretakerDownedSystem.caretakerDowned && API.ShouldSellMasterMode();
#else
		Condition expertCondition = Condition.InExpertMode;
		Condition masterCondition = Condition.InMasterMode;
		Condition caretakerCondition = CaretakerDefeated;
		Condition[] caretakerExpertCondition = { caretakerCondition, expertCondition };
		Condition[] caretakerMasterCondition = { caretakerCondition, masterCondition };
#endif

		// Master Mode weapons
		API.AddToShop<Exitium>(BossesAsNPCsAPI.SellingTownNPC.TheDestroyer, masterCondition);
		API.AddToShop<DoubleTrouble>(BossesAsNPCsAPI.SellingTownNPC.Retinazer, 1f, 5f, masterCondition);
		API.AddToShop<DoubleTrouble>(BossesAsNPCsAPI.SellingTownNPC.Spazmatism, 1f, 5f, masterCondition);
		API.AddToShop<HandPrime>(BossesAsNPCsAPI.SellingTownNPC.SkeletronPrime, 1f, 5f, masterCondition);

		// Terminator drops
		API.AddToShop<BrainRemote>(BossesAsNPCsAPI.SellingTownNPC.TheDestroyer, 250000);
		API.AddToShop<SoulofPlight>(BossesAsNPCsAPI.SellingTownNPC.TheDestroyer, 10f, 5f); // For some reason, these are sold for x10 as much without the 10f div?
		API.AddToShop<TerminatorMask>(BossesAsNPCsAPI.SellingTownNPC.TheDestroyer, 0.14f);
		API.AddToShop<TerminatorTrophy>(BossesAsNPCsAPI.SellingTownNPC.TheDestroyer, 0.1f, 5f);
		API.AddToShop<MechanicalCockpitPiece>(BossesAsNPCsAPI.SellingTownNPC.TheDestroyer, 1f, 5f, expertCondition);
		API.AddToShop<Killswitch>(BossesAsNPCsAPI.SellingTownNPC.TheDestroyer, 0.25f, masterCondition);
		API.AddToShop<TerminatorRelic>(BossesAsNPCsAPI.SellingTownNPC.TheDestroyer, 1f, 5f, masterCondition);
		API.AddToShop<Finis>(BossesAsNPCsAPI.SellingTownNPC.TheDestroyer, 1f, 5f, masterCondition);

		// Caretaker drops
		API.AddToShop<BeeRemote>(BossesAsNPCsAPI.SellingTownNPC.QueenBee, 250000, caretakerCondition);
		API.AddToShop<SoulofDight>(BossesAsNPCsAPI.SellingTownNPC.QueenBee, 10f, 5f, caretakerCondition);
		API.AddToShop<CaretakerMask>(BossesAsNPCsAPI.SellingTownNPC.QueenBee, 0.14f, caretakerCondition);
		API.AddToShop<CaretakerTrophy>(BossesAsNPCsAPI.SellingTownNPC.QueenBee, 0.1f, 5f, caretakerCondition);
		API.AddToShop<MechanicalWings>(BossesAsNPCsAPI.SellingTownNPC.QueenBee, 1f, 5f, caretakerExpertCondition);
		API.AddToShop<BeeController>(BossesAsNPCsAPI.SellingTownNPC.QueenBee, 0.25f, caretakerMasterCondition);
		API.AddToShop<CaretakerRelic>(BossesAsNPCsAPI.SellingTownNPC.QueenBee, 1f, 5f, caretakerMasterCondition);
		API.AddToShop<ArtificialStinger>(BossesAsNPCsAPI.SellingTownNPC.QueenBee, 1f, 5f, caretakerMasterCondition);
	}
}