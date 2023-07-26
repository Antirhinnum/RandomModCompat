using Microsoft.Xna.Framework;
using RandomModCompat.Core;
using RandomModCompat.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

// Call info from: https://github.com/Antirhinnum/AsymmetricEquips#mod-calls
internal sealed partial class AsymmetricEquipsAPI : ModAPI
{
	protected internal override string ModName => ModNames.AsymmetricEquips;

	#region Default

	internal void AddEquip(EquipType type, int id, int newId = -1, PlayerSide side = PlayerSide.Right)
	{
		WrappedMod.Call(nameof(AddEquip), type, id, newId, side);
	}

	internal void AddGlove(int itemId)
	{
		WrappedMod.Call(nameof(AddGlove), itemId);
	}

	internal void AddSpecialItem(int itemId, PlayerSide side = PlayerSide.Right)
	{
		WrappedMod.Call(nameof(AddSpecialItem), itemId, side);
	}

	internal bool ItemOnDefaultSide(Item item, Player player)
	{
		return (bool)WrappedMod.Call(nameof(ItemOnDefaultSide), item, player);
	}

	internal (int frontBalloon, int cFrontBalloon, int frontBalloonInner, int cFrontBalloonInner, Vector2 offset) GetFrontBalloon(Player player)
	{
		return ((int, int, int, int, Vector2))WrappedMod.Call(nameof(GetFrontBalloon), player);
	}

	#endregion Default

	#region Utility

	internal void AddHiddenEquip<T>(EquipType type) where T : ModItem
	{
		AddEquip(type, EquipHelper.GetItemEquip<T>(type));
	}

	internal void AddFlippedEquip<T>(EquipType type, PlayerSide side = PlayerSide.Right) where T : ModItem
	{
		ModItem item = ModContent.GetInstance<T>();
		AddEquip(type, EquipHelper.GetItemEquip<T>(type), EquipLoader.GetEquipSlot(Mod, FlippedEquipName(item.Name, type), type), side);
	}

	internal void AddSmallHead<T>(PlayerSide side = PlayerSide.Right) where T : ModItem
	{
		AddEquip(EquipType.Head, EquipHelper.GetItemEquip<T>(EquipType.Head), ArmorIDs.Head.FamiliarWig, side);
	}

	internal void AddBalloon(int itemId)
	{
		AddEquip(EquipType.Balloon, EquipHelper.GetItemEquip(ContentSamples.ItemsByType[itemId], EquipType.Balloon), side: PlayerSide.Left);
	}

	internal void AddBalloon<T>() where T : ModItem
	{
		AddBalloon(ModContent.ItemType<T>());
	}

	internal int AddFlippedEquipTexture(string modName, string name, EquipType type)
	{
		return EquipLoader.AddEquipTexture(Mod, FlippedEquipTexturePath(modName, name, type), type, name: FlippedEquipName(name, type));
	}

	private string FlippedEquipTexturePath(string modName, string name, EquipType type)
	{
		return $"{Mod.Name}/Assets/{modName}/AsymmetricEquips/{name}_{type}_Flipped";
	}

	private static string FlippedEquipName(string name, EquipType type)
	{
		return $"{name}_{type}_Flipped";
	}

	internal void AddGlove<T>() where T : ModItem
	{
		AddGlove(ModContent.ItemType<T>());
	}

	#endregion Utility

	#region Custom

	/// <inheritdoc cref="AsymmetricEquipsWrappedBalloonLayer.RegisterBalloonDrawLayer{T}"/>
	public void RegisterBalloonDrawLayer<T>() where T : PlayerDrawLayer
	{
		AsymmetricEquipsWrappedBalloonLayer.RegisterBalloonDrawLayer<T>();
	}

	#endregion Custom
}