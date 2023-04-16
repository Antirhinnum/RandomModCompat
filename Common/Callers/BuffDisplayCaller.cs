﻿using RandomModCompat.Core;

namespace RandomModCompat.Common.Callers;

internal sealed class BuffDisplayCaller : ModWithCalls
{
	protected override string ModName => "BuffDisplay";

	internal void SetCountAs(int buffId, int countsAs)
	{
		CalledMod.Call(nameof(SetCountAs), buffId, countsAs);
	}
}