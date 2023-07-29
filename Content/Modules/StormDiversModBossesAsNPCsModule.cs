using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using StormDiversMod.Items.Weapons;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class StormDiversModBossesAsNPCsModule : CrossModModule<BossesAsNPCsAPI>
{
	public override string CrossModName => ModNames.StormDiversMod;

	protected internal override void PostSetupContent()
	{
		API.AddToShop<EyeSword>(BossesAsNPCsAPI.SellingTownNPC.EyeOfCthulhu, 0.25f);
		API.AddToShop<EyeGun>(BossesAsNPCsAPI.SellingTownNPC.EyeOfCthulhu, 0.25f);
		API.AddToShop<EyeStaff>(BossesAsNPCsAPI.SellingTownNPC.EyeOfCthulhu, 0.25f);
		API.AddToShop<EyeMinion>(BossesAsNPCsAPI.SellingTownNPC.EyeOfCthulhu, 0.25f);
		// Internal class :(
		API.AddToShop(BossesAsNPCsAPI.SellingTownNPC.EyeOfCthulhu, CrossMod.Find<ModItem>("EyeHook").Type, 0.25f);

		API.AddToShop<CultistLazor>(BossesAsNPCsAPI.SellingTownNPC.LunaticCultist, 1 / 33f);
		API.AddToShop<CultistBow>(BossesAsNPCsAPI.SellingTownNPC.LunaticCultist, 0.25f);
		API.AddToShop<CultistTome>(BossesAsNPCsAPI.SellingTownNPC.LunaticCultist, 0.25f);
		API.AddToShop<CultistSpear>(BossesAsNPCsAPI.SellingTownNPC.LunaticCultist, 0.25f);
		API.AddToShop<CultistStaff>(BossesAsNPCsAPI.SellingTownNPC.LunaticCultist, 0.25f);
	}
}