using Bangarang.Content.Items.Weapons;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class BangarangThoriumModModule : CrossModModule<ThoriumModAPI>
{
	public override string CrossModName => ModNames.Bangarang;

	protected internal override void PostSetupContent()
	{
		API.AddMartianItemID<Rangaboom>();
	}
}