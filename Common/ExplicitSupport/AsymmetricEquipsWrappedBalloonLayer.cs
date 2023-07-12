using Microsoft.Xna.Framework;
using RandomModCompat.Common.Callers;
using RandomModCompat.Core;
using RandomModCompat.Utilities;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace RandomModCompat.Common.ExplicitSupport;

/// <summary>
/// A wrapper for mods that use their own <see cref="PlayerDrawLayer"/>s to draw balloons.
/// </summary>
public sealed class AsymmetricEquipsWrappedBalloonLayer : PlayerDrawLayer
{
	private static readonly List<(string ModName, PlayerDrawLayer OriginalLayer, MethodInfo DrawMethod)> _wrappedLayersInfo = new();

	public override bool IsLoadingEnabled(Mod mod)
	{
		return ModLoader.HasMod(ModNames.AsymmetricEquips);
	}

	/// <summary>
	/// Wraps a <see cref="PlayerDrawLayer"/> that draws balloons, allowing them to work with the front balloon system.
	/// </summary>
	/// <typeparam name="T">The <see cref="PlayerDrawLayer"/> to wrap.</typeparam>
	public static void RegisterBalloonDrawLayer<T>() where T : PlayerDrawLayer
	{
		PlayerDrawLayer instance = ModContent.GetInstance<T>();
		MethodInfo drawMethod = instance.GetType().GetMethod(nameof(Draw), ReflectionHelper.AllFlags);
		_wrappedLayersInfo.Add((instance.Mod.Name, instance, drawMethod));
	}

	public override Position GetDefaultPosition()
	{
		return new Between(PlayerDrawLayers.OffhandAcc, PlayerDrawLayers.ArmOverItem);
	}

	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
	{
		return _wrappedLayersInfo.Count > 0;
	}

	// Copied and adapted from AsymmetricEquips.Common.PlayerLayers.FrontBalloonPlayerLayer.
	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		foreach ((string modName, PlayerDrawLayer layer, MethodInfo drawMethod) in _wrappedLayersInfo)
		{
			if (/*!layer.Visible ||*/ !CrossModHandler.TryGetCaller(modName, out AsymmetricEquipsCaller caller))
			{
				continue;
			}

			Player drawPlayer = drawInfo.drawPlayer;
			(int frontBalloon, int cFrontBalloon, _, _, Vector2 offset) = caller.GetFrontBalloon(drawPlayer);

			// Normal balloon drawing logic needs:
			// - drawInfo.drawPlayer.balloon
			// - drawInfo.drawPlayer.cBalloon
			// - drawInfo.Position
			// After changing these, we can just call the original draw method.

			(sbyte origBalloon, int origCBalloon, Vector2 origPosition) = (drawPlayer.balloon, drawPlayer.cBalloon, drawInfo.Position);

			drawPlayer.balloon = (sbyte)frontBalloon;
			drawPlayer.cBalloon = cFrontBalloon;
			drawInfo.Position += offset;

			if (layer.GetDefaultVisibility(drawInfo))
			{
				// Call the original layer's draw method.
				object[] parameters = new object[] { drawInfo };
				drawMethod.Invoke(layer, parameters);
				drawInfo = (PlayerDrawSet)parameters[0]; // ref params, fun
			}

			// Reset back to normal.
			drawPlayer.balloon = origBalloon;
			drawPlayer.cBalloon = origCBalloon;
			drawInfo.Position = origPosition;
		}
	}
}