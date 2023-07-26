using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class PrimeReworkTerraTypingModule : CrossModModule<TerraTypingAPI>
{
	public override string CrossModName => ModNames.PrimeRework;

	protected internal override void PostSetupContent()
	{
		API.AddTypes(TerraTypingAPI.TypeToAdd.NPC, CrossMod);
		API.AddTypes(TerraTypingAPI.TypeToAdd.Projectile, CrossMod);
		API.AddTypes(TerraTypingAPI.TypeToAdd.SpecialItem, CrossMod);
		API.AddTypes(TerraTypingAPI.TypeToAdd.Weapon, CrossMod);
	}
}