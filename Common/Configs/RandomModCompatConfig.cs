using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace RandomModCompat.Common.Configs;

[Label(_localizationPrefix + "Name")]
public sealed class RandomModCompatConfig : ModConfig
{
	private const string _localizationPrefix = "$Mods.RandomModCompat.Configs.Main.";

	public override ConfigScope Mode => ConfigScope.ServerSide;

	[Label(_localizationPrefix + nameof(SupportinatorEnabled) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(SupportinatorEnabled) + ".Tooltip")]
	[DefaultValue(false)]
	public bool SupportinatorEnabled;
}