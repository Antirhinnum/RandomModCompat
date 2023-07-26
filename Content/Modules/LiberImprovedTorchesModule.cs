using Liber.Content.Tiles;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class LiberImprovedTorchesModule : CrossModModule<ImprovedTorchesAPI>
{
	public override string CrossModName => ModNames.Liber;

	protected internal override void PostSetupContent()
	{
		API.RegisterTorch<BioluminescentTorch>();
	}
}