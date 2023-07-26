using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using ThoriumMod.Core.ItemDropRules.DropConditions;
using ThoriumMod.NPCs.Bat;
using ThoriumMod.NPCs.Beholder;
using ThoriumMod.NPCs.Blizzard;
using ThoriumMod.NPCs.Buried;
using ThoriumMod.NPCs.Depths;
using ThoriumMod.NPCs.Granite;
using ThoriumMod.NPCs.Lich;
using ThoriumMod.NPCs.Primordials;
using ThoriumMod.NPCs.QueenJelly;
using ThoriumMod.NPCs.Scouter;
using ThoriumMod.NPCs.Thunder;

namespace RandomModCompat.Content.Modules;

// TODO: Remove in 1.4.4.
internal sealed class ThoriumModMagicStorageModule : CrossModModule<MagicStorageAPI>
{
	public override string CrossModName => ModNames.ThoriumMod;

	protected internal override void PostSetupContent()
	{
		API.RegisterShadowDiamondDrop<TheGrandThunderBirdv2>(1);
		API.RegisterShadowDiamondDrop<QueenJelly>(1);
		API.RegisterShadowDiamondDrop<Viscount>(1);
		API.RegisterShadowDiamondDrop<GraniteEnergyStorm>(1);
		API.RegisterShadowDiamondDrop<TheBuriedWarrior>(1);
		API.RegisterShadowDiamondDrop<ThePrimeScouter>(1);

		// Drop for both phases. A player could theoretically get more Diamonds because of this, but that would require them to kill the Borean Strider or Fallen Beholder before either had a chance to change phases, which is unlikely.
		API.RegisterShadowDiamondDrop<BoreanStrider>(1);
		API.RegisterShadowDiamondDrop<BoreanStriderPopped>(1);

		API.RegisterShadowDiamondDropNormalOnly<FallenDeathBeholder>(1);
		API.RegisterShadowDiamondDrop<FallenDeathBeholder2>(1);

		API.RegisterShadowDiamondDropNormalOnly<Lich>(1);
		API.RegisterShadowDiamondDrop<LichHeadless>(1);

		API.RegisterShadowDiamondDropNormalOnly<Abyssion>(1);
		API.RegisterShadowDiamondDropNormalOnly<AbyssionCracked>(1);
		API.RegisterShadowDiamondDrop<AbyssionReleased>(1);

		// The Primordials need an Expert Mode check to drop Diamonds, as well as a "last Primordial alive" check like the Twins.
		// Thankfully, Thorium handles the "last Primordial alive" check.
		IItemDropRule primordialsDropRule = API.GetShadowDiamondDropRule(2);
		IItemDropRule lastPrimordialRule = new LeadingConditionRule(new LastPrimordialDefeatedCondition());
		IItemDropRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
		lastPrimordialRule.OnSuccess(notExpertRule);
		notExpertRule.OnSuccess(primordialsDropRule);
		API.SetShadowDiamondDropRule(ModContent.NPCType<Aquaius>(), lastPrimordialRule);
		API.SetShadowDiamondDropRule(ModContent.NPCType<Omnicide>(), lastPrimordialRule);
		API.SetShadowDiamondDropRule(ModContent.NPCType<SlagFury>(), lastPrimordialRule);

		API.RegisterShadowDiamondDrop<RealityBreaker>(3);
	}
}