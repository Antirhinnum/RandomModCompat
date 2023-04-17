using RandomModCompat.Core;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomModCompat.Common.Callers;

internal sealed class TerraTypingCaller : ModWithCalls
{
	internal enum CallTypes
	{
		AddTypes,
		OverrideTypes
	}

	internal enum TypeToAdd
	{
		Ammo,
		Armor,
		NPC,
		Projectile,
		SpecialItem,
		Weapon
	}

	protected override string ModName => "TerraTyping";

	internal void CallWithArgs(IDictionary<string, object> args)
	{
		CalledMod.Call(args);
	}

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

	internal string TypesFileName(TypeToAdd type, Mod mod)
	{
		return Path.Combine("Assets", mod.Name, ModName, $"{type}.csv");
		//return $"Assets\\{mod.Name}\\TerraTyping\\{type}.csv";
	}

	// Dict. Layout:
	// { "call", CallTypes (string) }
	// { "typestoadd", TypeToAdd (string) }
	// { "callingmod", Mod }
	// { "filename", string }
	// { "modtarget", Mod }
	// Filename must be in CallingMod. No mod name, includes extension (.csv).
}