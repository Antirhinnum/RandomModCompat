using RandomModCompat.Common.Callers;
using RandomModCompat.Core;
using SlugNPCs.NPCs;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace RandomModCompat.Content;

[JITWhenModsEnabled(_modName)]
internal sealed class SlugNPCsSystem : CrossModHandler
{
	private const string _modName = "SlugNPCs";
	public override string ModName => _modName;

	/*
	 * This file adds support for:
	 * - Census
	 * - Dialogue Panel Rework
	 */

	internal override void PostSetupContent()
	{
		CensusSupport();
		DialogueTweakSupport();
	}

	private void CensusSupport()
	{
		if (!TryGetCaller(out CensusCaller caller))
		{
			return;
		}

		caller.TownNPCCondition<AlbinoSlug>("When the Eye of Cthulhu has been defeated");
		caller.TownNPCCondition<CryptsSlug>("When the Deerclops has been defeated");
		caller.TownNPCCondition<DevourerSlug>("When the Moon Lord has been defeated");
		caller.TownNPCCondition<StormSlug>("When Duke Fishron has been defeated");
		caller.TownNPCCondition<UniverseSlug>("When the any player is carrying the Zenith");
	}

	private void DialogueTweakSupport()
	{
		if (!TryGetCaller(out DialogueTweakCaller caller))
		{
			return;
		}

		caller.ReplaceShopButtonIcon(new List<int>()
		{
			ModContent.NPCType<AlbinoSlug>(),
			ModContent.NPCType<CryptsSlug>(),
			ModContent.NPCType<DevourerSlug>(),
			ModContent.NPCType<StormSlug>(),
			ModContent.NPCType<UniverseSlug>()
		}, "Head");
	}
}