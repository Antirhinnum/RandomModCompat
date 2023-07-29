using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;
using TheConfectionRebirth.Buffs.NeapoliniteBuffs;

namespace RandomModCompat.Content.Modules;

internal sealed class TheConfectionRebirthBuffDisplayModule : CrossModModule<BuffDisplayAPI>
{
	public override string CrossModName => ModNames.TheConfectionRebirth;

	protected internal override void PostSetupContent()
	{
		API.SetCountAs(ModContent.BuffType<SwirlySwarmII>(), ModContent.BuffType<SwirlySwarmI>());
		API.SetCountAs(ModContent.BuffType<SwirlySwarmIII>(), ModContent.BuffType<SwirlySwarmI>());
		API.SetCountAs(ModContent.BuffType<SwirlySwarmIV>(), ModContent.BuffType<SwirlySwarmI>());
		API.SetCountAs(ModContent.BuffType<SwirlySwarmV>(), ModContent.BuffType<SwirlySwarmI>());

		API.SetCountAs(ModContent.BuffType<ChocolateChargeII>(), ModContent.BuffType<ChocolateChargeI>());
		API.SetCountAs(ModContent.BuffType<ChocolateChargeIII>(), ModContent.BuffType<ChocolateChargeI>());
		API.SetCountAs(ModContent.BuffType<ChocolateChargeIV>(), ModContent.BuffType<ChocolateChargeI>());
		API.SetCountAs(ModContent.BuffType<ChocolateChargeV>(), ModContent.BuffType<ChocolateChargeI>());

		API.SetCountAs(ModContent.BuffType<StrawberryStrikeII>(), ModContent.BuffType<StrawberryStrikeI>());
		API.SetCountAs(ModContent.BuffType<StrawberryStrikeIII>(), ModContent.BuffType<StrawberryStrikeI>());
		API.SetCountAs(ModContent.BuffType<StrawberryStrikeIV>(), ModContent.BuffType<StrawberryStrikeI>());
		API.SetCountAs(ModContent.BuffType<StrawberryStrikeV>(), ModContent.BuffType<StrawberryStrikeI>());

		API.SetCountAs(ModContent.BuffType<VanillaValorII>(), ModContent.BuffType<VanillaValorI>());
		API.SetCountAs(ModContent.BuffType<VanillaValorIII>(), ModContent.BuffType<VanillaValorI>());
		API.SetCountAs(ModContent.BuffType<VanillaValorIV>(), ModContent.BuffType<VanillaValorI>());
		API.SetCountAs(ModContent.BuffType<VanillaValorV>(), ModContent.BuffType<VanillaValorI>());
	}
}