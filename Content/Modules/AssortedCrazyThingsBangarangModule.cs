using AssortedCrazyThings;
using AssortedCrazyThings.Items.Fun;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class AssortedCrazyThingsBangarangModule : CrossModModule<BangarangAPI>
{
	public override string CrossModName => ModNames.AssortedCrazyThings;

	protected internal override void PostSetupContent()
	{
		if (ModContent.GetInstance<ContentConfig>().Weapons)
		{
			API.RegisterSimpleBoomerang<GuideVoodoorang>();
		}
	}
}