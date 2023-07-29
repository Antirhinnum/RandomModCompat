using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria;
using TheConfectionRebirth.Tiles;

namespace RandomModCompat.Content.Modules;

internal sealed class TheConfectionRebirthUniversalCraftModule : CrossModModule<UniversalCraftAPI>
{
	public override string CrossModName => ModNames.TheConfectionRebirth;

	protected internal override void PostSetupContent()
	{
		API.AddStation<HeavensForgeTile>(() => Main.hardMode);
	}
}