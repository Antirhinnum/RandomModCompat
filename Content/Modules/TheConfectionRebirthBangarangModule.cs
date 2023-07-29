using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using TheConfectionRebirth.Items.Weapons;

namespace RandomModCompat.Content.Modules;

internal sealed class TheConfectionRebirthBangarangModule : CrossModModule<BangarangAPI>
{
	public override string CrossModName => ModNames.TheConfectionRebirth;

	protected internal override void PostSetupContent()
	{
		API.RegisterSimpleBoomerang<BakersDozen>(13, BangarangAPI.MakeSimpleHeldItemStackCheck());
		// Uses the same check as BakersDozen, even though maxStack is 1. Keep it for now.
		API.RegisterSimpleBoomerang<BearClaw>(useCheck: BangarangAPI.MakeSimpleHeldItemStackCheck());
	}
}