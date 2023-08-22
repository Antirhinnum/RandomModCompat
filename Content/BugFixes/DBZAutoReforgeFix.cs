// Fixed in the 1.4.4 port.
#if TML_2022_09
using DBZMODPORT;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using MonoMod.RuntimeDetour.HookGen;
using RandomModCompat.Common.Configs;
using RandomModCompat.Core;
using System;
using System.Reflection;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace RandomModCompat.Content.BugFixes;

[JITWhenModsEnabled(ModNames.DBZMODPORT, ModNames.AutoReroll)]
internal sealed class DBZAutoReforgeFix : ModSystem, IAddSupport
{

	private static readonly MethodBase _kiItemChoosePrefix = typeof(KiItem).GetMethod(nameof(KiItem.ChoosePrefix));

	string IAddSupport.BaseMod => ModNames.DBZMODPORT;
	string IAddSupport.SupportMod => ModNames.AutoReroll;

	public override bool IsLoadingEnabled(Mod mod)
	{
		if (!RandomModCompat.SupportEnabled(ModNames.DBZMODPORT, ModNames.AutoReroll))
		{
			return false;
		}

		if (ModContent.GetInstance<RandomModCompatConfig>().DisableIL)
		{
			mod.Logger.Info("Dragon Ball Terraria / Auto Reforge bug fix disabled because IL edits are disabled.");
			return false;
		}

		return ModLoader.TryGetMod(ModNames.DBZMODPORT, out Mod dbz) && dbz.Version <= new Version(0, 3, 3) && base.IsLoadingEnabled(mod);
		// There may or may not be a hotfix for this bug. v0.3.3 (v1.0.9.9) was the latest version this bug is guaranteed to happen on.
	}

	public override void Load()
	{
		HookEndpointManager.Modify(_kiItemChoosePrefix, DBZAutoReforgeFix_KiItemChoosePrefix);
	}

	public override void Unload()
	{
		HookEndpointManager.Unmodify(_kiItemChoosePrefix, DBZAutoReforgeFix_KiItemChoosePrefix);
	}

	/// <summary>
	/// Fixes ChoosePrefix constructing a new UnifiedRandom every time it's called.
	/// </summary>
	private static void DBZAutoReforgeFix_KiItemChoosePrefix(ILContext il)
	{
		ILCursor c = new(il);

		// Change this:
		//	... = new WeightedRandom<int>();
		// To this:
		//	... = new WeightedRandom<int>(rand);
		// "rand" here is provided by ChoosePrefix().

		// The Short Answer:
		// Auto Reforge calls ChoosePrefix() so fast that newly-created randoms get seeded with the same value.
		// This means ChoosePrefix() always returns the same prefix, meaning ki items are only ever shown one prefix in Auto Reforge's menu.

		// The Long Answer:
		// Without the provided UnifiedRandom parameter, WeightedRandom constructs a new UnifiedRandom using the default constructor.
		// The default UnifiedRandom constructor uses Environment.TickCount as its seed, which is the number of milliseconds since the system started.
		// This is all well and good in most cases, but ends up not working if you create multiple UnifiedRandoms in a millisecond.
		// Auto Reforge brute-forces Item.Prefix(-2) (calls ChoosePrefix()) to get prefixes, so it *does* end up creating multiple UnifiedRandoms with the same seed.
		// In fact, it goes through its entire stock of attempts in less than a millisecond.
		// This means that all attempts use UnifiedRandoms with the same seed and, thus, produce the same value.
		// Auto Reforge only adds more attempts if it gets a prefix it hasn't seen before, so it goes through all attempts without ever seeing a different prefix.
		// Thus, since Auto Reforge only thinks ki items can get one prefix, it only shows that one prefix.
		// Final Note: Sometimes, Environment.TickCount *does* update during this process. Only once, though, so only two prefixes are shown instead of one.

		if (!c.TryGotoNext(MoveType.Before,
			i => i.MatchNewobj<WeightedRandom<int>>()))
		{
			throw new Exception("Couldn't find newobj");
		}

		c.Emit(OpCodes.Ldarg_1);
		c.Next.Operand = typeof(WeightedRandom<int>).GetConstructor(new Type[] { typeof(UnifiedRandom) });
	}
}
#endif