using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using SlugNPCsEX.NPCs;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class SlugNPCsEXDialogueTweakerModule : CrossModModule<DialogueTweakAPI>
{
	public override string CrossModName => ModNames.SlugNPCsEX;

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