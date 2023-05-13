using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace RandomModCompat.Common;

public sealed class ModSupportConfig : ModConfig
{
	public override ConfigScope Mode => ConfigScope.ServerSide;

	/// <summary>
	/// Determines if <paramref name="supportMod"/> support is enabled for <paramref name="baseMod"/>. (Ex: If Census support is enabled for Thorium Mod).
	/// </summary>
	/// <param name="baseMod">The internal name of the base mod.</param>
	/// <param name="supportMod">The internal name of the support mod.</param>
	/// <param name="justCheckOption">If <see langword="true"/>, this method will not return <see langword="false"/> if one of the checked mods is not enabled.</param>
	/// <returns><see langword="true"/> if both mods are enabled (unless <paramref name="justCheckOption"/> is <see langword="true"/>), if <paramref name="baseMod"/> <i>has</i> support for <paramref name="supportMod"/>, and if that support is enabled. Returns <see langword="false"/> otherwise.</returns>
	public bool SupportEnabled(string baseMod, string supportMod, bool justCheckOption = false)
	{
		// TODO: Implement saving.
		return justCheckOption || (ModLoader.HasMod(baseMod) && ModLoader.HasMod(supportMod));
	}
}