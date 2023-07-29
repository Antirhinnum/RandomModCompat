using Bangarang.Content.Items.Weapons;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class BangarangBossesAsNPCsModule : CrossModModule<BossesAsNPCsAPI>
{
	public override string CrossModName => ModNames.Bangarang;

	protected internal override void PostSetupContent()
	{
		API.AddToShop<ChromaticCrux>(BossesAsNPCsAPI.SellingTownNPC.EmpressOfLight, 0.25f);
		API.AddToShop<Rangaboom>(BossesAsNPCsAPI.SellingTownNPC.MartianSaucer, 0.167f);
	}
}