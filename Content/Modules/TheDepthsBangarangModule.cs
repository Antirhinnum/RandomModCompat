using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using TheDepths.Items.Weapons;

namespace RandomModCompat.Content.Modules;

internal sealed class TheDepthsBangarangModule : CrossModModule<BangarangAPI>
{
	public override string CrossModName => ModNames.TheDepths;

	protected internal override void PostSetupContent()
	{
		API.RegisterSimpleBoomerang<Shaderang>(useCheck: BangarangAPI.MakeSimpleHeldItemStackCheck());
	}
}