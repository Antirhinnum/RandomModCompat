using OrchidMod.Content.Items.Melee;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class OrchidModBangarangModule : CrossModModule<BangarangAPI>
{
	public override string CrossModName => ModNames.OrchidMod;

	protected internal override void PostSetupContent()
	{
		API.RegisterSimpleBoomerang<PrototypeSecrecy>(2);
	}
}