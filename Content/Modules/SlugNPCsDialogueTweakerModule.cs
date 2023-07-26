using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using SlugNPCs.NPCs;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class SlugNPCsDialogueTweakerModule : CrossModModule<DialogueTweakAPI>
{
	public override string CrossModName => ModNames.SlugNPCs;

	protected internal override void PostSetupContent()
	{
		API.ReplaceShopButtonIcon(new List<int>()
		{
			ModContent.NPCType<AlbinoSlug>(),
			ModContent.NPCType<CryptsSlug>(),
			ModContent.NPCType<DevourerSlug>(),
			ModContent.NPCType<StormSlug>(),
			ModContent.NPCType<UniverseSlug>()
		}, "Head");
	}
}