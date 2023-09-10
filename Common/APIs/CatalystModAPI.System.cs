using CatalystMod.Items.Ammo.Lens;
using Microsoft.Xna.Framework;
using RandomModCompat.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

#if TML_2022_09
using MonoMod.RuntimeDetour.HookGen;
#endif

namespace RandomModCompat.Common.APIs;

internal sealed partial class CatalystModAPI
{
	[JITWhenModsEnabled(ModNames.CatalystMod)]
	private sealed class CatalystModAPISystem : ModSystem
	{
		/// <summary>
		/// Custom lens colors added by this mod, keyed by item types.
		/// </summary>
		private static readonly Dictionary<int, Color> _customLensColors = new();

		/// <summary>
		/// The method that determines the color of a player's flashlight beam.
		/// </summary>
		private static MethodInfo _getColorByItemType;

		public override bool IsLoadingEnabled(Mod mod)
		{
			return ModLoader.HasMod(ModNames.CatalystMod) && base.IsLoadingEnabled(mod);
		}

		public override void Load()
		{
			_getColorByItemType = typeof(LensColors).GetMethod("GetColorByItemType", ReflectionHelper.AllFlags);
#if TML_2022_09
			HookEndpointManager.Add(_getColorByItemType, GetCustomLensColors);
#else
			MonoModHooks.Add(_getColorByItemType, GetCustomLensColors);
#endif
		}

		public override void Unload()
		{
#if TML_2022_09
			HookEndpointManager.Remove(_getColorByItemType, GetCustomLensColors);
#endif
			_getColorByItemType = null;
		}

		private static Color GetCustomLensColors(Func<int, Player, Color> orig, int type, Player owner)
		{
			return _customLensColors.TryGetValue(type, out Color customColor) ? customColor : orig(type, owner);
		}

		/// <summary>
		/// Creates a new flashlight lens and adds it to <paramref name="mod"/>.
		/// </summary>
		/// <param name="mod">The mod to add the new lens to.</param>
		/// <param name="gemItemId">The item ID of the gem to add a lens for.</param>
		/// <param name="beamColor">The color of the new lens.</param>
		internal static void AddWulfrumFlashlightLens(Mod mod, int gemItemId, Color beamColor)
		{
			string internalName = ItemLoader.GetItem(gemItemId).Name;
			GenericFlashlightLens lens = new(gemItemId, internalName, beamColor);
			mod.AddContent(lens);
			_customLensColors.Add(lens.Type, beamColor);
			LensColors.LensTypes.Add(lens.Type);
		}
	}
}