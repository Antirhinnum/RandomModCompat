using RandomModCompat.Common.APIs;
using RandomModCompat.Content.ExplicitSupport;
using RandomModCompat.Core;
using Terraria.ModLoader;
using ThoriumMod.Items.BardItems;
using ThoriumMod.Items.BasicAccessories;
using ThoriumMod.Items.Depths;
using ThoriumMod.Items.Donate;
using ThoriumMod.Items.EarlyMagic;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.MeleeItems;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.SummonItems;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Items.Vanity;
using ThoriumMod.PlayerLayers;

#if TML_2022_09
using ThoriumMod.Items.EnergyStorm;
using ThoriumMod.Items.FallenBeholder;
using ThoriumMod.Items.Lich;
#else

using ThoriumMod.Items.BossFallenBeholder;
using ThoriumMod.Items.BossGraniteEnergyStorm;
using ThoriumMod.Items.BossLich;

#endif

namespace RandomModCompat.Content.Modules;

internal sealed class ThoriumModAsymmetricEquipsModule : CrossModModule<AsymmetricEquipsAPI>
{
	public override string CrossModName => ModNames.ThoriumMod;

	protected internal override void OnModLoad()
	{
		Mod.AddContent<ThoriumAsymmetricSheathPlayer>();

		API.AddFlippedEquipTexture(CrossModName, nameof(SupportSash), EquipType.Waist);
	}

	protected internal override void PostSetupContent()
	{
		// Basic hidden equips
		API.AddHiddenEquip<AmberRing>(EquipType.HandsOn);
		API.AddHiddenEquip<AmethystRing>(EquipType.HandsOn);
		API.AddHiddenEquip<AquamarineRing>(EquipType.HandsOn);
		API.AddHiddenEquip<DiamondRing>(EquipType.HandsOn);
		API.AddHiddenEquip<EmeraldRing>(EquipType.HandsOn);
		API.AddHiddenEquip<OpalRing>(EquipType.HandsOn);
		API.AddHiddenEquip<RubyRing>(EquipType.HandsOn);
		API.AddHiddenEquip<SapphireRing>(EquipType.HandsOn);
		API.AddHiddenEquip<TopazRing>(EquipType.HandsOn);
		API.AddHiddenEquip<TheRing>(EquipType.HandsOn);
		API.AddHiddenEquip<VampireGland>(EquipType.Waist);
		API.AddHiddenEquip<HungeringBlossom>(EquipType.Face);
		API.AddHiddenEquip<MetabolicPills>(EquipType.Waist);
		API.AddHiddenEquip<PlagueLordFlask>(EquipType.Waist);
		API.AddHiddenEquip<ManaBauble>(EquipType.Waist);
		API.AddHiddenEquip<CrystalHoney>(EquipType.Waist);
		API.AddHiddenEquip<DarkGlaze>(EquipType.Waist);
		API.AddHiddenEquip<TheLostCross>(EquipType.Waist);
		API.AddHiddenEquip<RingofUnity>(EquipType.HandsOn);
		API.AddHiddenEquip<PotionChaser>(EquipType.Waist);
		API.AddHiddenEquip<NecroticSkull>(EquipType.Waist);
		API.AddHiddenEquip<SoulStone>(EquipType.Waist);
		API.AddHiddenEquip<Canteen>(EquipType.Waist);
		API.AddHiddenEquip<DeadEyePatch>(EquipType.Face);
		API.AddHiddenEquip<MermaidCanteen>(EquipType.Waist);

#if TML_2022_09
		API.AddHiddenEquip<ApothecaryLife>(EquipType.Waist);
		API.AddHiddenEquip<ApothecaryMana>(EquipType.Waist);
		API.AddHiddenEquip<CrystalArcanum>(EquipType.Waist);
		API.AddHiddenEquip<LifeQuartzGem>(EquipType.Waist);
		API.AddHiddenEquip<NightShadePetal>(EquipType.Face);
#else
		API.AddHiddenEquip<ApothecarysCyanVial>(EquipType.Waist);
		API.AddHiddenEquip<ApothecarysScarletVial>(EquipType.Waist);
		API.AddHiddenEquip<CrystalArcanite>(EquipType.Waist);
		API.AddHiddenEquip<LifeGem>(EquipType.Waist);
		API.AddHiddenEquip<NightShadeFlower>(EquipType.Face);
#endif

		// Hairpin, needs hair override.
		API.AddSmallHead<NoteHairpin>();

		// Balloons
		API.AddBalloon<AutoTuner>();
		API.AddBalloon<DevilsSubwoofer>();
		API.AddBalloon<Subwoofer>();
		API.AddBalloon<TerrariumSurroundSound>();
		API.AddBalloon<HeartOfTheJungle>();
		API.AddBalloon<IncandescentSpark>();
		API.AddBalloon<UpDownBalloon>();
		API.AddBalloon<EyeoftheStorm>();
		API.AddBalloon<MirroroftheBeholder>();
		API.AddBalloon<DarkHeart>();
		API.AddBalloon<Phylactery>();
		API.AddBalloon<OlympicTorch>();

#if TML_2022_09
		API.AddBalloon<SpiritFlame>();
#else
		API.AddBalloon<InnerFlame>();
#endif

		API.RegisterBalloonDrawLayer<BalloonLayer>();

		// Sheaths
		API.AddSpecialItem(ModContent.ItemType<GardenersSheath>(), AsymmetricEquipsAPI.PlayerSide.Left);
		API.AddSpecialItem(ModContent.ItemType<LeatherSheath>(), AsymmetricEquipsAPI.PlayerSide.Left);
		API.AddSpecialItem(ModContent.ItemType<LeechingSheath>(), AsymmetricEquipsAPI.PlayerSide.Left);
		API.AddSpecialItem(ModContent.ItemType<TitanSlayerSheath>(), AsymmetricEquipsAPI.PlayerSide.Left);
		API.AddSpecialItem(ModContent.ItemType<WrithingSheath>(), AsymmetricEquipsAPI.PlayerSide.Left);

		// Custom textures
		API.AddFlippedEquip<SupportSash>(EquipType.Waist);

		// Sprites will be needed for:
		// - Some HandsOn equips
		// - Some pouch/bag equips
	}
}