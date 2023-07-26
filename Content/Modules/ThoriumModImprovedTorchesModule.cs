using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using ThoriumMod.Tiles;

namespace RandomModCompat.Content.Modules;

internal sealed class ThoriumModImprovedTorchesModule : CrossModModule<ImprovedTorchesAPI>
{
	public override string CrossModName => ModNames.ThoriumMod;

	protected internal override void PostSetupContent()
	{
		API.RegisterTorch<BlackTorch>();
		API.RegisterTorch<DeeplightTorch>();
		API.RegisterTorch<MagentaTorch>();
	}
}