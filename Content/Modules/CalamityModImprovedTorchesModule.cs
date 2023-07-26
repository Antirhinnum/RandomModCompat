using CalamityMod.Tiles.Abyss;
using CalamityMod.Tiles.Astral;
using CalamityMod.Tiles.Crags;
using CalamityMod.Tiles.FurnitureAbyss;
using CalamityMod.Tiles.SunkenSea;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class CalamityModImprovedTorchesModule : CrossModModule<ImprovedTorchesAPI>
{
	public override string CrossModName => ModNames.CalamityMod;

	protected internal override void PostSetupContent()
	{
		API.RegisterTorch<SulphurousTorch>();
		API.RegisterTorch<AstralTorch>();
		API.RegisterTorch<GloomTorch>();
		API.RegisterTorch<AbyssTorch>();
		API.RegisterTorch<AlgalPrismTorch>();
		API.RegisterTorch<NavyPrismTorch>();
		API.RegisterTorch<RefractivePrismTorch>();
	}
}