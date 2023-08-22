using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using StormDiversMod.NPCs.Boss;

namespace RandomModCompat.Content.Modules;

internal sealed class StormDiversModMagicStorageModule : CrossModModule<MagicStorageAPI>
{
	public override string CrossModName => ModNames.StormDiversMod;

	protected internal override void PostSetupContent()
	{
		API.RegisterShadowDiamondDrop<AridBoss>(1);
		API.RegisterShadowDiamondDrop<StormBoss>(1);

#if TML_2022_09
		API.RegisterShadowDiamondDrop<ThePainBoss>(1);
#else
		API.RegisterShadowDiamondDrop<TheUltimateBoss>(1);
#endif
	}
}