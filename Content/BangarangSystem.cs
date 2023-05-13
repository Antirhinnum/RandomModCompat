using Bangarang.Content.Items.Weapons;
using RandomModCompat.Common.Callers;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content;

[JITWhenModsEnabled(_modName)]
internal sealed class BangarangSystem : CrossModHandler
{
	private const string _modName = "Bangarang";
	public override string ModName => _modName;

	/*
	 * This file adds support for:
	 * - TerraTyping
	 * - W1K's Weapon Scaling
	 */

	internal override void PostSetupContent()
	{
		TerraTypingSupport();
		WWeaponSupport();
	}

	private void TerraTypingSupport()
	{
		if (!TryGetCaller(out TerraTypingCaller caller))
		{
			return;
		}

		caller.AddTypes(TerraTypingCaller.TypeToAdd.Projectile, CrossMod);
		caller.AddTypes(TerraTypingCaller.TypeToAdd.SpecialItem, CrossMod);
		caller.AddTypes(TerraTypingCaller.TypeToAdd.Weapon, CrossMod);
	}

	private void WWeaponSupport()
	{
		if (!TryGetCaller(out WWeaponScalingCaller caller))
		{
			return;
		}

		caller.AddScaling<ShadeChakram>(WWeaponScalingCaller.Tier.Evil);
		caller.AddScaling<Synapse>(WWeaponScalingCaller.Tier.Evil);
		caller.AddScaling<Beemerang>(WWeaponScalingCaller.Tier.Jungle);
		caller.AddScaling<Bonerang>(WWeaponScalingCaller.Tier.Dungeon);
		caller.AddScaling<Bananarang>(WWeaponScalingCaller.Tier.Molten);
		caller.AddScaling<SawedOffShotrang>(WWeaponScalingCaller.Tier.Molten);
		caller.AddScaling<YinAndRang>(WWeaponScalingCaller.Tier.Adamantite);
		caller.AddScaling<LightDisc>(WWeaponScalingCaller.Tier.Hallowed);
		caller.AddScaling<TheChloroplast>(WWeaponScalingCaller.Tier.Chlorophyte);
		caller.AddScaling<Teslarang>(WWeaponScalingCaller.Tier.PostPlantera);
		caller.AddScaling<ChromaticCrux>(WWeaponScalingCaller.Tier.PostGolem);
		caller.AddScaling<Rangaboom>(WWeaponScalingCaller.Tier.PostGolem);
		caller.AddScaling<WhiteDwarf>(WWeaponScalingCaller.Tier.Lunar);
	}
}