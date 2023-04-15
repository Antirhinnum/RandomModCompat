using Microsoft.Xna.Framework;
using RandomModCompat.Core;
using RandomModCompat.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomModCompat.Common;

internal sealed class AsymmetricEquipsCaller : ModWithCalls
{
	internal enum PlayerSide
	{
		Both,
		Left,
		Right
	}

	protected override string ModName => "AsymmetricEquips";

	internal void AddEquip(EquipType type, int id, int newId = -1, PlayerSide side = PlayerSide.Right)
	{
		CalledMod.Call(nameof(AddEquip), type, id, newId, side);
	}

	internal void AddGlove(int itemId)
	{
		CalledMod.Call(nameof(AddGlove), itemId);
	}

	internal void AddSpecialItem(int itemId, PlayerSide side = PlayerSide.Right)
	{
		CalledMod.Call(nameof(AddSpecialItem), itemId, side);
	}

	internal bool ItemOnDefaultSide(Item item, Player player)
	{
		return (bool)CalledMod.Call(nameof(ItemOnDefaultSide), item, player);
	}

	internal (int frontBalloon, int cFrontBalloon, int frontBalloonInner, int cFrontBalloonInner, Vector2 offset) GetFrontBalloon(Player player)
	{
		return ((int, int, int, int, Vector2))CalledMod.Call(nameof(GetFrontBalloon), player);
	}

	internal void AddHiddenEquip<T>(EquipType type) where T : ModItem
	{
		AddEquip(type, EquipHelper.GetItemEquip<T>(type));
	}

	internal void AddSmallHead<T>(PlayerSide side = PlayerSide.Right) where T : ModItem
	{
		AddEquip(EquipType.Head, EquipHelper.GetItemEquip<T>(EquipType.Head), ArmorIDs.Head.FamiliarWig, side);
	}

	internal void AddBalloon<T>() where T : ModItem
	{
		AddEquip(EquipType.Balloon, EquipHelper.GetItemEquip<T>(EquipType.Balloon), side: PlayerSide.Left);
	}
}