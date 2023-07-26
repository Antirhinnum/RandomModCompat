using CalamityMod.Buffs;
using CalamityMod.Buffs.Mounts;
using CalamityMod.Buffs.StatBuffs;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class CalamityModBuffDisplayModule : CrossModModule<BuffDisplayAPI>
{
	public override string CrossModName => ModNames.CalamityMod;

	protected internal override void PostSetupContent()
	{
		API.SetCountAs(ModContent.BuffType<PopoNoselessBuff>(), ModContent.BuffType<PopoBuff>());
		API.SetCountAs(ModContent.BuffType<AndromedaSmallBuff>(), ModContent.BuffType<AndromedaBuff>());

		API.SetCountAs(ModContent.BuffType<HallowedRuneDefense>(), ModContent.BuffType<HallowedRuneRegeneration>());
		API.SetCountAs(ModContent.BuffType<HallowedRunePower>(), ModContent.BuffType<HallowedRuneRegeneration>());

		API.SetCountAs(ModContent.BuffType<PhantomicEmpowerment>(), ModContent.BuffType<PhantomicShield>());
		API.SetCountAs(ModContent.BuffType<PhantomicRegen>(), ModContent.BuffType<PhantomicShield>());

		API.SetCountAs(ModContent.BuffType<SpiritDefense>(), ModContent.BuffType<SpiritRegen>());
		API.SetCountAs(ModContent.BuffType<SpiritPower>(), ModContent.BuffType<SpiritRegen>());
	}
}