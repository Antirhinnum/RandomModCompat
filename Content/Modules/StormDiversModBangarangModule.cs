using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using StormDiversMod.Items.Weapons;
using StormDiversMod.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class StormDiversModBangarangModule : CrossModModule<BangarangAPI>
{
	public override string CrossModName => ModNames.StormDiversMod;

	protected internal override void PostSetupContent()
	{
		API.RegisterSimpleBoomerang<BoneBoomerang>(3);
		API.RegisterSimpleBoomerang<FrostStar>(3);
		API.RegisterSimpleBoomerang<LunarSelenianBlade>(-1);

		// This uses some weird logic that doesn't always work with Bangarang's accessories.
		API.RegisterBoomerang<TheSickle>(new[] { ModContent.ProjectileType<TheSickleProj2>() }, 2, TheSickleCheck);
	}

	// Only the right-click has boomerang functionality.
	// Additionally, each added boomerang adds two sickles, as each "use" of the item throws two.
	private static bool TheSickleCheck(Player player, Item item, int extra)
	{
		return player.ownedProjectileCounts[ModContent.ProjectileType<TheSickleProj2>()] < 2 * (1 + extra);
	}
}