using FishingReborn.Common.CustomCatchRules.Conditions;
using FishingReborn.Common.CustomCatchRules.Pools;
using OrchidMod.Shaman.Weapons.Hardmode;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class OrchidModFishingRebornModule : CrossModModule<FishingRebornAPI>
{
	public override string CrossModName => ModNames.OrchidMod;

	protected internal override void PostSetupContent()
	{
		int id = ModContent.ItemType<WyvernMoray>();
		API.AddToPool(nameof(SpaceCatchPool), id, 1f, new HardmodeCondition());
		API.AddCatchData(id, 80, FishingRebornAPI.FishType.Floater);
	}
}