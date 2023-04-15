using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomModCompat.Utilities;

/// <summary>
/// A variety of methods to make working with equip slots easier.
/// </summary>
internal class EquipHelper : ILoadable
{
	private static MethodInfo _EquipLoader_GetPlayerEquip;
	private static MethodInfo _EquipLoader_GetTextureArray;

	void ILoadable.Load(Mod mod)
	{
		_EquipLoader_GetPlayerEquip = typeof(EquipLoader).GetMethod("GetPlayerEquip", BindingFlags.NonPublic | BindingFlags.Static);
		_EquipLoader_GetTextureArray = typeof(EquipLoader).GetMethod("GetTextureArray", BindingFlags.NonPublic | BindingFlags.Static);
	}

	void ILoadable.Unload()
	{
		_EquipLoader_GetPlayerEquip = null;
		_EquipLoader_GetTextureArray = null;
	}

	/// <summary>
	/// Gets the given equip slot of the given item.
	/// </summary>
	/// <param name="item"></param>
	/// <param name="type"></param>
	/// <returns>The associated equip slot.</returns>
	internal static int GetItemEquip(Item item, EquipType type)
	{
		return type switch
		{
			EquipType.Head => item.headSlot,
			EquipType.Body => item.bodySlot,
			EquipType.Legs => item.legSlot,
			EquipType.HandsOn => item.handOnSlot,
			EquipType.HandsOff => item.handOffSlot,
			EquipType.Back => item.backSlot,
			EquipType.Front => item.frontSlot,
			EquipType.Shoes => item.shoeSlot,
			EquipType.Waist => item.waistSlot,
			EquipType.Wings => item.wingSlot,
			EquipType.Shield => item.shieldSlot,
			EquipType.Neck => item.neckSlot,
			EquipType.Face => item.faceSlot,
			EquipType.Beard => item.beardSlot,
			EquipType.Balloon => item.balloonSlot,
			_ => 0
		};
	}

	internal static int GetItemEquip<T>(EquipType type) where T : ModItem
	{
		return GetItemEquip(ContentSamples.ItemsByType[ModContent.ItemType<T>()], type);
	}

	/// <summary>
	/// Gets the player's current equip of the given type.
	/// </summary>
	/// <param name="player">The player to check from.</param>
	/// <param name="type">The equip to check for.</param>
	/// <returns></returns>
	internal static int GetPlayerEquip(Player player, EquipType type)
	{
		return (int)_EquipLoader_GetPlayerEquip?.Invoke(null, new object[] { player, type });
	}

	/// <summary>
	/// Gets the texture array for the given equip type.
	/// </summary>
	internal static Asset<Texture2D>[] GetTextureArray(EquipType type)
	{
		return (Asset<Texture2D>[])_EquipLoader_GetTextureArray.Invoke(null, new object[] { type });
	}

	/// <summary>
	/// Gets the dye slot for the specified equip.<br/>
	/// Note: This does not check alternate equip slots, such as <see cref="Player.faceFlower"/>.
	/// </summary>
	internal static int GetDrawDyeSlot(in PlayerDrawSet drawInfo, EquipType type)
	{
		return type switch
		{
			EquipType.Head => drawInfo.cHead,
			EquipType.Body => drawInfo.cBody,
			EquipType.Legs => drawInfo.cLegs,
			EquipType.HandsOn => drawInfo.cHandOn,
			EquipType.HandsOff => drawInfo.cHandOff,
			EquipType.Back => drawInfo.cBack,
			EquipType.Front => drawInfo.cFront,
			EquipType.Shoes => drawInfo.cShoe,
			EquipType.Waist => drawInfo.cWaist,
			EquipType.Wings => drawInfo.cWings,
			EquipType.Shield => drawInfo.cShield,
			EquipType.Neck => drawInfo.cNeck,
			EquipType.Face => drawInfo.cFace,
			EquipType.Beard => drawInfo.cBeard,
			EquipType.Balloon => drawInfo.cBalloon,
			_ => 0
		};
	}
}