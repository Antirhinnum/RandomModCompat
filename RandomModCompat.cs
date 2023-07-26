using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat;

public sealed class RandomModCompat : Mod
{
	private const string AssemblyName = "RandomModCompatDynamicAssembly";
	public DynamicAssembly DynamicAssembly { get; init; } = new(AssemblyName);
	public static RandomModCompat Instance => ModContent.GetInstance<RandomModCompat>();

	public RandomModCompat()
	{
		PreJITFilter = new CrossModJITFilter();
		SupportAggregator.PrepareSuperEarlyChanges();
	}

	/// <inheritdoc cref="SupportAggregator.SupportEnabled(string, string, bool)"/>
	public static bool SupportEnabled(string baseMod, string supportMod, bool ignoreEnabledMod = false)
	{
		return SupportAggregator.SupportEnabled(baseMod, supportMod, ignoreEnabledMod);
	}
}