using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria;
using TheConfectionRebirth.Items.Placeable;
using TheConfectionRebirth.Items.Weapons;

namespace RandomModCompat.Content.Modules;

internal sealed class TheConfectionRebirthBossesAsNPCsModule : CrossModModule<BossesAsNPCsAPI>
{
	public override string CrossModName => ModNames.TheConfectionRebirth;

	protected internal override void PostSetupContent()
	{
		API.AddToShop<NeapoliniteBar>(BossesAsNPCsAPI.SellingTownNPC.TheDestroyer, Item.buyPrice(silver: 20));
		API.AddToShop<NeapoliniteBar>(BossesAsNPCsAPI.SellingTownNPC.Retinazer, Item.buyPrice(silver: 20));
		API.AddToShop<NeapoliniteBar>(BossesAsNPCsAPI.SellingTownNPC.Spazmatism, Item.buyPrice(silver: 20));
		API.AddToShop<NeapoliniteBar>(BossesAsNPCsAPI.SellingTownNPC.SkeletronPrime, Item.buyPrice(silver: 20));

		API.AddToShop<GrandSlammer>(BossesAsNPCsAPI.SellingTownNPC.WallOfFlesh, 7800 * 5);
	}
}