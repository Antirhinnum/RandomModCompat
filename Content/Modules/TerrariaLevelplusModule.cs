using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class TerrariaLevelplusModule : CrossModModule<LevelplusAPI>
{
	public override string CrossModName { get; } = ModNames.Terraria;

	protected internal override void PostSetupContent()
	{
		API.AddDamageAndCritEffects(LevelplusAPI.Stat.Dexterity, DamageClass.Throwing);
	}
}