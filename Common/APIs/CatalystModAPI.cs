using Microsoft.Xna.Framework;
using RandomModCompat.Core;

namespace RandomModCompat.Common.APIs;

internal sealed partial class CatalystModAPI : ModAPI
{
	protected internal override string ModName => ModNames.CatalystMod;

	internal void AddWulfrumFlashlightLens(int gemItemId, Color beamColor)
	{
		CatalystModAPISystem.AddWulfrumFlashlightLens(Mod, gemItemId, beamColor);
	}
}