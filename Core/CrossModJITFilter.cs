using RandomModCompat.Utilities;
using System;
using System.Reflection;
using Terraria.ModLoader;

namespace RandomModCompat.Core;

/// <summary>
/// When encountering a <see cref="CrossModModule"/>, this filter only JITs if <see cref="CrossModModule.CrossModName"/> and <see cref="CrossModModule.GeneralAPI"/>'s <see cref="ModAPI.ModName"/> are enabled.
/// <br/> In any other case, it functions as normal.
/// </summary>
internal class CrossModJITFilter : PreJITFilter
{
	public override bool ShouldJIT(MemberInfo member)
	{
		if (member is Type type && ReflectionHelper.IsCrossModModule(type, out Type apiType))
		{
			CrossModModule module = Activator.CreateInstance(type) as CrossModModule;
			ModAPI api = Activator.CreateInstance(apiType) as ModAPI;
			if (!ModLoader.HasMod(module.CrossModName) || !ModLoader.HasMod(api.ModName))
			{
				return false;
			}
		}

		return base.ShouldJIT(member);
	}
}