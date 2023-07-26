using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Tiles;

namespace RandomModCompat.Content.Modules;

internal sealed class ThoriumModUniversalCraftModule : CrossModModule<UniversalCraftAPI>
{
	public override string CrossModName => ModNames.ThoriumMod;

	protected internal override void PostSetupContent()
	{
		static bool GuidesFinalGiftCondition() => NPC.downedMoonlord && ModContent.GetInstance<ThoriumConfigServer>().donatorOther.toggleGuidesFinalGift;

		API.AddStation<ArcaneArmorFabricator>(() => NPC.downedBoss1);
		// Removed since it's just a movable Demon/Crimson Altar.
		//API.AddStation<ThoriumMod.Tiles.GrimPedestal>(() => NPC.downedBoss1);
		API.AddStation<GuidesFinalGiftTile>(GuidesFinalGiftCondition);
		API.AddStation<SoulForgeNew>(() => NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3);
		API.AddStation<ThoriumAnvil>();
	}
}