using System.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RandomModCompat.Content.PrimeReworkSupport;

/// <summary>
/// Tracks if the "Caretaker" boss from "Mech Boss Rework" has been downed.
/// <br/> The mod doesn't have a downed bool for the boss.
/// <br/> Should always load. Otherwise, the data might get lost if this mod is enabled without "Mech Boss Rework".
/// </summary>
internal sealed class PrimeReworkCaretakerDownedSystem : ModSystem
{
	internal static bool caretakerDowned;

	// TODO: Convert to ClearWorld on 1.4.4.
	public override void OnWorldLoad()
	{
		caretakerDowned = false;
	}

	public override void OnWorldUnload()
	{
		caretakerDowned = false;
	}

	public override void SaveWorldData(TagCompound tag)
	{
		if (caretakerDowned)
		{
			tag[nameof(caretakerDowned)] = true;
		}
	}

	public override void LoadWorldData(TagCompound tag)
	{
		caretakerDowned = tag.ContainsKey(nameof(caretakerDowned));
	}

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write(caretakerDowned);
	}

	public override void NetReceive(BinaryReader reader)
	{
		caretakerDowned = reader.ReadBoolean();
	}
}