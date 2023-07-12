using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace RandomModCompat.Common.Configs;

public sealed class RandomModCompatConfig : ModConfig
{
	public override ConfigScope Mode => ConfigScope.ServerSide;

	[Label("Enable Debug Support Checks")]
	[Tooltip("If enabled, then this mod will run through all loaded mods and log potential content to add support for.\nForcibly enabled if the mod is running in debug mode.")]
	[DefaultValue(false)]
	public bool SupportinatorEnabled;
}