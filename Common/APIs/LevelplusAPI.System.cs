﻿using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using MonoMod.RuntimeDetour.HookGen;
using MonoMod.Utils;
using RandomModCompat.Common.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

internal sealed partial class LevelplusAPI
{
	[JITWhenModsEnabled(ModNames.levelplus)]
	private sealed class LevelplusSupportSystem : ModPlayer
	{
		#region StatButton Reflection

#if TML_2022_09
		private const string _modPlayerName = "levelplusModPlayer";
#else
		private const string _modPlayerName = "LevelPlusModPlayer";
		private static ILHook _statButtonUpdateHook;
#endif

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

			if (player.TryGetModPlayer(_levelPlusPlayerInstance, out ModPlayer mPlayer))
			{
				return (ushort)_statToGetter[stat].Invoke(mPlayer, Array.Empty<object>());
			}
			else
			{
				return 0;
			}
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
				mod.Logger.Warn("Level+ support cannot be loaded on v1.2.0 or higher! Pester this mod's dev to update support for Level+ v1.2.0!");
				return false;
			}

			// This support system requires IL edits.
			if (ModContent.GetInstance<RandomModCompatConfig>().DisableIL)
			{
				mod.Logger.Info("Level+ support disabled because IL edits are disabled.");
				return false;
			}

			return modLoaded && base.IsLoadingEnabled(mod);
		}

		public override void Load()
		{
			_levelPlusModPlayer = ModLoader.GetMod(ModNames.levelplus).Code.GetType(ModNames.levelplus + "." + _modPlayerName);
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
			_statButton = ModLoader.GetMod(ModNames.levelplus).Code.GetType(ModNames.levelplus + ".UI.StatButton");
			_statButtonUpdate = _statButton.GetMethod("Update", Utilities.ReflectionHelper.AllFlags);
			_statButtonGetType = _statButton.GetProperty("type", Utilities.ReflectionHelper.AllFlags).GetGetMethod();

#if TML_2022_09
			HookEndpointManager.Modify(_statButtonUpdate, AddTooltips);
		}

		public override void Unload()
		{
			HookEndpointManager.Unmodify(_statButtonUpdate, AddTooltips);
		}
#else
			_statButtonUpdateHook = new(_statButtonUpdate, AddTooltips);
		}

		public override void Unload()
		{
			_statButtonUpdateHook?.Undo();
		}

#endif

		public override void SetStaticDefaults()
		{
			if (!ModContent.TryFind(ModNames.levelplus, _modPlayerName, out _levelPlusPlayerInstance))
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

		internal static void AddEffect(Stat stat, ApplyBonus effect, GetDescription description)
		{
			_bonuses.Add(new(stat, effect, description));
		}
	}
}