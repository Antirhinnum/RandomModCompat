using OrchidMod.Shaman.Weapons.Hardmode;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class OrchidModBossesAsNPCsModule : CrossModModule<BossesAsNPCsAPI>
{
	public override string CrossModName => ModNames.OrchidMod;

	protected internal override void PostSetupContent()
	{
		API.AddToShop<MartianBeamer>(BossesAsNPCsAPI.SellingTownNPC.MartianSaucer, 0.25f);
	}
}