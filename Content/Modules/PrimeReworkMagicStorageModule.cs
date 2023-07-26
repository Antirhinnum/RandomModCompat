using PrimeRework.NPCs;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class PrimeReworkMagicStorageModule : CrossModModule<MagicStorageAPI>
{
	public override string CrossModName => ModNames.PrimeRework;

	protected internal override void PostSetupContent()
	{
		API.RegisterShadowDiamondDrop<TheTerminator>(1);
		API.RegisterShadowDiamondDrop<Caretaker>(1);
	}
}