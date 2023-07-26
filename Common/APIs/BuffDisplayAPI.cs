using RandomModCompat.Core;

namespace RandomModCompat.Common.APIs;

internal sealed class BuffDisplayAPI : ModAPI
{
	protected internal override string ModName => ModNames.BuffDisplay;

	internal void SetCountAs(int buffId, int countsAs)
	{
		WrappedMod.Call(nameof(SetCountAs), buffId, countsAs);
	}
}