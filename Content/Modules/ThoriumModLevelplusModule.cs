using RandomModCompat.Common.APIs;
using RandomModCompat.Common.Configs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class ThoriumModLevelplusModule : CrossModModule<LevelplusAPI>
{
	public override string CrossModName => ModNames.ThoriumMod;

	protected internal override void PostSetupContent()
	{
		if (!CrossMod.TryFind("BardDamage", out DamageClass bardDamage) || !CrossMod.TryFind("HealerDamage", out DamageClass healerDamage))
		{
			Mod.Logger.Warn("Cannot find Thorium's damage types for Level+ compatibility!");
			return;
		}

		LevelPlusValuesConfig config = LevelPlusValuesConfig.Instance;

		// Damage and crit
		API.AddDamageAndCritEffects(LevelplusAPI.Stat.Charisma, bardDamage, () => config.BardDamagePerPoint, () => config.PointsPerBardCrit);
		API.AddDamageAndCritEffects(LevelplusAPI.Stat.Intelligence, healerDamage, () => config.HealerDamagePerPoint, () => config.PointsPerHealerCrit);

		// TODO: Inspiration is capped at 60.
		// TODO: Bonus healing? Things other than just damage.
	}
}