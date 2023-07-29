using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using TheConfectionRebirth.Tiles;

namespace RandomModCompat.Content.Modules;

internal sealed class TheConfectionRebirthImprovedTorchesModule : CrossModModule<ImprovedTorchesAPI>
{
	public override string CrossModName => ModNames.TheConfectionRebirth;

	protected internal override void PostSetupContent()
	{
		API.RegisterTorch<ConfectionTorch>();
		API.RegisterTorch<SherbetTorch>();
	}
}