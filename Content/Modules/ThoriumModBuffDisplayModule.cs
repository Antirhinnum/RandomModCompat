using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;
using ThoriumMod.Buffs;

namespace RandomModCompat.Content.Modules;

internal sealed class ThoriumModBuffDisplayModule : CrossModModule<BuffDisplayAPI>
{
	public override string CrossModName => ModNames.ThoriumMod;

	protected internal override void PostSetupContent()
	{
		int grimShadowBuff = ModContent.BuffType<Grimshadow>();
		API.SetCountAs(ModContent.BuffType<Grimshadow2>(), grimShadowBuff);
		API.SetCountAs(ModContent.BuffType<Grimshadow3>(), grimShadowBuff);
	}
}