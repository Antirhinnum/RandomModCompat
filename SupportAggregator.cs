using MonoMod.RuntimeDetour.HookGen;
using RandomModCompat.Common;
using RandomModCompat.Core;
using RandomModCompat.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Core;

namespace RandomModCompat;

/// <summary>
/// Handles aggregating all known support modules and creating the Mod Support Config from them.
/// </summary>
public static class SupportAggregator
{
	/// <summary>
	/// All mod support added by this mod.
	/// </summary>
	private static Dictionary<string, ICollection<string>> AddedSupport { get; set; } = new();

	/// <summary>
	/// All known mod support handled by other mods and thus, not by this one.
	/// </summary>
	private static Dictionary<string, ICollection<string>> KnownExternalSupport { get; } = new()
	{
		{ ModNames.AssortedCrazyThings, new[] { ModNames.AmuletOfManyMinions, ModNames.BossChecklist, ModNames.SummonersAssociation } },
		{ ModNames.CalamityMod, new[] { ModNames.BossChecklist, ModNames.BossesAsNPCs, ModNames.Census, ModNames.DialogueTweak, ModNames.Fargowiltas, ModNames.SummonersAssociation, ModNames.Wikithis } },
		{ ModNames.ClickerClass, new[] { ModNames.ColoredDamageTypes, ModNames.BossesAsNPCs, ModNames.RecipeBrowser, ModNames.ThoriumMod, ModNames.Wikithis } },
		{ ModNames.Consolaria, new[] { ModNames.BossChecklist, ModNames.Fargowiltas } },
		{ ModNames.ItReallyMustBe, new[] { ModNames.BossChecklist} },
		{ ModNames.OrchidMod, new[] { ModNames.BossChecklist, ModNames.Census, ModNames.ThoriumMod } },
		{ ModNames.PrimeRework, new[] { ModNames.BossChecklist, ModNames.CalamityMod, ModNames.FargowiltasSouls } },
		{ ModNames.StormDiversMod, new[] { ModNames.BossChecklist, ModNames.BossesAsNPCs, ModNames.ThoriumMod } },
		{ ModNames.ThoriumMod, new[] { ModNames.Bangarang, ModNames.BetterTaxes, ModNames.BossChecklist, ModNames.BossesAsNPCs, ModNames.CalamityMod, ModNames.Census, ModNames.ColoredDamageTypes, ModNames.Fargowiltas, ModNames.HEROsMod, ModNames.RecipeBrowser, ModNames.SummonersAssociation, /*ModNames.YABHB,*/  ModNames.Wikithis, ModNames.WWeaponScaling } }
	};

	#region Config Fields

	private static Dictionary<string, FieldInfo> _baseModToField;

	// Support flag by two mod names.
	// Keyed: _supportFlagByModNames["PrimeRework"]["ThoriumMod"]
	private static Dictionary<string, Dictionary<string, FieldInfo>> _supportFlagByModNames;

	private static ModConfig _config;

	#endregion Config Fields

	private static void SuperEarlyChanges(RandomModCompat mod)
	{
		PopulateAddedSupport(mod);
		SetupConfig(mod);
	}

	private static void PopulateAddedSupport(Mod mod)
	{
		IEnumerable<IAddSupport> supporters = AssemblyManager.GetLoadableTypes(mod.Code)
			.Where(t => t.GetInterface(nameof(IAddSupport)) is not null && !t.IsInterface && !t.IsAbstract)
			.Select(t => Activator.CreateInstance(t) as IAddSupport);

		EnsureModulesDontSupportThemselves(supporters);

		foreach (IAddSupport supporter in supporters)
		{
			if (!AddedSupport.TryGetValue(supporter.BaseMod, out ICollection<string> supportMods))
			{
				supportMods = AddedSupport[supporter.BaseMod] = new List<string>();
			}

			supportMods.Add(supporter.SupportMod);
		}

		// If there are multiple modules for a pair of mods, make sure they aren't added to the list multiple times.
		// Also, sort the dict. so that that values look nice in the config.
		foreach ((string baseMod, ICollection<string> supportMods) in AddedSupport)
		{
			AddedSupport[baseMod] = supportMods.Distinct().OrderBy(s => s).ToArray();
		}

		AddedSupport = AddedSupport.OrderBy(pair => pair.Key)
			.ToDictionary(pair => pair.Key, pair => pair.Value);
	}

	private static void EnsureModulesDontSupportThemselves(IEnumerable<IAddSupport> supporters)
	{
		List<string> issues = new();
		foreach (IAddSupport supporter in supporters)
		{
			if (supporter.BaseMod == supporter.SupportMod)
			{
				issues.Add($"{supporter.GetType().Name}: {supporter.BaseMod} - {supporter.SupportMod}");
			}
		}

		if (issues.Count != 0)
		{
			throw new Exception($"Some modules are supporting themsleves!{Environment.NewLine}{string.Join(Environment.NewLine, issues)}");
		}
	}

	private static void SetupConfig(RandomModCompat mod)
	{
		Type configType = SupportConfigBuilder.Create(mod.DynamicAssembly.ModuleBuilder, AddedSupport);

		FieldInfo[] fields = configType.GetFields();

		_baseModToField = fields.ToDictionary(f => f.Name);
		_supportFlagByModNames = fields.ToDictionary(
			   f => f.Name,
			   f => f.FieldType.GetFields().ToDictionary(inner => inner.Name));

		_config = Activator.CreateInstance(configType) as ModConfig;
		mod.AddConfig(configType.Name, _config);
	}

	/// <summary>
	/// Determines if <paramref name="supportMod"/> support for <paramref name="baseMod"/> is enabled.
	/// </summary>
	/// <param name="baseMod">The base mod, the one that adds the content.</param>
	/// <param name="supportMod">The support mod, the one that adds the systems.</param>
	/// <param name="ignoreEnabledMod">If <see langword="true"/>, then this method will return <see langword="true"/> even if one of the checked mods is disabled.</param>
	/// <returns><see langword="true"/> if both mods are enabled (unless <paramref name="ignoreEnabledMod"/> is <see langword="true"/>) and support is enabled, <see langword="false"/> otherwise.</returns>
	public static bool SupportEnabled(string baseMod, string supportMod, bool ignoreEnabledMod = false)
	{
		if (!ignoreEnabledMod)
		{
			if (!AddedSupport.TryGetValue(baseMod, out ICollection<string> possibleSupportMods) || !possibleSupportMods.Contains(supportMod))
			{
				RandomModCompat.Instance.Logger.Debug($"HEY DUMBASS: SupportEnabled called with mods {baseMod}, {supportMod}, which aren't in the config.");
			}

			if (!ModLoader.HasMod(baseMod) || !ModLoader.HasMod(supportMod))
			{
				return false;
			}
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

	internal static string CreateTestingEnabledList()
	{
		IEnumerable<string> mods = AddedSupport.SelectMany(pair => pair.Value)
			.Concat(AddedSupport.Keys)
			.Append(nameof(RandomModCompat)) // Include this mod in the list
			.Distinct();

		StringBuilder builder = new();
		builder.Append("[\r\n");
		foreach (string mod in mods)
		{
			builder.Append("  \"");
			builder.Append(mod);
			builder.Append('"');
			if (mod != nameof(RandomModCompat)) // Guaranteed to be the last element
			{
				builder.Append(',');
			}
			builder.Append("\r\n");
		}
		builder.Append(']');
		return builder.ToString();
	}

	#region Super Early Hook

	public static void PrepareSuperEarlyChanges()
	{
		const string AutoloadConfig = nameof(AutoloadConfig);
		MethodBase autoloadConfig = typeof(Mod).GetMethod(AutoloadConfig, ReflectionHelper.AllFlags)
			?? throw new MethodAccessException("Cannot find Mod::AutoloadConfig(), cannot create this mod's config.");

#if TML_2022_09
		HookEndpointManager.Add(autoloadConfig, AutoloadConfigHook);
#else
		MonoModHooks.Add(autoloadConfig, AutoloadConfigHook);
#endif
	}

	private static void AutoloadConfigHook(Action<Mod> orig, Mod mod)
	{
		if (mod is RandomModCompat randomModCompat)
		{
			SuperEarlyChanges(randomModCompat);
		}

		orig(mod);
	}

	#endregion Super Early Hook
}