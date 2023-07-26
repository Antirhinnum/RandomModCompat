using DBZMODPORT.Tiles;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria;

namespace RandomModCompat.Content.Modules;

internal sealed class DBZMODPORTUniversalCraftModule : CrossModModule<UniversalCraftAPI>
{
	public override string CrossModName => ModNames.DBZMODPORT;

	protected internal override void PostSetupContent()
	{
		API.AddStation<ZTable>();
		API.AddStation<KaiTable>(() => NPC.downedPlantBoss);
	}
}