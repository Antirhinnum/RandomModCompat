using PrimeRework.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace RandomModCompat.Content.ExplicitSupport;

/// <summary>
/// Tracks if the "Caretaker" boss from "Mech Boss Rework" has been downed.
/// <br/> The mod doesn't have a downed bool for the boss.
/// </summary>
[JITWhenModsEnabled(_modName)]
internal sealed class PrimeReworkCaretakerKillTracker : GlobalNPC
{
	private const string _modName = ModNames.PrimeRework;

	public override bool IsLoadingEnabled(Mod mod)
	{
		return ModLoader.HasMod(_modName);
	}

	public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
	{
		return entity.type == ModContent.NPCType<Caretaker>();
	}

	public override void OnKill(NPC npc)
	{
		NPC.SetEventFlagCleared(ref PrimeReworkCaretakerDownedSystem.caretakerDowned, -1);
	}
}