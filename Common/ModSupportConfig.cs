using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;

namespace RandomModCompat.Common;

public sealed class ModSupportConfig : ModConfig
{
	private const string _localizationHeader = $"$Mods.{nameof(RandomModCompat)}._Config.";
	private const string _modNames = _localizationHeader + "ModNames.";

	// All fields should be named like the corresponding mod's internal name.
	[Label(_modNames + "BalloonsExtended")]
	public sealed class BalloonsExtendedSupportConfig
	{
		[Label(_modNames + "AsymmetricEquips")]
		public bool AsymmetricEquips;

		public override bool Equals(object obj)
		{
			return obj is BalloonsExtendedSupportConfig other
				&& AsymmetricEquips.Equals(other.AsymmetricEquips);
		}

		public override int GetHashCode()
		{
			return new { AsymmetricEquips }.GetHashCode();
		}
	}

	[Label(_modNames + "Bangarang")]
	public sealed class BangarangSupportConfig
	{
		[Label(_modNames + "TerraTyping")]
		public bool TerraTyping;

		[Label(_modNames + "WWeaponScaling")]
		public bool WWeaponScaling;

		public override bool Equals(object obj)
		{
			return obj is BangarangSupportConfig other
				&& TerraTyping.Equals(other.TerraTyping)
				&& WWeaponScaling.Equals(other.WWeaponScaling);
		}

		public override int GetHashCode()
		{
			return new { TerraTyping, WWeaponScaling }.GetHashCode();
		}
	}

	[Label(_modNames + "CalamityMod")]
	public sealed class CalamityModSupportConfig
	{
		[Label(_modNames + "AsymmetricEquips")]
		public bool AsymmetricEquips;

		[Label(_modNames + "Bangarang")]
		public bool Bangarang;

		[Label(_modNames + "BuffDisplay")]
		public bool BuffDisplay;

		[Label(_modNames + "ItemCheckBlacklist")]
		public bool ItemCheckBlacklist;

		[Label(_modNames + "ThoriumMod")]
		public bool ThoriumMod;

		[Label(_modNames + "UniversalCraft")]
		public bool UniversalCraft;

		public override bool Equals(object obj)
		{
			return obj is CalamityModSupportConfig other
				&& AsymmetricEquips.Equals(other.AsymmetricEquips)
				&& Bangarang.Equals(other.Bangarang)
				&& BuffDisplay.Equals(other.BuffDisplay)
				&& ItemCheckBlacklist.Equals(other.ItemCheckBlacklist)
				&& ThoriumMod.Equals(other.ThoriumMod)
				&& UniversalCraft.Equals(other.UniversalCraft);
		}

		public override int GetHashCode()
		{
			return new { AsymmetricEquips, Bangarang, BuffDisplay, ItemCheckBlacklist, ThoriumMod, UniversalCraft }.GetHashCode();
		}
	}

	[Label(_modNames + "ItReallyMustBe")]
	public sealed class ItReallyMustBeSupportConfig
	{
		[Label(_modNames + "BossesAsNPCs")]
		public bool BossesAsNPCs;

		[Label(_modNames + "ItemCheckBlacklist")]
		public bool ItemCheckBlacklist;

		[Label(_modNames + "ROR2HealthBars")]
		public bool ROR2HealthBars;

		[Label(_modNames + "TerraTyping")]
		public bool TerraTyping;

		[Label(_modNames + "WWeaponScaling")]
		public bool WWeaponScaling;

		public override bool Equals(object obj)
		{
			return obj is ItReallyMustBeSupportConfig other
				&& BossesAsNPCs.Equals(other.BossesAsNPCs)
				&& ItemCheckBlacklist.Equals(other.ItemCheckBlacklist)
				&& ROR2HealthBars.Equals(other.ROR2HealthBars)
				&& TerraTyping.Equals(other.TerraTyping)
				&& WWeaponScaling.Equals(other.WWeaponScaling);
		}

		public override int GetHashCode()
		{
			return new { BossesAsNPCs, ItemCheckBlacklist, ROR2HealthBars, TerraTyping, WWeaponScaling }.GetHashCode();
		}
	}

	[Label(_modNames + "Mask")]
	public sealed class MaskSupportConfig
	{
		[Label(_modNames + "AsymmetricEquips")]
		public bool AsymmetricEquips;

		[Label(_modNames + "Census")]
		public bool Census;

		public override bool Equals(object obj)
		{
			return obj is MaskSupportConfig other
				&& AsymmetricEquips.Equals(other.AsymmetricEquips)
				&& Census.Equals(other.Census);
		}

		public override int GetHashCode()
		{
			return new { AsymmetricEquips, Census }.GetHashCode();
		}
	}

	[Label(_modNames + "PrimeRework")]
	public sealed class PrimeReworkSupportConfig
	{
		[Label(_modNames + "Bangarang")]
		public bool Bangarang;

		[Label(_modNames + "BossesAsNPCs")]
		public bool BossesAsNPCs;

		[Label(_modNames + "ItemCheckBlacklist")]
		public bool ItemCheckBlacklist;

		[Label(_modNames + "MagicStorage")]
		public bool MagicStorage;

		[Label(_modNames + "ROR2HealthBars")]
		public bool ROR2HealthBars;

		[Label(_modNames + "TerraTyping")]
		public bool TerraTyping;

		[Label(_modNames + "ThoriumMod")]
		public bool ThoriumMod;

		[Label(_modNames + "WWeaponScaling")]
		public bool WWeaponScaling;

		public override bool Equals(object obj)
		{
			return obj is PrimeReworkSupportConfig other
				&& Bangarang.Equals(other.Bangarang)
				&& BossesAsNPCs.Equals(other.BossesAsNPCs)
				&& ItemCheckBlacklist.Equals(other.ItemCheckBlacklist)
				&& MagicStorage.Equals(other.MagicStorage)
				&& ROR2HealthBars.Equals(other.ROR2HealthBars)
				&& TerraTyping.Equals(other.TerraTyping)
				&& ThoriumMod.Equals(other.ThoriumMod)
				&& WWeaponScaling.Equals(other.WWeaponScaling);
		}

		public override int GetHashCode()
		{
			return new { Bangarang, BossesAsNPCs, ItemCheckBlacklist, MagicStorage, ROR2HealthBars, TerraTyping, ThoriumMod, WWeaponScaling }.GetHashCode();
		}
	}

	[Label(_modNames + "SlugNPCs")]
	public sealed class SlugNPCsSupportConfig
	{
		[Label(_modNames + "Census")]
		public bool Census;

		[Label(_modNames + "DialogueTweak")]
		public bool DialogueTweak;

		public override bool Equals(object obj)
		{
			return obj is SlugNPCsSupportConfig other
				&& Census.Equals(other.Census)
				&& DialogueTweak.Equals(other.DialogueTweak);
		}

		public override int GetHashCode()
		{
			return new { Census, DialogueTweak }.GetHashCode();
		}
	}

	[Label(_modNames + "ThoriumMod")]
	public sealed class ThoriumModSupportConfig
	{
		[Label(_modNames + "AsymmetricEquips")]
		public bool AsymmetricEquips;

		[Label(_modNames + "BuffDisplay")]
		public bool BuffDisplay;

		[Label(_modNames + "DialogueTweak")]
		public bool DialogueTweak;

		[Label(_modNames + "ItemCheckBlacklist")]
		public bool ItemCheckBlacklist;

		[Label(_modNames + "MagicStorage")]
		public bool MagicStorage;

		[Label(_modNames + "OverpoweredGoldDust")]
		public bool OverpoweredGoldDust;

		[Label(_modNames + "RescueFairies")]
		public bool RescueFairies;

		[Label(_modNames + "ROR2HealthBars")]
		public bool ROR2HealthBars;

		[Label(_modNames + "UniversalCraft")]
		public bool UniversalCraft;

		public override bool Equals(object obj)
		{
			return obj is ThoriumModSupportConfig other
				&& AsymmetricEquips.Equals(other.AsymmetricEquips)
				&& BuffDisplay.Equals(other.BuffDisplay)
				&& DialogueTweak.Equals(other.DialogueTweak)
				&& ItemCheckBlacklist.Equals(other.ItemCheckBlacklist)
				&& MagicStorage.Equals(other.MagicStorage)
				&& OverpoweredGoldDust.Equals(other.OverpoweredGoldDust)
				&& RescueFairies.Equals(other.RescueFairies)
				&& ROR2HealthBars.Equals(other.ROR2HealthBars)
				&& UniversalCraft.Equals(other.UniversalCraft);
		}

		public override int GetHashCode()
		{
			return new { AsymmetricEquips, BuffDisplay, DialogueTweak, ItemCheckBlacklist, MagicStorage, OverpoweredGoldDust, RescueFairies, ROR2HealthBars, UniversalCraft }.GetHashCode();
		}
	}

	public override ConfigScope Mode => ConfigScope.ServerSide;

	[Header(_localizationHeader + "Info")]

	// All fields should be named like the corresponding mod's internal name.
	
	public BalloonsExtendedSupportConfig BalloonsExtended;
	public BangarangSupportConfig Bangarang;
	public CalamityModSupportConfig CalamityMod;
	public ItReallyMustBeSupportConfig ItReallyMustBe;
	public MaskSupportConfig Mask;
	public PrimeReworkSupportConfig PrimeRework;
	public SlugNPCsSupportConfig SlugNPCs;
	public ThoriumModSupportConfig ThoriumMod;

	// Instance of a field in this class by mod internal name.
	private static readonly Dictionary<string, FieldInfo> _fieldByModName;

	// Support flag by two mod names.
	// Keyed: _supportFlagByModNames["PrimeRework"]["ThoriumMod"]
	private static readonly Dictionary<string, Dictionary<string, FieldInfo>> _supportFlagByModNames;

	static ModSupportConfig()
	{
		Type type = typeof(ModSupportConfig);

		_fieldByModName = type.GetFields(BindingFlags.Public | BindingFlags.Instance)
			.ToDictionary(f => f.Name);

		Type[] nestedTypes = type.GetNestedTypes();
		_supportFlagByModNames = _fieldByModName.ToDictionary(
			   pair => pair.Key,
			   pair =>
				{
					Type nestedType = pair.Value.FieldType;
					return nestedType.GetFields(BindingFlags.Public | BindingFlags.Instance)
						.ToDictionary(f => f.Name);
				});
	}

	public ModSupportConfig()
	{
		// Initialize all subclass instances to all true values.
		foreach (FieldInfo info in _fieldByModName.Values)
		{
			object instance = Activator.CreateInstance(info.FieldType);
			info.SetValue(this, instance);
			foreach (FieldInfo flagInfo in _supportFlagByModNames[info.Name].Values)
			{
				flagInfo.SetValue(instance, true);
			}
		}
	}

	// 99.99% of the support added needs to be reloaded to be (en/dis)abled, so force a reload if anything has changed.
	// Code almost exactly the same as the base impl., just missing the ReloadRequiredAttribute check.
	public override bool NeedsReload(ModConfig pendingConfig)
	{
		foreach (PropertyFieldWrapper fieldsAndProperty in ConfigManager.GetFieldsAndProperties(this))
		{
			if (!ConfigManager.ObjectEquals(fieldsAndProperty.GetValue(this), fieldsAndProperty.GetValue(pendingConfig)))
			{
				return true;
			}
		}

		return false;
	}

	/// <summary>
	/// Determines if <paramref name="supportMod"/> support is enabled for <paramref name="baseMod"/>. (Ex: If Census support is enabled for Thorium Mod).
	/// </summary>
	/// <param name="baseMod">The internal name of the base mod.</param>
	/// <param name="supportMod">The internal name of the support mod.</param>
	/// <param name="justCheckOption">If <see langword="true"/>, this method will not return <see langword="false"/> if one of the checked mods is not enabled.</param>
	/// <returns><see langword="true"/> if both mods are enabled (unless <paramref name="justCheckOption"/> is <see langword="true"/>), if <paramref name="baseMod"/> <i>has</i> support for <paramref name="supportMod"/>, and if that support is enabled. Returns <see langword="false"/> otherwise.</returns>
	[Pure]
	public bool SupportEnabled(string baseMod, string supportMod, bool justCheckOption = false)
	{
		return (justCheckOption || (ModLoader.HasMod(baseMod) && ModLoader.HasMod(supportMod)))
			&& _supportFlagByModNames.TryGetValue(baseMod, out var subDict)
			&& subDict.TryGetValue(supportMod, out var field)
			&& Convert.ToBoolean(field.GetValue(_fieldByModName[baseMod].GetValue(this)));
	}
}