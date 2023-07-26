using Consolaria.Content.Items.Consumables;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class ConsolariaItemCheckBlacklistModule : CrossModModule<ItemCheckBlacklistAPI>
{
	public override string CrossModName => ModNames.Consolaria;

	protected internal override void PostSetupContent()
	{
		// TODO: Remove in 1.4.4.
		API.Add<HolyHandgrenade2>();
	}
}