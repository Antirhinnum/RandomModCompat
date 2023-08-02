using Consolaria.Content.Items.Consumables;
using Consolaria.Content.Items.Weapons.Ammo;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class ConsolariaItemCheckBlacklistModule : CrossModModule<ItemCheckBlacklistAPI>
{
	public override string CrossModName => ModNames.Consolaria;

	protected internal override void PostSetupContent()
	{
		API.Add<VulcanBolt>();

		// TODO: Remove in 1.4.4.
		API.Add<SpectralArrow>();
		API.Add<HolyHandgrenade2>();
	}
}