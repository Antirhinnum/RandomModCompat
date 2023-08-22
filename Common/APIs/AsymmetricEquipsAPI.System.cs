using Microsoft.Xna.Framework;
using RandomModCompat.Utilities;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

internal sealed partial class AsymmetricEquipsAPI
{
	/// <summary>
	/// A wrapper for mods that use their own <see cref="PlayerDrawLayer"/>s to draw balloons.
	/// </summary>
	private sealed class AsymmetricEquipsWrappedBalloonLayer : PlayerDrawLayer
	{
		public static readonly List<(string ModName, PlayerDrawLayer OriginalLayer, MethodInfo DrawMethod)> wrappedLayersInfo = new();

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
			wrappedLayersInfo.Add((instance.Mod.Name, instance, drawMethod));
		}

		public override Position GetDefaultPosition()
		{
			return new Between(PlayerDrawLayers.OffhandAcc, PlayerDrawLayers.ArmOverItem);
		}

		public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
		{
			return wrappedLayersInfo.Count > 0;
		}

		// Copied and adapted from AsymmetricEquips.Common.PlayerLayers.FrontBalloonPlayerLayer.
		protected override void Draw(ref PlayerDrawSet drawInfo)
		{
			AsymmetricEquipsAPI api = ModContent.GetInstance<AsymmetricEquipsAPI>();
			if (!api.SupportEnabled())
			{
				return;
			}

			foreach ((string modName, PlayerDrawLayer layer, MethodInfo drawMethod) in wrappedLayersInfo)
			{
				if (/*!layer.Visible ||*/ !RandomModCompat.SupportEnabled(modName, api.ModName))
				{
					continue;
				}

				Player drawPlayer = drawInfo.drawPlayer;
				(int frontBalloon, int cFrontBalloon, _, _, Vector2 offset) = api.GetFrontBalloon(drawPlayer);

				// Normal balloon drawing logic needs:
				// - drawInfo.drawPlayer.balloon
				// - drawInfo.drawPlayer.cBalloon
				// - drawInfo.Position
				// After changing these, we can just call the original draw method.

#if TML_2022_09
				(sbyte origBalloon, int origCBalloon, Vector2 origPosition) = (drawPlayer.balloon, drawPlayer.cBalloon, drawInfo.Position);
#else
				(int origBalloon, int origCBalloon, Vector2 origPosition) = (drawPlayer.balloon, drawPlayer.cBalloon, drawInfo.Position);
#endif

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
}