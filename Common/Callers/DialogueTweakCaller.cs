using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RandomModCompat.Core;
using System;
using System.Collections.Generic;
using Terraria;

namespace RandomModCompat.Common.Callers;

internal sealed class DialogueTweakCaller : ModWithCalls
{
	protected internal override string ModName => "DialogueTweak";

	internal void ReplaceExtraButtonIcon(List<int> npcIds, string texturePath)
	{
		CalledMod.Call(nameof(ReplaceExtraButtonIcon), npcIds, texturePath);
	}

	internal void ReplaceExtraButtonIcon(List<int> npcIds, string texturePath, Func<bool> available, Func<Rectangle> frame)
	{
		CalledMod.Call(nameof(ReplaceExtraButtonIcon), npcIds, texturePath, available, frame);
	}

	internal void ReplaceShopButtonIcon(List<int> npcIds, string texturePath)
	{
		CalledMod.Call(nameof(ReplaceShopButtonIcon), npcIds, texturePath);
	}

	internal void ReplaceShopButtonIcon(List<int> npcIds, string texturePath, Func<bool> available, Func<Rectangle> frame)
	{
		CalledMod.Call(nameof(ReplaceShopButtonIcon), npcIds, texturePath, available, frame);
	}

	internal void ReplaceHappinessButtonIcon(List<int> npcIds, string texturePath)
	{
		CalledMod.Call(nameof(ReplaceHappinessButtonIcon), npcIds, texturePath);
	}

	internal void ReplaceHappinessButtonIcon(List<int> npcIds, string texturePath, Func<bool> available, Func<Rectangle> frame)
	{
		CalledMod.Call(nameof(ReplaceHappinessButtonIcon), npcIds, texturePath, available, frame);
	}

	internal void ReplaceBackButtonIcon(List<int> npcIds, string texturePath)
	{
		CalledMod.Call(nameof(ReplaceBackButtonIcon), npcIds, texturePath);
	}

	internal void ReplaceBackButtonIcon(List<int> npcIds, string texturePath, Func<bool> available, Func<Rectangle> frame)
	{
		CalledMod.Call(nameof(ReplaceBackButtonIcon), npcIds, texturePath, available, frame);
	}

	internal void ReplacePortrait(List<int> npcIds, string texturePath)
	{
		CalledMod.Call(nameof(ReplacePortrait), npcIds, texturePath);
	}

	internal void ReplacePortrait(List<int> npcIds, string texturePath, Func<bool> available, Func<Rectangle> frame)
	{
		CalledMod.Call(nameof(ReplacePortrait), npcIds, texturePath, available, frame);
	}

	internal void OnPostPortraitDraw(Action<SpriteBatch, Color, Rectangle> action)
	{
		CalledMod.Call(nameof(OnPostPortraitDraw), action);
	}

	internal void OnPreNPCPortraitDraw(Action<SpriteBatch, Color, Rectangle, NPC> action)
	{
		CalledMod.Call(nameof(OnPreNPCPortraitDraw), action);
	}

	internal void OnPostNPCPortraitDraw(Action<SpriteBatch, Color, Rectangle, NPC> action)
	{
		CalledMod.Call(nameof(OnPostNPCPortraitDraw), action);
	}

	internal void OnPreSignPortraitDraw(Action<SpriteBatch, Color, Rectangle, int> action)
	{
		CalledMod.Call(nameof(OnPreSignPortraitDraw), action);
	}

	internal void OnPostSignPortraitDraw(Action<SpriteBatch, Color, Rectangle, int> action)
	{
		CalledMod.Call(nameof(OnPostSignPortraitDraw), action);
	}

	internal void AddButton(List<int> npcIds, Func<string> buttonText, string texturePath, Action hoverAction)
	{
		CalledMod.Call(nameof(AddButton), npcIds, buttonText, texturePath, hoverAction);
	}

	internal void AddButton(List<int> npcIds, Func<string> buttonText, string texturePath, Action hoverAction, Func<bool> available)
	{
		CalledMod.Call(nameof(AddButton), npcIds, buttonText, texturePath, hoverAction, available);
	}
}