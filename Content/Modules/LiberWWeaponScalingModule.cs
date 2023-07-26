using Liber.Content.Items.Weapons.Melee.Shortswords;
using Liber.Content.Items.Weapons.Melee.Spears;
using Liber.Content.Items.Weapons.Melee.Swinging;
using Liber.Content.Items.Weapons.Melee.Swords;
using Liber.Content.Items.Weapons.Summon.Whips;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class LiberWWeaponScalingModule : CrossModModule<WWeaponScalingAPI>
{
	public override string CrossModName => ModNames.Liber;

	protected internal override void PostSetupContent()
	{
		API.AddScaling<Baselard>(WWeaponScalingAPI.Tier.Iron);
		API.AddScaling<CombatKnife>(WWeaponScalingAPI.Tier.Gold);
		API.AddScaling<Rapier>(WWeaponScalingAPI.Tier.Gold);
		API.AddScaling<Icicle>(WWeaponScalingAPI.Tier.Gold);
		API.AddScaling<PalmAxe>(WWeaponScalingAPI.Tier.Iron);
		API.AddScaling<BambooSword>(WWeaponScalingAPI.Tier.Iron);
		API.AddScaling<BastardSword>(WWeaponScalingAPI.Tier.Gold);
		API.AddScaling<RustedCutlass>(WWeaponScalingAPI.Tier.Iron);
		API.AddScaling<VikingSword>(WWeaponScalingAPI.Tier.Gold);
		API.AddScaling<Transendence>(WWeaponScalingAPI.Tier.Iron);
	}
}