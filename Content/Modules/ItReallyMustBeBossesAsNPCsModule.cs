using ItReallyMustBe;
using ItReallyMustBe.Items;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class ItReallyMustBeBossesAsNPCsModule : CrossModModule<BossesAsNPCsAPI>
{
	public override string CrossModName => ModNames.ItReallyMustBe;

	protected internal override void PostSetupContent()
	{
		static bool BaitCondition() => !ModContent.GetInstance<MustConfig>().ByeBait;
		API.AddToShop<FunnyBait>(BossesAsNPCsAPI.SellingTownNPC.Dreadnautilus, 1f, 5f, BaitCondition);

		API.AddToShop<DreadPistol>(BossesAsNPCsAPI.SellingTownNPC.Dreadnautilus, 0.5f);
		API.AddToShop<DreadnautilusMask>(BossesAsNPCsAPI.SellingTownNPC.Dreadnautilus, 0.14f);
		API.AddToShop<DreadnautilusTrophy>(BossesAsNPCsAPI.SellingTownNPC.Dreadnautilus, 0.1f);

		API.AddToShop<DreadnautilusRelic>(BossesAsNPCsAPI.SellingTownNPC.Dreadnautilus, 1f, 5f, API.ShouldSellMasterMode);
		API.AddToShop<BloodyCarKey>(BossesAsNPCsAPI.SellingTownNPC.Dreadnautilus, 1f, 5f, API.ShouldSellMasterMode);
	}
}