﻿using RandomModCompat.Core;
using System.Collections.Generic;
using System.IO;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

internal sealed partial class TerraTypingAPI : ModAPI
{
	protected internal override string ModName => ModNames.TerraTyping;

	#region Default

	internal void CallWithArgs(IDictionary<string, object> args)
	{
		WrappedMod.Call(args);
	}

	#endregion Default

	#region Utility

	internal void AddTypes(TypeToAdd type, Mod targetMod)
	{
		CallWithArgs(new Dictionary<string, object>()
		{
			{ "Call", CallTypes.AddTypes.ToString() },
			{ "TypesToAdd", type.ToString() },
			{ "CallingMod", Mod },
			{ "Filename", TypesFileName(type, targetMod) },
			{ "ModTarget", targetMod }
		});
	}

	internal void OverrideTypes(TypeToAdd type, Mod targetMod)
	{
		CallWithArgs(new Dictionary<string, object>()
		{
			{ "Call", CallTypes.OverrideTypes.ToString() },
			{ "TypesToAdd", type.ToString() },
			{ "CallingMod", Mod },
			{ "Filename", TypesFileName(type, targetMod) },
			{ "ModTarget", targetMod }
		});
	}

	internal string TypesFileName(TypeToAdd type, Mod mod)
	{
		return Path.Combine("Assets", mod.Name, ModName, $"{type}.csv");
	}

	// Dict. Layout:
	// { "call", CallTypes (string) }
	// { "typestoadd", TypeToAdd (string) }
	// { "callingmod", Mod }
	// { "filename", string }
	// { "modtarget", Mod }
	// Filename must be in CallingMod. No mod name, includes extension (.csv).

	#endregion Utility
}