using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using StormDiversMod.Items.Weapons;
using StormDiversMod.Projectiles;

namespace RandomModCompat.Content.Modules;

internal sealed class StormDiversModThoriumModModule : CrossModModule<ThoriumModAPI>
{
	public override string CrossModName => ModNames.StormDiversMod;

	protected internal override void PostSetupContent()
	{
		API.AddFlailProjectileID<DestroyerFlailProj>();
		API.AddMartianItemID<SuperDartLauncher>();
	}
}