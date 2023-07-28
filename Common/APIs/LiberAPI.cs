using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

internal sealed partial class LiberAPI : ModAPI
{
	protected internal override string ModName => ModNames.Liber;

	/// <summary>
	/// Adds a large gem pedestal.
	/// </summary>
	internal void AddLargeGemPedestal(int gemType, int largeGemType)
	{
		Mod.AddContent(new GemPedestal(gemType, largeGemType));
	}
}