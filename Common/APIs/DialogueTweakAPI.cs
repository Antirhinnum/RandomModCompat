using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RandomModCompat.Core;
using System;
using System.Collections.Generic;
using Terraria;

namespace RandomModCompat.Common.APIs;

internal sealed class DialogueTweakAPI : ModAPI
{
	protected internal override string ModName => ModNames.DialogueTweak;

	internal void ReplaceExtraButtonIcon(List<int> npcIds, string texturePath)
	{
		WrappedMod.Call(nameof(ReplaceExtraButtonIcon), npcIds, texturePath);
	}

	internal void ReplaceExtraButtonIcon(List<int> npcIds, string texturePath, Func<bool> available, Func<Rectangle> frame)
	{
		WrappedMod.Call(nameof(ReplaceExtraButtonIcon), npcIds, texturePath, available, frame);
	}

	internal void ReplaceShopButtonIcon(List<int> npcIds, string texturePath)
	{
		WrappedMod.Call(nameof(ReplaceShopButtonIcon), npcIds, texturePath);
	}

	internal void ReplaceShopButtonIcon(List<int> npcIds, string texturePath, Func<bool> available, Func<Rectangle> frame)
	{
		WrappedMod.Call(nameof(ReplaceShopButtonIcon), npcIds, texturePath, available, frame);
	}

	internal void ReplaceHappinessButtonIcon(List<int> npcIds, string texturePath)
	{
		WrappedMod.Call(nameof(ReplaceHappinessButtonIcon), npcIds, texturePath);
	}

	internal void ReplaceHappinessButtonIcon(List<int> npcIds, string texturePath, Func<bool> available, Func<Rectangle> frame)
	{
		WrappedMod.Call(nameof(ReplaceHappinessButtonIcon), npcIds, texturePath, available, frame);
	}

	internal void ReplaceBackButtonIcon(List<int> npcIds, string texturePath)
	{
		WrappedMod.Call(nameof(ReplaceBackButtonIcon), npcIds, texturePath);
	}

	internal void ReplaceBackButtonIcon(List<int> npcIds, string texturePath, Func<bool> available, Func<Rectangle> frame)
	{
		WrappedMod.Call(nameof(ReplaceBackButtonIcon), npcIds, texturePath, available, frame);
	}

	internal void ReplacePortrait(List<int> npcIds, string texturePath)
	{
		WrappedMod.Call(nameof(ReplacePortrait), npcIds, texturePath);
	}

	internal void ReplacePortrait(List<int> npcIds, string texturePath, Func<bool> available, Func<Rectangle> frame)
	{
		WrappedMod.Call(nameof(ReplacePortrait), npcIds, texturePath, available, frame);
	}

	internal void OnPostPortraitDraw(Action<SpriteBatch, Color, Rectangle> action)
	{
		WrappedMod.Call(nameof(OnPostPortraitDraw), action);
	}

	internal void OnPreNPCPortraitDraw(Action<SpriteBatch, Color, Rectangle, NPC> action)
	{
		WrappedMod.Call(nameof(OnPreNPCPortraitDraw), action);
	}

	internal void OnPostNPCPortraitDraw(Action<SpriteBatch, Color, Rectangle, NPC> action)
	{
		WrappedMod.Call(nameof(OnPostNPCPortraitDraw), action);
	}

	internal void OnPreSignPortraitDraw(Action<SpriteBatch, Color, Rectangle, int> action)
	{
		WrappedMod.Call(nameof(OnPreSignPortraitDraw), action);
	}

	internal void OnPostSignPortraitDraw(Action<SpriteBatch, Color, Rectangle, int> action)
	{
		WrappedMod.Call(nameof(OnPostSignPortraitDraw), action);
	}

	internal void AddButton(List<int> npcIds, Func<string> buttonText, string texturePath, Action hoverAction)
	{
		WrappedMod.Call(nameof(AddButton), npcIds, buttonText, texturePath, hoverAction);
	}

	internal void AddButton(List<int> npcIds, Func<string> buttonText, string texturePath, Action hoverAction, Func<bool> available)
	{
		WrappedMod.Call(nameof(AddButton), npcIds, buttonText, texturePath, hoverAction, available);
	}
}