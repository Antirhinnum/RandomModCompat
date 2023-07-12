using AssortedCrazyThings;
using AssortedCrazyThings.Base.DrawLayers;
using AssortedCrazyThings.Items.Accessories.Useful;
using AssortedCrazyThings.Items.Accessories.Vanity;
using AssortedCrazyThings.Items.Fun;
using AssortedCrazyThings.NPCs.Harvester;
using RandomModCompat.Common.Callers;
using RandomModCompat.Common.ExplicitSupport;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.AssortedCrazyThingsSupport;

[JITWhenModsEnabled(_modName)]
internal sealed class AssortedCrazyThingsSystem : CrossModHandler
{
    private const string _localizationHeader = $"Mods.RandomModCompat.{_modName}.";

    private const string _modName = ModNames.AssortedCrazyThings;
    public override string ModName => _modName;

    /*
	 * Assorted Crazy Things adds support for the following mods:
	 * - Amulet of Many Minions
	 * - Boss Checklist
	 * - Summoners Association
	 *
	 * This file adds support for:
	 * - Asymmetric Equips
	 * - Bangarang
	 * - Magic Storage
	 * - RoR 2 Health Bars
	 * 
	 * Additionally:
	 * - AssortedCrazyThingsFishingRebornSystem: Adds support for Fishing Reborn.
	 */
    // TODO: Bosses as NPCs

    internal override void PostSetupContent()
    {
        AsymmetricEquipsSupport();
        BangarangSupport();
        MagicStorageSupport();
        ROR2HealthBarsSupport();
    }

    private void AsymmetricEquipsSupport()
    {
        if (!TryGetCaller(out AsymmetricEquipsCaller caller))
        {
            return;
        }

        if (ModContent.GetInstance<ContentConfig>().Accessories)
        {
            caller.AddHiddenEquip<BottledDreams>(EquipType.Waist);

            caller.AddBalloon<MassiveBundleOfBalloons>();
            caller.AddBalloon<StarInABalloon>();
            caller.AddBalloon<StarWispBalloon>();
            caller.AddBalloon<WispInABalloon>();

            caller.AddBalloon<CrazyBundleOfAssortedBalloons>();
            AsymmetricEquipsWrappedBalloonLayer.RegisterBalloonDrawLayer<CrazyBundleOfAssortedBalloonsLayer>();
        }

        if (ModContent.GetInstance<ContentConfig>().VanityAccessories)
        {
            caller.AddBalloon<Cobballoon>();
            caller.AddBalloon<EyelloonRetinazer>();
            caller.AddBalloon<GreenEyelloon>();
            caller.AddBalloon<GreenEyelloonFractured>();
            caller.AddBalloon<GreenEyelloonMetal>();
            caller.AddBalloon<GreenEyelloonMetalFractured>();
            caller.AddBalloon<PurpleEyelloon>();
            caller.AddBalloon<PurpleEyelloonFractured>();
            caller.AddBalloon<PurpleEyelloonMetal>();
            caller.AddBalloon<PurpleEyelloonMetalFractured>();
            caller.AddBalloon<RedEyelloon>();
            caller.AddBalloon<RedEyelloonFractured>();
            caller.AddBalloon<RedEyelloonMetal>();
            caller.AddBalloon<RedEyelloonMetalFractured>();
            caller.AddBalloon<SpazmatismEyelloon>();
        }
    }

    private void BangarangSupport()
    {
        if (!TryGetCaller(out BangarangCaller caller))
        {
            return;
        }

        if (ModContent.GetInstance<ContentConfig>().Weapons)
        {
            caller.RegisterSimpleBoomerang<GuideVoodoorang>();
        }
    }

    private void MagicStorageSupport()
    {
        if (!TryGetCaller(out MagicStorageCaller caller))
        {
            return;
        }

        if (ModContent.GetInstance<ContentConfig>().Bosses)
        {
            caller.RegisterShadowDiamondDrop<HarvesterBoss>(1);
        }
    }

    private void ROR2HealthBarsSupport()
    {
        if (!TryGetCaller(out ROR2HealthBarsCaller caller))
        {
            return;
        }

        if (ModContent.GetInstance<ContentConfig>().Bosses)
        {
            caller.AddDesc<HarvesterBoss>(_localizationHeader, "HarvesterBoss");
        }
    }
}