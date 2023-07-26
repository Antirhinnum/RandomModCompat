using Liber.Content.Tiles;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class LiberUniversalCraftModule : CrossModModule<UniversalCraftAPI>
{
	public override string CrossModName => ModNames.Liber;

	protected internal override void PostSetupContent()
	{
		API.AddStation<TanningRack>();
		API.AddStation<TransmutationTable>();
	}
}