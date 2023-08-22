using MeleeEffects.GlobalItems;
using MeleeEffects.Projectiles;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using MonoMod.RuntimeDetour.HookGen;
using RandomModCompat.Common.Configs;
using RandomModCompat.Core;
using RandomModCompat.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Core.Sheaths;
using ThoriumMod.Utilities;

namespace RandomModCompat.Content.BugFixes;

// Thorium's Sheath accessories have an internal timer that counts how long the player has been holding a sword without using it.
// That timer doesn't tick if Item::noMelee is true, and Melee Effects+ sets Item::noMelee to true to handle its effects.
// This system adds a whitelist to that timer for any items Melee Effects+ has changed.
[JITWhenModsEnabled(ModNames.ThoriumMod, ModNames.MeleeEffects)]
internal sealed class ThoriumModMeleeEffectsFix : ModSystem, IAddSupport
{
	#region Hook

#if !TML_2022_09
	private static ILHook _handleStrikeCooldownHook;
#endif

	private static readonly MethodBase _handleStrikeCooldown = typeof(SheathData).GetMethod("HandleStrikeCooldown", ReflectionHelper.AllFlags);

	#endregion Hook

	private static readonly FieldInfo _slashGlobalItemSlashEffect = typeof(SlashGlobalItem).GetField("slasheffect", ReflectionHelper.AllFlags);
	private static HashSet<int> _affectedItems;

	string IAddSupport.BaseMod => ModNames.ThoriumMod;
	string IAddSupport.SupportMod => ModNames.MeleeEffects;

	public override bool IsLoadingEnabled(Mod mod)
	{
		if (!RandomModCompat.SupportEnabled(ModNames.ThoriumMod, ModNames.MeleeEffects))
		{
			return false;
		}

		if (ModContent.GetInstance<RandomModCompatConfig>().DisableIL)
		{
			mod.Logger.Info("Thorium Mod / Melee Effects+ bug fix disabled because IL edits are disabled.");
			return false;
		}

		return base.IsLoadingEnabled(mod);
	}

#if TML_2022_09
	public override void Load()
	{
		HookEndpointManager.Modify(_handleStrikeCooldown, AddWhitelist);
	}

	public override void Unload()
	{
		HookEndpointManager.Unmodify(_handleStrikeCooldown, AddWhitelist);
	}
#else

	public override void Load()
	{
		_handleStrikeCooldownHook = new(_handleStrikeCooldown, AddWhitelist);
	}

	public override void Unload()
	{
		_handleStrikeCooldownHook?.Undo();
	}

#endif

	public override void PostSetupContent()
	{
		static bool SlashCheck(Item item) => _slashGlobalItemSlashEffect.GetValue(item.GetGlobalItem<SlashGlobalItem>()) is true;

		_affectedItems = ContentSamples.ItemsByType.Values
			.Where(SlashCheck)
			.Select(item => item.type)
			.ToHashSet();
	}

	private static void AddWhitelist(ILContext il)
	{
		ILCursor c = new(il);

		// Match (C#):
		//	if (player.HeldItem.CountsAsClass(DamageClass.Melee) && !player.HeldItem.noMelee)
		// Match (IL):
		//	ldarg.1
		//	callvirt instance class [tModLoader]Terraria.Item [tModLoader]Terraria.Player::get_HeldItem()
		//	ldfld bool [tModLoader]Terraria.Item::noMelee
		//	brtrue LABEL
		// Replace (C#):
		//	if ( ... && !ShouldItemNotTickSheathCounter(payer.HeldItem))
		// Replace (IL):
		//	ldarg.1
		//	callvirt instance class [tModLoader]Terraria.Item [tModLoader]Terraria.Player::get_HeldItem()
		//	call static class [RandomModCompat]RandomModCompat.Content.ThoriumModSupport.ThoriumModMeleeEffectsSystem::ShouldItemNotTickSheathCounter()
		//	brtrue LABEL

		MethodBase getHeldItem = typeof(Player).GetProperty(nameof(Player.HeldItem)).GetGetMethod();
		if (!c.TryGotoNext(MoveType.Before,
			i => i.MatchLdarg(1),
			i => i.MatchCallvirt(getHeldItem),
			i => i.MatchLdfld<Item>(nameof(Item.noMelee)),
			i => i.MatchBrtrue(out _)
			))
		{
			throw new Exception("Thorium Mod / Melee Effects+ support did not succeed.");
		}

		c.GotoNext(i => i.OpCode == OpCodes.Ldfld);
		c.Remove();
		c.EmitDelegate(ShouldItemNotTickSheathCounter);
	}

	private static bool ShouldItemNotTickSheathCounter(Item item)
	{
		return item.noMelee && !_affectedItems.Contains(item.type);
	}
}

// This player is responsible for making Thorium Mod's Sheath accessories actually proc when hitting an enemy with a sword swing.
// ThoriumPlayer only tries this in OnHitNPC and ModifyHitNPC, meaning projectiles are never (natively) checked.
[JITWhenModsEnabled(ModNames.ThoriumMod, ModNames.MeleeEffects)]
internal sealed class ThoriumModMeleeEffectsPlayer : ModPlayer
{
	private static int[] _meleeEffectsProjectileTypes;

	public override bool IsLoadingEnabled(Mod mod)
	{
		return RandomModCompat.SupportEnabled(ModNames.ThoriumMod, ModNames.MeleeEffects);
	}

	public override void SetStaticDefaults()
	{
		_meleeEffectsProjectileTypes = new int[]
		{
			ModContent.ProjectileType<Slash>(),
			ModContent.ProjectileType<ChargeSlash>(),
			ModContent.ProjectileType<SlashMethod>(),
			ModContent.ProjectileType<SlashMethod2>(),
		};
	}

	public override void Unload()
	{
		_meleeEffectsProjectileTypes = null;
	}

#if TML_2022_09

	public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
	{
		if (_meleeEffectsProjectileTypes.Contains(proj.type))
		{
			Player.GetThoriumPlayer().sheathTracker.Strike(modifyHit: true, Player, target, ref damage, ref crit);
		}
	}

	public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
	{
		if (_meleeEffectsProjectileTypes.Contains(proj.type))
		{
			Player.GetThoriumPlayer().sheathTracker.Strike(modifyHit: false, Player, target, ref damage, ref crit);
		}
	}

#else

	public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (_meleeEffectsProjectileTypes.Contains(proj.type))
		{
			Player.GetThoriumPlayer().sheathTracker.Strike(target, hit, damageDone);
		}
	}

#endif
}