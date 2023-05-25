using Microsoft.Xna.Framework;
using RandomModCompat.Core;
using RandomModCompat.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomModCompat.Common.Callers;

// Call info from: https://thoriummod.wiki.gg/wiki/Mod_Calls
internal sealed class ThoriumModCaller : ModWithCalls
{
	internal enum ThoriumBoss
	{
		TheGrandThunderBird,
		QueenJellyfish,
		Viscount,
		GraniteEnergyStorm,
		BuriedChampion,
		StarScouter,
		BoreanStrider,
		FallenBeholder,
		Lich,
		ForgottenOne,
		ThePrimordials,
		PatchWerk,
		CorpseBloom,
		Illusionist
	}

	internal enum InstrumentType
	{
		Other,
		Wind,
		Brass,
		String,
		Percussion,
		Electronic
	}

	protected internal override string ModName => ModNames.ThoriumMod;

	#region Default

	internal bool GetDownedBoss(ThoriumBoss boss)
	{
		return (bool)CalledMod.Call(nameof(GetDownedBoss), boss.ToString());
	}

	internal Rectangle GetAquaticDepthsBounds()
	{
		return (Rectangle)CalledMod.Call(nameof(GetAquaticDepthsBounds));
	}

	internal Rectangle GetBloodChamberBounds()
	{
		return (Rectangle)CalledMod.Call(nameof(GetBloodChamberBounds));
	}

	internal Vector2 GetBloodAltarPosition()
	{
		return (Vector2)CalledMod.Call(nameof(GetBloodAltarPosition));
	}

	internal bool AddFlailProjectileID(int projectileId)
	{
		return (bool)CalledMod.Call(nameof(AddFlailProjectileID), projectileId);
	}

	internal bool IsFlailProjectileID(int projectileId)
	{
		return (bool)CalledMod.Call(nameof(IsFlailProjectileID), projectileId);
	}

	internal bool AddMartianItemID(int itemId)
	{
		return (bool)CalledMod.Call(nameof(AddMartianItemID), itemId);
	}

	internal bool IsMartianProjectileID(int projectileId)
	{
		return (bool)CalledMod.Call(nameof(IsMartianProjectileID), projectileId);
	}

	internal bool AddPlayerDoTBuffID(int buffId)
	{
		return (bool)CalledMod.Call(nameof(AddPlayerDoTBuffID), buffId);
	}

	internal bool IsPlayerDoTBuffID(int buffId)
	{
		return (bool)CalledMod.Call(nameof(IsPlayerDoTBuffID), buffId);
	}

	internal bool AddPlayerStatusBuffID(int buffId)
	{
		return (bool)CalledMod.Call(nameof(AddPlayerStatusBuffID), buffId);
	}

	internal bool IsPlayerStatusBuffID(int buffId)
	{
		return (bool)CalledMod.Call(nameof(IsPlayerStatusBuffID), buffId);
	}

	internal bool IsBardItem(Item item)
	{
		return (bool)CalledMod.Call(nameof(IsBardItem), item);
	}

	internal (bool, InstrumentType) IsBardWeapon(Item item)
	{
		return ((bool, InstrumentType))CalledMod.Call(nameof(IsBardWeapon), item);
	}

	internal (bool, InstrumentType) IsBardProjectile(Projectile projectile)
	{
		return ((bool, InstrumentType))CalledMod.Call(nameof(IsBardProjectile), projectile);
	}

	internal void BonusBardEmpowermentRange(Player player, int addedRange)
	{
		CalledMod.Call(nameof(BonusBardEmpowermentRange), player, addedRange);
	}

	internal void BonusBardEmpowermentDuration(Player player, short addedDurationTicks)
	{
		CalledMod.Call(nameof(BonusBardEmpowermentDuration), player, addedDurationTicks);
	}

	internal void BonusBardInspirationMax(Player player, int added)
	{
		CalledMod.Call(nameof(BonusBardInspirationMax), player, added);
	}

	internal int GetBardInspiration(Player player)
	{
		return (int)CalledMod.Call(nameof(GetBardInspiration), player);
	}

	internal int GetBardInspirationMax(Player player)
	{
		return (int)CalledMod.Call(nameof(GetBardInspirationMax), player);
	}

	internal void BonusHealerHealBonus(Player player, int added)
	{
		CalledMod.Call(nameof(BonusHealerHealBonus), player, added);
	}

	internal int GetHealerHealBonus(Player player)
	{
		return (int)CalledMod.Call(nameof(GetHealerHealBonus), player);
	}

	internal void BonusLifeRecovery(Player player, int added)
	{
		CalledMod.Call(nameof(BonusLifeRecovery), player, added);
	}

	internal void BonusLifeRecoveryIntervalReduction(Player player, int added)
	{
		CalledMod.Call(nameof(BonusLifeRecoveryIntervalReduction), player, added);
	}

	internal int GetLifeRecovery(Player player)
	{
		return (int)CalledMod.Call(nameof(GetLifeRecovery), player);
	}

	internal int GetLifeRecoveryInterval(Player player)
	{
		return (int)CalledMod.Call(nameof(GetLifeRecoveryInterval), player);
	}

	internal void BonusDartDamage(Player player, float mult)
	{
		CalledMod.Call(nameof(BonusDartDamage), player, mult);
	}

	internal void BonusMartianDamage(Player player, float mult)
	{
		CalledMod.Call(nameof(BonusMartianDamage), player, mult);
	}

	#endregion Default

	#region Utility

	internal bool AddFlailProjectileID<T>() where T : ModProjectile
	{
		return AddFlailProjectileID(ModContent.ProjectileType<T>());
	}

	internal bool IsFlailProjectileID<T>() where T : ModProjectile
	{
		return IsFlailProjectileID(ModContent.ProjectileType<T>());
	}

	internal bool AddMartianItemID<T>() where T : ModItem
	{
		return AddMartianItemID(ModContent.ItemType<T>());
	}

	internal bool IsMartianProjectileID<T>() where T : ModProjectile
	{
		return IsMartianProjectileID(ModContent.ProjectileType<T>());
	}

	internal bool AddPlayerDoTBuffID<T>() where T : ModBuff
	{
		return AddPlayerDoTBuffID(ModContent.BuffType<T>());
	}

	internal bool IsPlayerDoTBuffID<T>() where T : ModBuff
	{
		return IsPlayerDoTBuffID(ModContent.BuffType<T>());
	}

	internal bool AddPlayerStatusBuffID<T>() where T : ModBuff
	{
		return AddPlayerStatusBuffID(ModContent.BuffType<T>());
	}

	internal bool IsPlayerStatusBuffID<T>() where T : ModBuff
	{
		return IsPlayerStatusBuffID(ModContent.BuffType<T>());
	}

	#endregion Utility

	#region Custom

	// These should probably go in ExplicitSupport, but eh.

	internal enum ThoriumRepellentType
	{
		Bat,
		Bug,
		Fish,
		Skeleton,
		Zombie
	}

	// Internal class :(
	private const BindingFlags _flags = ReflectionHelper.AllFlags;

	private static readonly Type _thoriumCacheHandler = ModLoader.GetMod(ModNames.ThoriumMod).Code.GetType("ThoriumMod.ThoriumCacheHandler");
	private static readonly HashSet<int> _miscUndeadNPCs = (HashSet<int>)_thoriumCacheHandler.GetField("MiscUndeadNPCs", _flags).GetValue(null);
	private static readonly HashSet<int> _miscSkeletonNPCs = (HashSet<int>)_thoriumCacheHandler.GetField("MiscVanillaSkeletonNPCs", _flags).GetValue(null);
	private static readonly HashSet<int> _batNPCs = (HashSet<int>)_thoriumCacheHandler.GetField("BatNPCs", _flags).GetValue(null);
	private static readonly HashSet<int> _insectNPCs = (HashSet<int>)_thoriumCacheHandler.GetField("HostileInsectNPCs", _flags).GetValue(null);
	private static readonly HashSet<int> _fishNPCs = (HashSet<int>)_thoriumCacheHandler.GetField("HostileFishNPCs", _flags).GetValue(null);
	private static readonly Dictionary<int, int> _mineralLauncherItemToProj = (Dictionary<int, int>)_thoriumCacheHandler.GetField("MineralLauncherItemToProj", _flags).GetValue(null);

	/// <summary>
	/// Marks an enemy as being repelled by one of Thorium's Enemy Repellents.
	/// <br/> Repelled enemies are non-hostile towards the player.
	/// <br/> Bosses cannot be repelled.
	/// </summary>
	/// <typeparam name="T">The NPC type.</typeparam>
	/// <param name="type">Which repellent to use.</param>
	internal void AddRepelledEnemy<T>(ThoriumRepellentType type)
		where T : ModNPC
	{
		if (type == ThoriumRepellentType.Zombie)
		{
			NPCID.Sets.Zombies[ModContent.NPCType<T>()] = true;
			return;
		}

		HashSet<int> set = type switch
		{
			ThoriumRepellentType.Bat => _batNPCs,
			ThoriumRepellentType.Bug => _insectNPCs,
			ThoriumRepellentType.Fish => _fishNPCs,
			ThoriumRepellentType.Skeleton => _miscSkeletonNPCs,
			_ => throw new NotImplementedException(),
		};
		set!.Add(ModContent.NPCType<T>());
	}

	/// <summary>
	/// Marks an enemy as being undead, which makes the Palm Cross deal double damage to it.
	/// <br/> Any enemy in the <see cref="NPCID.Sets.Zombies"/> or <see cref="NPCID.Sets.Skeletons"/> sets will already have this effect, as well as any enemy registered as a <see cref="ThoriumRepellentType.Skeleton"/> for <see cref="AddRepelledEnemy{T}(ThoriumRepellentType)"/>.
	/// </summary>
	/// <typeparam name="T">The NPC type.</typeparam>
	internal void AddUndeadEnemy<T>()
		where T : ModNPC
	{
		_miscUndeadNPCs.Add(ModContent.NPCType<T>());
	}

	/// <summary>
	/// Registers an item and projectile for use with the Mineral Launcher.
	/// <br/> <paramref name="projectile"/> should refer to an OreShot.
	/// </summary>
	/// <param name="item">The ore item ID.</param>
	/// <param name="projectile">The projectile ID.</param>
	internal void RegisterAsMineralLauncherProjectile(int item, int projectile)
	{
		_mineralLauncherItemToProj[item] = projectile;
	}

	#endregion Custom
}