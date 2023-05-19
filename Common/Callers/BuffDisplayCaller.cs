using RandomModCompat.Core;

namespace RandomModCompat.Common.Callers;

internal sealed class BuffDisplayCaller : ModWithCalls
{
	protected internal override string ModName => ModNames.BuffDisplay;

	internal void SetCountAs(int buffId, int countsAs)
	{
		CalledMod.Call(nameof(SetCountAs), buffId, countsAs);
	}
}