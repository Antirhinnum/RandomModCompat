using ItReallyMustBe;
using ItReallyMustBe.Items;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class ItReallyMustBeItemCheckBlacklistModule : CrossModModule<ItemCheckBlacklistAPI>
{
	public override string CrossModName => ModNames.ItReallyMustBe;

	protected internal override void PostSetupContent()
	{
		if (ModContent.GetInstance<MustConfig>().ByeBait)
		{
			API.Add<FunnyBait>();
		}
	}
}