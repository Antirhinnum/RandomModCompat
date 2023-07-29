using OrchidMod.Shaman.Weapons.Hardmode;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class OrchidModThoriumModModule : CrossModModule<ThoriumModAPI>
{
	public override string CrossModName => ModNames.OrchidMod;

	protected internal override void PostSetupContent()
	{
		API.AddMartianItemID<MartianBeamer>();
	}
}