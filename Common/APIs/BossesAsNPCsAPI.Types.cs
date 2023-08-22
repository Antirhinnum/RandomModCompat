namespace RandomModCompat.Common.APIs;

internal sealed partial class BossesAsNPCsAPI
{
	internal enum DownedBoss
	{
		Betsy,
		DungeonGuardian,
		DarkMage,
		Ogre,
		GoblinSummoner,
		Dreadnautilus,
		Mothron,
		EoW,
		BoC
	}

	internal enum SpawnableTownNPC
	{
		KingSlime,
		EyeOfCthulhu,
		EaterOfWorlds,
		BrainOfCthulhu,
		QueenBee,
		Skeletron,
		Deerclops,
		WallOfFlesh,
		QueenSlime,
		Destroyer,
		Twins,
		SkeletronPrime,
		Plantera,
		Golem,
		EmpressOfLight,
		DukeFishron,
		Betsy,
		LunaticCultist,
		MoonLord,
		Dreadnautilus,
		Mothron,
		Pumpking,
		IceQueen,
		MartianSaucer,
		TorchGod
	}

	internal enum SellingTownNPC
	{
		KingSlime,
		EyeOfCthulhu,
		EaterOfWorlds,
		BrainOfCthulhu,
		QueenBee,
		Skeletron,
		Deerclops,
		WallOfFlesh,
		QueenSlime,
		TheDestroyer,
		Retinazer,
		Spazmatism,
		SkeletronPrime,
		Plantera,
		Golem,
		EmpressOfLight,
		DukeFishron,
		Betsy,
		LunaticCultist,
		MoonLord,
		Dreadnautilus,
		Mothron,
		Pumpking,
		IceQueen,
		MartianSaucer,
		GoblinTinkerer,
		Pirate
	}

	internal enum PriceType
	{
		DefaultPrice,
		CustomPrice,
		WithDiv,
		WithDivAndMulti
	}

#if !TML_2022_09
	internal enum ConditionType
	{
		TownNPCsCrossModSupport,
		SellExtraItems,
		GoblinSellInvasionItems,
		PirateSellInvasionItems,
		IsNotNpcShimmered,
		Expert,
		Master,
		DaytimeEoLDefated,
		DownedBetsy,
		DownedDungeonGuardian,
		DownedDarkMage,
		DownedOgre,
		DownedGoblinWarlock,
		DownedMothron,
		DownedDreadnautilus,
		DownedEaterOfWorlds,
		DownedBrainOfCthulhu,
		DownedWallOfFlesh,
		RescuedWizard,
		UnlockOWMusicOrDrunkWorld,
		CorruptionOrHardmode,
		CrimsonOrHardmode,
		UndergroundCavernsOrHardmode,
		HallowOrCorruptionOrCrimson,
		InIceAndHallowOrCorruptionOrCrimson
	}
#endif
}