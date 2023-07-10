using MonoMod.RuntimeDetour.HookGen;
using RandomModCompat.Common;
using RandomModCompat.Core;
using RandomModCompat.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace RandomModCompat;

public sealed class RandomModCompat : Mod
{
	private const string AssemblyName = "RandomModCompatDynamicAssembly";

	private static readonly Dictionary<string, string[]> _supportedMods = new()
	{
		{ ModNames.BalloonsExtended, new[] { ModNames.AsymmetricEquips } },
		{ ModNames.Bangarang, new[] { ModNames.TerraTyping, ModNames.WWeaponScaling } },
		{ ModNames.CalamityMod, new[] { ModNames.AsymmetricEquips, ModNames.Bangarang, ModNames.BuffDisplay, ModNames.ImprovedTorches, ModNames.ItemCheckBlacklist, ModNames.ThoriumMod, ModNames.UniversalCraft } },
		{ ModNames.Consolaria, new[] { ModNames.AsymmetricEquips, ModNames.ItemCheckBlacklist, ModNames.MagicStorage, ModNames.ROR2HealthBars, ModNames.SummonersAssociation, ModNames.TerraTyping, ModNames.ThoriumMod } },
		{ ModNames.DBZMODPORT, new[] { ModNames.AutoReroll } },
		{ ModNames.ItReallyMustBe, new[] { ModNames.BossesAsNPCs, ModNames.ItemCheckBlacklist, ModNames.ROR2HealthBars, ModNames.TerraTyping, ModNames.WWeaponScaling } },
		{ ModNames.Liber, new[] { ModNames.AsymmetricEquips, ModNames.ImprovedTorches, ModNames.ThoriumMod, ModNames.UniversalCraft, ModNames.WWeaponScaling } },
		{ ModNames.Mask, new[] { ModNames.AsymmetricEquips, ModNames.Census } },
		{ ModNames.PrimeRework, new[] { ModNames.Bangarang, ModNames.BossesAsNPCs, ModNames.ItemCheckBlacklist, ModNames.MagicStorage, ModNames.ROR2HealthBars, ModNames.TerraTyping, ModNames.ThoriumMod, ModNames.WWeaponScaling } },
		{ ModNames.SlugNPCs, new[] { ModNames.Census, ModNames.DialogueTweak } },
		{ ModNames.ThoriumMod, new[] { ModNames.AsymmetricEquips, ModNames.BuffDisplay, ModNames.DialogueTweak, ModNames.FishingReborn, ModNames.ImprovedTorches, ModNames.ItemCheckBlacklist, ModNames.levelplus, ModNames.MagicStorage, ModNames.MeleeEffects, ModNames.OverpoweredGoldDust, ModNames.RescueFairies, ModNames.ROR2HealthBars, ModNames.UniversalCraft } }
	};

	public DynamicAssembly DynamicAssembly { get; init; } = new(AssemblyName);
	public static RandomModCompat Instance => ModContent.GetInstance<RandomModCompat>();

	private static Dictionary<string, FieldInfo> _baseModToField;

	// Support flag by two mod names.
	// Keyed: _supportFlagByModNames["PrimeRework"]["ThoriumMod"]
	private static Dictionary<string, Dictionary<string, FieldInfo>> _supportFlagByModNames;

	private static ModConfig _config;

	public RandomModCompat()
	{
		HookAutoloadConfig();
	}

	// Hook into Mod::AutoloadConfig().
	// This has to be done here since nothing else runs early enough.
	private static void HookAutoloadConfig()
	{
		const string AutoloadConfig = nameof(AutoloadConfig);
		MethodBase autoloadConfig = typeof(Mod).GetMethod(AutoloadConfig, ReflectionHelper.AllFlags)
			?? throw new MethodAccessException("Cannot find Mod::AutoloadConfig(), cannot create this mod's config.");

		HookEndpointManager.Add(autoloadConfig, AddAndCreateSupportConfig);
	}

	private static void AddAndCreateSupportConfig(Action<Mod> orig, Mod mod)
	{
		orig(mod);

		if (mod is RandomModCompat randomModCompat)
		{
			Type configType = SupportConfigBuilder.Create(randomModCompat.DynamicAssembly.ModuleBuilder, _supportedMods);

			FieldInfo[] fields = configType.GetFields();

			_baseModToField = fields.ToDictionary(f => f.Name);
			_supportFlagByModNames = fields.ToDictionary(
				   f => f.Name,
				   f => f.FieldType.GetFields().ToDictionary(inner => inner.Name));

			_config = Activator.CreateInstance(configType) as ModConfig;
			mod.AddConfig(configType.Name, _config);
		}
	}

	/// <summary>
	/// Determines if <paramref name="supportMod"/> support for <paramref name="baseMod"/> is enabled.
	/// </summary>
	/// <param name="baseMod">The base mod, the one that adds the content.</param>
	/// <param name="supportMod">The support mod, the one that adds the systems.</param>
	/// <returns><see langword="true"/> if both mods are enabled and support is enabled, <see langword="false"/> otherwise.</returns>
	public static bool SupportEnabled(string baseMod, string supportMod)
	{
		if (!_supportedMods.TryGetValue(baseMod, out string[] possibleSupportMods) || !possibleSupportMods.Contains(supportMod))
		{
			Instance.Logger.Warn($"HEY DUMBASS: SupportEnabled called with mods {baseMod}, {supportMod}, which aren't in the config.");
		}

		if (!ModLoader.HasMod(baseMod) || !ModLoader.HasMod(supportMod))
		{
			return false;
		}

		if (!_supportFlagByModNames.TryGetValue(baseMod, out Dictionary<string, FieldInfo> supportedMods))
		{
			return false;
		}

		if (!supportedMods.TryGetValue(supportMod, out FieldInfo field))
		{
			return false;
		}

		return Convert.ToBoolean(field.GetValue(_baseModToField[baseMod].GetValue(_config)));
	}
}