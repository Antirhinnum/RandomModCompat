using PrimeRework.Items;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class PrimeReworkBangarangModule : CrossModModule<BangarangAPI>
{
	public override string CrossModName => ModNames.PrimeRework;

	protected internal override void PostSetupContent()
	{
		API.RegisterSimpleBoomerang<LaserStar>(3, BangarangAPI.MakeSimpleHeldItemStackCheck());
	}
}