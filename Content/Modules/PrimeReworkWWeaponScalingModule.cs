using PrimeRework.Items;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class PrimeReworkWWeaponScalingModule : CrossModModule<WWeaponScalingAPI>
{
	public override string CrossModName => ModNames.PrimeRework;

	protected internal override void PostSetupContent()
	{
		API.AddScaling<Bloodshed>(WWeaponScalingAPI.Tier.Molten);
		API.AddScaling<BloodStainedPocketWatch>(WWeaponScalingAPI.Tier.Hallowed);
		API.AddScaling<ClockworkWrench>(WWeaponScalingAPI.Tier.Hallowed);
		API.AddScaling<DoubleTrouble>(WWeaponScalingAPI.Tier.Hallowed);
		API.AddScaling<Exitium>(WWeaponScalingAPI.Tier.Hallowed);
		API.AddScaling<Finis>(WWeaponScalingAPI.Tier.Hallowed);
		API.AddScaling<HandPrime>(WWeaponScalingAPI.Tier.Hallowed);
		API.AddScaling<Jumboshark>(WWeaponScalingAPI.Tier.Hallowed);
		//API.AddScaling<LaserStar>(WWeaponScalingAPI.Tier.Hallowed); // Doesn't work since the Laser Star stacks.
		API.AddScaling<RepurposedBrainRemote>(WWeaponScalingAPI.Tier.Hallowed);
		API.AddScaling<RepurposedEyeRemote>(WWeaponScalingAPI.Tier.Hallowed);
		API.AddScaling<RepurposedWormRemote>(WWeaponScalingAPI.Tier.Hallowed);
		API.AddScaling<SublimeStellarSling>(WWeaponScalingAPI.Tier.Hallowed);
		API.AddScaling<TheSnake>(WWeaponScalingAPI.Tier.Hallowed);
		API.AddScaling<TheSpur>(WWeaponScalingAPI.Tier.Hallowed);
		API.AddScaling<TrueBloodshed>(WWeaponScalingAPI.Tier.Hallowed);

		// Caretaker
		API.AddScaling<ArtificialStinger>(WWeaponScalingAPI.Tier.Hallowed);
		API.AddScaling<BreachCutter>(WWeaponScalingAPI.Tier.Hallowed);
		API.AddScaling<RepurposedBeeRemote>(WWeaponScalingAPI.Tier.Hallowed);
		API.AddScaling<SwarmGrenade>(WWeaponScalingAPI.Tier.Hallowed);
	}
}