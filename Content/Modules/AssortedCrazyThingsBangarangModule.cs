using AssortedCrazyThings;
using AssortedCrazyThings.Items.Fun;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using System;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class AssortedCrazyThingsBangarangModule : CrossModModule<BangarangAPI>
{
	public override string CrossModName => ModNames.AssortedCrazyThings;

	protected internal override bool ShouldLoadSupport()
	{
		// Versions higher than this will have their own Bangarang support.
		return CrossMod.Version <= new Version("1.4.4.4");
	}

	protected internal override void PostSetupContent()
	{
		if (ModContent.GetInstance<ContentConfig>().Weapons)
		{
			API.RegisterSimpleBoomerang<GuideVoodoorang>();
		}
	}
}