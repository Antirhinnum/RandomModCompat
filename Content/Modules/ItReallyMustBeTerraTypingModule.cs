using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class ItReallyMustBeTerraTypingModule : CrossModModule<TerraTypingAPI>
{
	public override string CrossModName => ModNames.ItReallyMustBe;

	protected internal override void PostSetupContent()
	{
		API.AddTypes(TerraTypingAPI.TypeToAdd.Weapon, CrossMod);
	}
}