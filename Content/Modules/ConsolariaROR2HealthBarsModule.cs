using Consolaria.Content.NPCs.Bosses.Ocram;
using Consolaria.Content.NPCs.Bosses.Turkor;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class ConsolariaROR2HealthBarsModule : CrossModModule<ROR2HealthBarsAPI>
{
	private const string _localizationHeader = $"Mods.RandomModCompat.{ModNames.Consolaria}.";

	public override string CrossModName => ModNames.Consolaria;

	protected internal override void PostSetupContent()
	{
		int lepusType = ModContent.Find<ModNPC>(ModNames.Consolaria, "Lepus").Type;

		API.AddDesc(lepusType, _localizationHeader, "Lepus");
		API.AddDesc<Ocram>(_localizationHeader, "Ocram");
		API.AddDesc<TurkortheUngrateful>(_localizationHeader, "Turkor");
	}
}