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
	private const string _localizationPrefix = $"$Mods.{nameof(RandomModCompat)}._Config.";
	private const string _modNamesPrefix = _localizationPrefix + "ModNames.";

	// All fields should be named like the corresponding mod's internal name.
	[Label(_modNamesPrefix + ModNames.BalloonsExtended)]
	public sealed class BalloonsExtendedSupportConfig
	{
		[Label(_modNamesPrefix + ModNames.AsymmetricEquips)]
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

	[Label(_modNamesPrefix + ModNames.Bangarang)]
	public sealed class BangarangSupportConfig
	{
		[Label(_modNamesPrefix + ModNames.TerraTyping)]
		public bool TerraTyping;

		[Label(_modNamesPrefix + ModNames.WWeaponScaling)]
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

	[Label(_modNamesPrefix + ModNames.CalamityMod)]
	public sealed class CalamityModSupportConfig
	{
		[Label(_modNamesPrefix + ModNames.AsymmetricEquips)]
		public bool AsymmetricEquips;

		[Label(_modNamesPrefix + ModNames.Bangarang)]
		public bool Bangarang;

		[Label(_modNamesPrefix + ModNames.BuffDisplay)]
		public bool BuffDisplay;

		[Label(_modNamesPrefix + ModNames.ImprovedTorches)]
		public bool ImprovedTorches;

		[Label(_modNamesPrefix + ModNames.ItemCheckBlacklist)]
		public bool ItemCheckBlacklist;

		[Label(_modNamesPrefix + ModNames.ThoriumMod)]
		public bool ThoriumMod;

		[Label(_modNamesPrefix + ModNames.UniversalCraft)]
		public bool UniversalCraft;

		public override bool Equals(object obj)
		{
			return obj is CalamityModSupportConfig other
				&& AsymmetricEquips.Equals(other.AsymmetricEquips)
				&& Bangarang.Equals(other.Bangarang)
				&& BuffDisplay.Equals(other.BuffDisplay)
				&& ImprovedTorches.Equals(other.ImprovedTorches)
				&& ItemCheckBlacklist.Equals(other.ItemCheckBlacklist)
				&& ThoriumMod.Equals(other.ThoriumMod)
				&& UniversalCraft.Equals(other.UniversalCraft);
		}

		public override int GetHashCode()
		{
			return new { AsymmetricEquips, Bangarang, BuffDisplay, ImprovedTorches, ItemCheckBlacklist, ThoriumMod, UniversalCraft }.GetHashCode();
		}
	}

	[Label(_modNamesPrefix + ModNames.ItReallyMustBe)]
	public sealed class ItReallyMustBeSupportConfig
	{
		[Label(_modNamesPrefix + ModNames.BossesAsNPCs)]
		public bool BossesAsNPCs;

		[Label(_modNamesPrefix + ModNames.ItemCheckBlacklist)]
		public bool ItemCheckBlacklist;

		[Label(_modNamesPrefix + ModNames.ROR2HealthBars)]
		public bool ROR2HealthBars;

		[Label(_modNamesPrefix + ModNames.TerraTyping)]
		public bool TerraTyping;

		[Label(_modNamesPrefix + ModNames.WWeaponScaling)]
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

	[Label(_modNamesPrefix + ModNames.Liber)]
	public sealed class LiberSupportConfig
	{
		[Label(_modNamesPrefix + ModNames.AsymmetricEquips)]
		public bool AsymmetricEquips;

		[Label(_modNamesPrefix + ModNames.ImprovedTorches)]
		public bool ImprovedTorches;

		[Label(_modNamesPrefix + ModNames.ThoriumMod)]
		public bool ThoriumMod;

		[Label(_modNamesPrefix + ModNames.UniversalCraft)]
		public bool UniversalCraft;

		[Label(_modNamesPrefix + ModNames.WWeaponScaling)]
		public bool WWeaponScaling;

		public override bool Equals(object obj)
		{
			return obj is LiberSupportConfig other
				&& AsymmetricEquips.Equals(other.AsymmetricEquips)
				&& ImprovedTorches.Equals(other.ImprovedTorches)
				&& ThoriumMod.Equals(other.ThoriumMod)
				&& UniversalCraft.Equals(other.UniversalCraft)
				&& WWeaponScaling.Equals(other.WWeaponScaling);
		}

		public override int GetHashCode()
		{
			return new { AsymmetricEquips, ImprovedTorches, ThoriumMod, UniversalCraft, WWeaponScaling }.GetHashCode();
		}
	}

	[Label(_modNamesPrefix + ModNames.Mask)]
	public sealed class MaskSupportConfig
	{
		[Label(_modNamesPrefix + ModNames.AsymmetricEquips)]
		public bool AsymmetricEquips;

		[Label(_modNamesPrefix + ModNames.Census)]
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

	[Label(_modNamesPrefix + ModNames.PrimeRework)]
	public sealed class PrimeReworkSupportConfig
	{
		[Label(_modNamesPrefix + ModNames.Bangarang)]
		public bool Bangarang;

		[Label(_modNamesPrefix + ModNames.BossesAsNPCs)]
		public bool BossesAsNPCs;

		[Label(_modNamesPrefix + ModNames.ItemCheckBlacklist)]
		public bool ItemCheckBlacklist;

		[Label(_modNamesPrefix + ModNames.MagicStorage)]
		public bool MagicStorage;

		[Label(_modNamesPrefix + ModNames.ROR2HealthBars)]
		public bool ROR2HealthBars;

		[Label(_modNamesPrefix + ModNames.TerraTyping)]
		public bool TerraTyping;

		[Label(_modNamesPrefix + ModNames.ThoriumMod)]
		public bool ThoriumMod;

		[Label(_modNamesPrefix + ModNames.WWeaponScaling)]
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

	[Label(_modNamesPrefix + ModNames.SlugNPCs)]
	public sealed class SlugNPCsSupportConfig
	{
		[Label(_modNamesPrefix + ModNames.Census)]
		public bool Census;

		[Label(_modNamesPrefix + ModNames.DialogueTweak)]
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

	[Label(_modNamesPrefix + ModNames.ThoriumMod)]
	public sealed class ThoriumModSupportConfig
	{
		[Label(_modNamesPrefix + ModNames.AmuletOfManyMinions)]
		public bool AmuletOfManyMinions;

		[Label(_modNamesPrefix + ModNames.AsymmetricEquips)]
		public bool AsymmetricEquips;

		[Label(_modNamesPrefix + ModNames.BuffDisplay)]
		public bool BuffDisplay;

		[Label(_modNamesPrefix + ModNames.DialogueTweak)]
		public bool DialogueTweak;

		[Label(_modNamesPrefix + ModNames.ImprovedTorches)]
		public bool ImprovedTorches;

		[Label(_modNamesPrefix + ModNames.ItemCheckBlacklist)]
		public bool ItemCheckBlacklist;

		[Label(_modNamesPrefix + ModNames.MagicStorage)]
		public bool MagicStorage;

		[Label(_modNamesPrefix + ModNames.OverpoweredGoldDust)]
		public bool OverpoweredGoldDust;

		[Label(_modNamesPrefix + ModNames.RescueFairies)]
		public bool RescueFairies;

		[Label(_modNamesPrefix + ModNames.ROR2HealthBars)]
		public bool ROR2HealthBars;

		[Label(_modNamesPrefix + ModNames.UniversalCraft)]
		public bool UniversalCraft;

		public override bool Equals(object obj)
		{
			return obj is ThoriumModSupportConfig other
				&& AmuletOfManyMinions.Equals(other.AmuletOfManyMinions)
				&& AsymmetricEquips.Equals(other.AsymmetricEquips)
				&& BuffDisplay.Equals(other.BuffDisplay)
				&& DialogueTweak.Equals(other.DialogueTweak)
				&& ImprovedTorches.Equals(other.ImprovedTorches)
				&& ItemCheckBlacklist.Equals(other.ItemCheckBlacklist)
				&& MagicStorage.Equals(other.MagicStorage)
				&& OverpoweredGoldDust.Equals(other.OverpoweredGoldDust)
				&& RescueFairies.Equals(other.RescueFairies)
				&& ROR2HealthBars.Equals(other.ROR2HealthBars)
				&& UniversalCraft.Equals(other.UniversalCraft);
		}

		public override int GetHashCode()
		{
			return new { AmuletOfManyMinions, AsymmetricEquips, BuffDisplay, DialogueTweak, ImprovedTorches, ItemCheckBlacklist, MagicStorage, OverpoweredGoldDust, RescueFairies, ROR2HealthBars, UniversalCraft }.GetHashCode();
		}
	}

	public override ConfigScope Mode => ConfigScope.ServerSide;

	[Header(_localizationPrefix + "Info")]
	public BalloonsExtendedSupportConfig BalloonsExtended;

	public BangarangSupportConfig Bangarang;
	public CalamityModSupportConfig CalamityMod;
	public ItReallyMustBeSupportConfig ItReallyMustBe;
	public LiberSupportConfig Liber;
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