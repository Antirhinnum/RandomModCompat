using PrimeRework.NPCs;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class PrimeReworkThoriumModModule : CrossModModule<ThoriumModAPI>
{
	public override string CrossModName => ModNames.PrimeRework;

	protected internal override void PostSetupEverything()
	{
		API.AddMechanicalBossDrops(ModContent.NPCType<TheTerminator>());
		API.AddMechanicalBossDrops(ModContent.NPCType<Caretaker>());
	}
}