using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class BangarangTerraTypingModule : CrossModModule<TerraTypingAPI>
{
	public override string CrossModName => ModNames.Bangarang;

	protected internal override void PostSetupContent()
	{
		API.AddTypes(TerraTypingAPI.TypeToAdd.Projectile, CrossMod);
		API.AddTypes(TerraTypingAPI.TypeToAdd.SpecialItem, CrossMod);
		API.AddTypes(TerraTypingAPI.TypeToAdd.Weapon, CrossMod);
	}
}