using Microsoft.Xna.Framework;
using RandomModCompat.Core;
using Terraria;
using Terraria.ModLoader;

namespace RandomModCompat.Common.Callers;

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

	protected override string ModName => "ThoriumMod";

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

	internal bool AddFlailProjectileID<T>() where T : ModProjectile
	{
		return AddFlailProjectileID(ModContent.ProjectileType<T>());
	}

	internal bool IsFlailProjectileID(int projectileId)
	{
		return (bool)CalledMod.Call(nameof(IsFlailProjectileID), projectileId);
	}

	internal bool IsFlailProjectileID<T>() where T : ModProjectile
	{
		return IsFlailProjectileID(ModContent.ProjectileType<T>());
	}

	internal bool AddMartianItemID(int itemId)
	{
		return (bool)CalledMod.Call(nameof(AddMartianItemID), itemId);
	}

	internal bool AddMartianItemID<T>() where T : ModItem
	{
		return AddMartianItemID(ModContent.ItemType<T>());
	}

	internal bool IsMartianProjectileID(int projectileId)
	{
		return (bool)CalledMod.Call(nameof(IsMartianProjectileID), projectileId);
	}

	internal bool IsMartianProjectileID<T>() where T : ModProjectile
	{
		return IsMartianProjectileID(ModContent.ProjectileType<T>());
	}

	internal bool AddPlayerDoTBuffID(int buffId)
	{
		return (bool)CalledMod.Call(nameof(AddPlayerDoTBuffID), buffId);
	}

	internal bool AddPlayerDoTBuffID<T>() where T : ModBuff
	{
		return AddPlayerDoTBuffID(ModContent.BuffType<T>());
	}

	internal bool IsPlayerDoTBuffID(int buffId)
	{
		return (bool)CalledMod.Call(nameof(IsPlayerDoTBuffID), buffId);
	}

	internal bool IsPlayerDoTBuffID<T>() where T : ModBuff
	{
		return IsPlayerDoTBuffID(ModContent.BuffType<T>());
	}

	internal bool AddPlayerStatusBuffID(int buffId)
	{
		return (bool)CalledMod.Call(nameof(AddPlayerStatusBuffID), buffId);
	}

	internal bool AddPlayerStatusBuffID<T>() where T : ModBuff
	{
		return AddPlayerStatusBuffID(ModContent.BuffType<T>());
	}

	internal bool IsPlayerStatusBuffID(int buffId)
	{
		return (bool)CalledMod.Call(nameof(IsPlayerStatusBuffID), buffId);
	}

	internal bool IsPlayerStatusBuffID<T>() where T : ModBuff
	{
		return IsPlayerStatusBuffID(ModContent.BuffType<T>());
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

	// TODO: Finish. https://thoriummod.wiki.gg/wiki/Mod_Calls#BonusBardEmpowermentRange
}