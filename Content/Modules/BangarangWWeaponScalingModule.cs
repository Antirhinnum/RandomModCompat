using Bangarang.Content.Items.Weapons;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class BangarangWWeaponScalingModule : CrossModModule<WWeaponScalingAPI>
{
	public override string CrossModName => ModNames.Bangarang;

	protected internal override void PostSetupContent()
	{
#if TML_2022_09
		API.AddScaling<Bananarang>(WWeaponScalingAPI.Tier.Molten);
		API.AddScaling<LightDisc>(WWeaponScalingAPI.Tier.Hallowed);
#endif

		API.AddScaling<ShadeChakram>(WWeaponScalingAPI.Tier.Evil);
		API.AddScaling<Synapse>(WWeaponScalingAPI.Tier.Evil);
		API.AddScaling<Beemerang>(WWeaponScalingAPI.Tier.Jungle);
		API.AddScaling<Bonerang>(WWeaponScalingAPI.Tier.Dungeon);
		API.AddScaling<SawedOffShotrang>(WWeaponScalingAPI.Tier.Molten);
		API.AddScaling<YinAndRang>(WWeaponScalingAPI.Tier.Adamantite);
		API.AddScaling<TheChloroplast>(WWeaponScalingAPI.Tier.Chlorophyte);
		API.AddScaling<Teslarang>(WWeaponScalingAPI.Tier.PostPlantera);
		API.AddScaling<ChromaticCrux>(WWeaponScalingAPI.Tier.PostGolem);
		API.AddScaling<Rangaboom>(WWeaponScalingAPI.Tier.PostGolem);
		API.AddScaling<WhiteDwarf>(WWeaponScalingAPI.Tier.Lunar);
	}
}