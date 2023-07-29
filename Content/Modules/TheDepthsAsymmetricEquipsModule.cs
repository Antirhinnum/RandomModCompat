using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using RandomModCompat.Utilities;
using Terraria.ModLoader;
using TheDepths.Items.Accessories;

namespace RandomModCompat.Content.Modules;

internal sealed class TheDepthsAsymmetricEquipsModule : CrossModModule<AsymmetricEquipsAPI>
{
	public override string CrossModName => ModNames.TheDepths;

	protected internal override void PostSetupContent()
	{
		// Internal class :(
		API.AddEquip(EquipType.Face, EquipHelper.GetItemEquip(CrossMod.Find<ModItem>("StoneRose").Item, EquipType.Face));
		API.AddGlove<AquaGlove>();
	}
}