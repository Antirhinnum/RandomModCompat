using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using TheDepths.Tiles;

namespace RandomModCompat.Content.Modules;

internal sealed class TheDepthsImprovedTorchesModule : CrossModModule<ImprovedTorchesAPI>
{
	public override string CrossModName => ModNames.TheDepths;

	protected internal override void PostSetupContent()
	{
		API.RegisterTorch<BlackTorch>();
		API.RegisterTorch<GeoTorch>();
	}
}