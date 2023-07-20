using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour.HookGen;
using MonoMod.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace RandomModCompat.Common.ExplicitSupport;

[JITWhenModsEnabled(ModNames.levelplus)]
internal sealed class LevelplusSupportSystem : ModPlayer
{
	internal readonly struct StatBonus
	{
		internal delegate void ApplyBonus(Player player, ushort statValue);

		internal delegate string GetDescription(ushort statValue);

		internal Stat Stat { get; init; }
		internal ApplyBonus Apply { get; init; }
		internal GetDescription Description { get; init; }

		public StatBonus(Stat stat, ApplyBonus apply, GetDescription description)
		{
			Stat = stat;
			Apply = apply;
			Description = description;
		}
	}

	internal enum Stat
	{
		Constitution,
		Strength,
		Intelligence,
		Charisma,
		Dexterity,
		Mobility,
		Excavation,
		Animalia,
		Luck,
		Mysticism
	}

	#region StatButton Reflection

	// Internal class :(
	private delegate void orig_StatButtonUpdate(UIElement self, GameTime gameTime);

	private delegate void hook_StatButtonUpdate(orig_StatButtonUpdate orig, UIElement self, GameTime gameTime);

	private static event ILContext.Manipulator StatButtonUpdate
	{
		add
		{
			HookEndpointManager.Modify<hook_StatButtonUpdate>(_statButtonUpdate, value);
		}
		remove
		{
			HookEndpointManager.Unmodify<hook_StatButtonUpdate>(_statButtonUpdate, value);
		}
	}

	private static Type _statButton;
	private static MethodBase _statButtonUpdate;
	private static MethodBase _statButtonGetType;

	#endregion StatButton Reflection

	#region LevelPlusModPlayer Reflection

	private static ModPlayer _levelPlusPlayerInstance;
	private static Type _levelPlusModPlayer;
	private static IDictionary<Stat, MethodInfo> _statToGetter;

	private static ushort GetStatValue(Stat stat, Player player = null)
	{
		player ??= Main.LocalPlayer;
		ModPlayer modPlayer = player.GetModPlayer(_levelPlusPlayerInstance);
		return (ushort)_statToGetter[stat].Invoke(modPlayer, Array.Empty<object>());
	}

	#endregion LevelPlusModPlayer Reflection

	private static readonly IList<StatBonus> _bonuses = new List<StatBonus>();

	public override bool IsLoadingEnabled(Mod mod)
	{
		// 1.2.0 changes how stats are stored, but it isn't released yet.
		// https://github.com/PoctorDepper/LevelPlus/compare/master...1.2.0
		bool modLoaded = ModLoader.TryGetMod(ModNames.levelplus, out Mod levelPlus);
		if (levelPlus?.Version >= new Version(1, 2, 0))
		{
			Mod.Logger.Warn("Level+ support cannot be loaded on v1.2.0 or higher! Pester this mod's dev to update support for Level+ v1.2.0!");
		}
		return modLoaded;
	}

	public override void Load()
	{
		_levelPlusModPlayer = ModLoader.GetMod(ModNames.levelplus).Code.GetType("levelplus.levelplusModPlayer");
		_statToGetter = new Dictionary<Stat, MethodInfo>()
		{
			{ Stat.Constitution, _levelPlusModPlayer.GetProperty("constitution").GetGetMethod() },
			{ Stat.Strength, _levelPlusModPlayer.GetProperty("strength").GetGetMethod() },
			{ Stat.Intelligence, _levelPlusModPlayer.GetProperty("intelligence").GetGetMethod() },
			{ Stat.Charisma, _levelPlusModPlayer.GetProperty("charisma").GetGetMethod() },
			{ Stat.Dexterity, _levelPlusModPlayer.GetProperty("dexterity").GetGetMethod() },
			{ Stat.Mobility, _levelPlusModPlayer.GetProperty("mobility").GetGetMethod() },
			{ Stat.Excavation, _levelPlusModPlayer.GetProperty("excavation").GetGetMethod() },
			{ Stat.Animalia, _levelPlusModPlayer.GetProperty("animalia").GetGetMethod() },
			{ Stat.Luck, _levelPlusModPlayer.GetProperty("luck").GetGetMethod() },
			{ Stat.Mysticism, _levelPlusModPlayer.GetProperty("mysticism").GetGetMethod() }
		};
		_statButton = ModLoader.GetMod(ModNames.levelplus).Code.GetType("levelplus.UI.StatButton");
		_statButtonUpdate = _statButton.GetMethod("Update", Utilities.ReflectionHelper.AllFlags);
		_statButtonGetType = _statButton.GetProperty("type", Utilities.ReflectionHelper.AllFlags).GetGetMethod();

		StatButtonUpdate += AddTooltips;
	}

	public override void SetStaticDefaults()
	{
		if (!ModContent.TryFind(ModNames.levelplus, "levelplusModPlayer", out _levelPlusPlayerInstance))
		{
			throw new Exception("Cannot find ModPlayer, cannot load Level+ compatibility.");
		}
	}

	#region UI Tooltip

	private static void AddTooltips(ILContext il)
	{
		ILCursor c = new(il);

		// There's only one string local in the method, so find it.
		int textIndex = il.Body.Variables.FirstOrDefault(v => v.VariableType.ResolveReflection() == typeof(string))?.Index ?? -1;
		if (textIndex == -1)
		{
			throw new Exception("Can't find text.");
		}

		// Find (C#):
		//	Main.instance...
		// Find (IL):
		//	ldsfld class [tModLoader]Terraria.Main [tModLoader]Terraria.Main::'instance'
		// This is where text is rendered, and thus the last chance to change it.
		if (!c.TryGotoNext(MoveType.Before,
			i => i.MatchLdsfld<Main>(nameof(Main.instance))
			))
		{
			throw new Exception("Can't find MouseText.");
		}

		// text = ModifyText(text, type);
		c.Emit(OpCodes.Ldloc, textIndex);
		c.Emit(OpCodes.Ldarg_0);
		c.Emit(OpCodes.Call, _statButtonGetType);
		c.EmitDelegate(ModifyText);
		c.Emit(OpCodes.Stloc, textIndex);
	}

	private static string ModifyText(string unmodified, Stat stat)
	{
		IEnumerable<string> renderedDescriptions = _bonuses
			.Where(d => stat == d.Stat)
			.Select(d => d.Description(GetStatValue(stat)));
		return string.Join("\n", unmodified, string.Join("\n", renderedDescriptions));
	}

	#endregion UI Tooltip

	public override void ResetEffects()
	{
		foreach (StatBonus bonus in _bonuses)
		{
			bonus.Apply(Player, GetStatValue(bonus.Stat, Player));
		}
	}

	internal static void AddEffect(Stat stat, StatBonus.ApplyBonus effect, StatBonus.GetDescription description)
	{
		_bonuses.Add(new(stat, effect, description));
	}

	internal static void AddDamageAndCritEffects(Stat stat, DamageClass damageClass, Func<float> damagePerPoint = null, Func<int> pointsPerCrit = null)
	{
		damagePerPoint ??= () => 0.01f;
		pointsPerCrit ??= () => 15;

		AddEffect(stat,
			(player, statValue) => player.GetDamage(damageClass) *= 1f + (damagePerPoint() * statValue),
			statValue => Language.GetTextValueWith("Mods.RandomModCompat.LevelPlus.AddDamage",
				new { Amount = (int)(statValue * (damagePerPoint() * 100)), DamageType = damageClass.DisplayName }));
		AddEffect(stat,
			(player, statValue) => player.GetCritChance(damageClass) += statValue / pointsPerCrit(),
			statValue => Language.GetTextValueWith("Mods.RandomModCompat.LevelPlus.AddCrit",
				new { Amount = statValue / pointsPerCrit(), DamageTypeNoDamage = StripDamageFromClassName(damageClass) }));
	}


	// tML requires that the word "damage" be included in damage class display names, so there isn't a localized version of the just the class name.
	private static string StripDamageFromClassName(DamageClass damageClass)
	{
		if (Language.ActiveCulture == GameCulture.FromCultureName(GameCulture.CultureName.English))
		{
			// Remove the word "damage".
			return damageClass.DisplayName.Replace("damage", null).Trim();
		}
		else
		{
			// No idea how to handle this.
			return damageClass.DisplayName;
		}
	}
}