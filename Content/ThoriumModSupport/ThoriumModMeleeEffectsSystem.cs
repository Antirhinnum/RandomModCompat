using MeleeEffects.GlobalItems;
using MeleeEffects.Projectiles;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour.HookGen;
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

namespace RandomModCompat.Content.ThoriumModSupport;

// Thorium's Sheath accessories have an internal timer that counts how long the player has been holding a sword without using it.
// That timer doesn't tick if Item::noMelee is true, and Melee Effects+ sets Item::noMelee to true to handle its effects.
// This system adds a whitelist to that timer for any items Melee Effects+ has changed.
[JITWhenModsEnabled(ModNames.ThoriumMod, ModNames.MeleeEffects)]
internal sealed class ThoriumModMeleeEffectsSystem : ModSystem
{
	#region Hook

	private delegate void orig_HandleStrikeCooldown(SheathData self, Player player, ref int timer);

	private delegate void hook_HandleStrikeCooldown(orig_HandleStrikeCooldown orig, SheathData self, Player player, ref int timer);

	private static event ILContext.Manipulator SheathDataHandleStrikeCooldown
	{
		add
		{
			HookEndpointManager.Modify<hook_HandleStrikeCooldown>(_handleStrikeCooldown, value);
		}
		remove
		{
			HookEndpointManager.Unmodify<hook_HandleStrikeCooldown>(_handleStrikeCooldown, value);
		}
	}

	private static readonly MethodBase _handleStrikeCooldown = typeof(SheathData).GetMethod("HandleStrikeCooldown", ReflectionHelper.AllFlags);

	#endregion Hook

	private static readonly FieldInfo _slashGlobalItemSlashEffect = typeof(SlashGlobalItem).GetField("slasheffect", ReflectionHelper.AllFlags);
	private static HashSet<int> _affectedItems;

	public override bool IsLoadingEnabled(Mod mod)
	{
		return RandomModCompat.SupportEnabled(ModNames.ThoriumMod, ModNames.MeleeEffects);
	}

	public override void Load()
	{
		SheathDataHandleStrikeCooldown += AddWhitelist;
	}

	public override void Unload()
	{
		SheathDataHandleStrikeCooldown -= AddWhitelist;
	}

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
			throw new Exception("Thorium Mod / Melee Effects + support did not succeed.");
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
}