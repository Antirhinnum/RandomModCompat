using RandomModCompat.Common.Configs;
using Terraria.ModLoader;

namespace RandomModCompat.Common;

internal sealed class ModPackCreator : ModSystem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return ModContent.GetInstance<RandomModCompatConfig>().OutputModpack;
	}

	public override void PostSetupContent()
	{
		Mod.Logger.Info("enabled.json:\r\n" + SupportAggregator.CreateTestingEnabledList());
	}
}