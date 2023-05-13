using Microsoft.Xna.Framework;
using RandomModCompat.Common;
using RandomModCompat.Common.Callers;
using RandomModCompat.Core;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using ThoriumMod.PlayerLayers;

namespace RandomModCompat.Content.ThoriumModSupport;

// Thorium draws some of its balloons a special way, so we need extra code to make them asymmetric.
[JITWhenModsEnabled(BaseMod)]
internal sealed class ThoriumAsymmetricBalloonLayer : PlayerDrawLayer
{
	private const string BaseMod = "ThoriumMod";
	private const string SupportMod = "AsymmetricEquips";
	private static MethodInfo _thoriumBalloonLayerDraw;

	public override bool IsLoadingEnabled(Mod mod)
	{
		return ModContent.GetInstance<ModSupportConfig>().SupportEnabled(BaseMod, SupportMod);
	}

	public override void Load()
	{
		_thoriumBalloonLayerDraw = typeof(BalloonLayer).GetMethod(nameof(Draw), BindingFlags.Instance | BindingFlags.NonPublic);
	}

	public override Position GetDefaultPosition()
	{
		return new Between(PlayerDrawLayers.OffhandAcc, PlayerDrawLayers.ArmOverItem);
	}

	// Copied and adapted from AsymmetricEquips.Common.PlayerLayers.FrontBalloonPlayerLayer.
	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		CrossModHandler.TryGetCaller<AsymmetricEquipsCaller>(BaseMod, out var caller);
		Player drawPlayer = drawInfo.drawPlayer;
		(int frontBalloon, int cFrontBalloon, _, _, Vector2 offset) = caller.GetFrontBalloon(drawPlayer);

		// Thorium's logic needs:
		// - drawInfo.drawPlayer.balloon
		// - drawInfo.drawPlayer.cBalloon
		// - drawInfo.Position
		// After changing these, we can just call Thorium's draw method.

		(sbyte origBalloon, int origCBalloon, Vector2 origPosition) = (drawPlayer.balloon, drawPlayer.cBalloon, drawInfo.Position);

		drawPlayer.balloon = (sbyte)frontBalloon;
		drawPlayer.cBalloon = cFrontBalloon;
		drawInfo.Position += offset;

		// Reflection is needed to get Thorium's draw layer's Draw method.
		// The method is protected, and Thorium uses `Mod.Assets.Request` instead of `ModContent.Request`, so this is a bit of a pain.
		// PlayerDrawLayer.DrawWithTransformationAndChildren has side effects.
		object[] parameters = new object[] { drawInfo };
		_thoriumBalloonLayerDraw.Invoke(ModContent.GetInstance<BalloonLayer>(), parameters);
		drawInfo = (PlayerDrawSet)parameters[0]; // ref params, fun

		// Reset back to normal.
		drawPlayer.balloon = origBalloon;
		drawPlayer.cBalloon = origCBalloon;
		drawInfo.Position = origPosition;
	}
}