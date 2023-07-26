using AssortedCrazyThings;
using AssortedCrazyThings.Items.Pets;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class AssortedCrazyThingsFishingRebornModule : CrossModModule<FishingRebornAPI>
{
	public override string CrossModName => ModNames.AssortedCrazyThings;

	protected internal override void PostSetupContent()
	{
		if (ModContent.GetInstance<ContentConfig>().OtherPets)
		{
			int itemId = ModContent.ItemType<AnomalocarisItem>();
			API.AddToPool("GeneralCatchPool", itemId, 0.075f, new FishingRebornAPI.ArbitraryCondition(AnomalocarisCondition));
			API.AddCatchData(itemId, 120, FishingRebornAPI.FishType.Sinker);
		}
	}

	private static bool AnomalocarisCondition(FishingAttempt attempt, Projectile projectile) => attempt.X / 16 < Main.maxTilesX * 0.08f || attempt.X / 16 > Main.maxTilesX * 0.92f;
}