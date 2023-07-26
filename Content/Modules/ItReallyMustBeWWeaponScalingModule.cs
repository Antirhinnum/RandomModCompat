using ItReallyMustBe.Items;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class ItReallyMustBeWWeaponScalingModule : CrossModModule<WWeaponScalingAPI>
{
	public override string CrossModName => ModNames.ItReallyMustBe;

	protected internal override void PostSetupContent()
	{
		API.AddScaling<DreadPistol>(WWeaponScalingAPI.Tier.Adamantite);
	}
}