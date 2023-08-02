using OrchidMod.Alchemist.Accessories;
using OrchidMod.Alchemist.Weapons.Air;
using OrchidMod.Alchemist.Weapons.Dark;
using OrchidMod.Alchemist.Weapons.Fire;
using OrchidMod.Alchemist.Weapons.Light;
using OrchidMod.Alchemist.Weapons.Nature;
using OrchidMod.Alchemist.Weapons.Water;
using OrchidMod.Content.Items.Mounts;
using OrchidMod.Content.Items.Ranged;
using OrchidMod.Dancer.Weapons;
using OrchidMod.Gambler;
using OrchidMod.Gambler.Accessories;
using OrchidMod.General.Items.Misc;
using OrchidMod.Guardian.Accessories;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class OrchidModItemCheckBlacklistModule : CrossModModule<ItemCheckBlacklistAPI>
{
	public override string CrossModName => ModNames.OrchidMod;

	protected internal override void PostSetupContent()
	{
		// From https://terrariamods.wiki.gg/wiki/Orchid_Mod/List_of_items
		API.Add(
			ModContent.ItemType<AlchemistTest>(),
			ModContent.ItemType<AirVial>(),
			ModContent.ItemType<DarkVial>(),
			ModContent.ItemType<FireVial>(),
			ModContent.ItemType<LightVial>(),
			ModContent.ItemType<NatureVial>(),
			ModContent.ItemType<WaterVial>(),
			ModContent.ItemType<GoldFan>(),
			ModContent.ItemType<WoodenTekko>(),
			ModContent.ItemType<GamblerTest>(),
			ModContent.ItemType<GamblerDummyTest>(),
			ModContent.ItemType<GamblerReset>(),
			ModContent.ItemType<GoblinSpike>(),
			ModContent.ItemType<GuardianTest>(),
			ModContent.ItemType<CopperKey>(),
			ModContent.ItemType<HeraldOfFrost>(),
			ModContent.ItemType<SearingOnslaught>(),
			ModContent.ItemType<SquareMinecart>()
		);
	}
}