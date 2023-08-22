using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace RandomModCompat.Common.Configs;

#if TML_2022_09
[Label(_localizationPrefix + "DisplayName")] 
#endif
public sealed class RandomModCompatConfig : ModConfig
{
	private const string _localizationPrefix = $"$Mods.RandomModCompat.Configs.{nameof(RandomModCompatConfig)}.";

	public override ConfigScope Mode => ConfigScope.ServerSide;

#if TML_2022_09
	[Label(_localizationPrefix + nameof(DisableIL) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(DisableIL) + ".Tooltip")] 
#endif
	[DefaultValue(false)]
	[ReloadRequired]
	public bool DisableIL;

#if TML_2022_09
	[Label(_localizationPrefix + nameof(SupportinatorEnabled) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(SupportinatorEnabled) + ".Tooltip")] 
#endif
	[DefaultValue(false)]
	public bool SupportinatorEnabled;

#if TML_2022_09
	[Label(_localizationPrefix + nameof(OutputModpack) + ".Label")]
	[Tooltip(_localizationPrefix + nameof(OutputModpack) + ".Tooltip")] 
#endif
	[DefaultValue(false)]
	public bool OutputModpack;
}