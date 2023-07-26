using PrimeRework.Items;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class PrimeReworkItemCheckBlacklistModule : CrossModModule<ItemCheckBlacklistAPI>
{
	public override string CrossModName => ModNames.PrimeRework;

	protected internal override void PostSetupContent()
	{
		API.Add<RabbitRune>();
	}
}