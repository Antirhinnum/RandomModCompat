using RandomModCompat.Core;

namespace RandomModCompat.Common.APIs;

internal sealed partial class TheDepthsAPI : ModAPI
{
	protected internal override string ModName => ModNames.TheDepths;

	/// <inheritdoc cref="TheDepthsAPISystem.AddNewTile(int, int, int, float)"/>
	internal void AddShaleGem(int gemItemId, int gemTileId, int gemBaseTileId, float frequency)
	{
		TheDepthsAPISystem.AddNewTile(Mod, gemItemId, gemTileId, gemBaseTileId, frequency);
	}
}