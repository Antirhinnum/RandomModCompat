using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class ConsolariaTerraTypingModule : CrossModModule<TerraTypingAPI>
{
	public override string CrossModName => ModNames.Consolaria;

	protected internal override void PostSetupContent()
	{
		API.AddTypes(TerraTypingAPI.TypeToAdd.Ammo, CrossMod);
		API.AddTypes(TerraTypingAPI.TypeToAdd.Armor, CrossMod);
		API.AddTypes(TerraTypingAPI.TypeToAdd.NPC, CrossMod);
		API.AddTypes(TerraTypingAPI.TypeToAdd.Projectile, CrossMod);
		API.AddTypes(TerraTypingAPI.TypeToAdd.Weapon, CrossMod);
	}
}