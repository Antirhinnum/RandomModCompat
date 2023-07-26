using Consolaria.Content.NPCs.Bosses.Ocram;
using Consolaria.Content.NPCs.Bosses.Turkor;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class ConsolariaMagicStorageModule : CrossModModule<MagicStorageAPI>
{
	public override string CrossModName => ModNames.Consolaria;

	protected internal override void PostSetupContent()
	{
		int lepusType = ModContent.Find<ModNPC>(ModNames.Consolaria, "Lepus").Type;

		API.RegisterShadowDiamondDrop(lepusType, 1);
		API.RegisterShadowDiamondDrop<Ocram>(1);
		API.RegisterShadowDiamondDrop<TurkortheUngrateful>(1);
	}
}