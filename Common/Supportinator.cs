using RandomModCompat.Utilities;
using System.Diagnostics;
using System.Reflection;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace RandomModCompat.Common;

/// <summary>
/// A simple checker for mod support.
/// </summary>
internal sealed class Supportinator : ModSystem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return Debugger.IsAttached;
	}

	// Runs after PostSetupContent().
	public override void PostSetupRecipes()
	{
		FishingChecker();
	}

	// Fishing Reborn
	private void FishingChecker()
	{
		Mod.Logger.Debug("Checking fishing methods...");
		MethodInfo catchFish = typeof(ModPlayer).GetMethod(nameof(ModPlayer.CatchFish), ReflectionHelper.AllFlags);
		foreach (ModPlayer player in ModContent.GetContent<ModPlayer>().WhereMethodIsOverridden(catchFish))
		{
			string modName = player.Mod.Name;
			Mod.Logger.Debug($"Hit! Mod: \"{modName}\" | ModPlayer: \"{player.Name}\"");
			if (!RandomModCompat.SupportEnabled(modName, ModNames.FishingReborn))
			{
				Mod.Logger.Debug("Missing FishingReborn support!");
			}
		}
	}
}