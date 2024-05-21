using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Projectiles.DraedonsArsenal;
using CalamityMod.Projectiles.Melee;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class CalamityModThoriumModModule : CrossModModule<ThoriumModAPI>
{
	public override string CrossModName => ModNames.CalamityMod;

	protected internal override void PostSetupContent()
	{
		// Flails
		API.AddFlailProjectileID<BallOFuguProj>();
		API.AddFlailProjectileID<ClamCrusherFlail>();
		API.AddFlailProjectileID<CosmicDischargeFlail>();
		API.AddFlailProjectileID<CrescentMoonFlail>();
		API.AddFlailProjectileID<DragonPowFlail>();
		API.AddFlailProjectileID<MourningstarFlail>();
		API.AddFlailProjectileID<NebulashFlail>();
		API.AddFlailProjectileID<PulseDragonProjectile>();
		API.AddFlailProjectileID<RemsRevengeProj>();
		API.AddFlailProjectileID<SpineOfThanatosProjectile>();
		API.AddFlailProjectileID<TumbleweedFlail>();
		API.AddFlailProjectileID<UrchinBall>();
		API.AddFlailProjectileID<UrchinMaceProjectile>();
		API.AddFlailProjectileID<YateveoBloomProj>();

		// Martian
		API.AddMartianItemID<NullificationPistol>();
		API.AddMartianItemID<Wingman>();
		API.AddMartianItemID<ShockGrenade>();

		// TODO: DoT debuffs?
		// TODO: Status debuffs?
	}
}