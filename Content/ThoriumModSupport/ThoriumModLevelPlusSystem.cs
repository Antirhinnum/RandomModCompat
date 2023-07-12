using RandomModCompat.Common.Configs;
using RandomModCompat.Common.ExplicitSupport;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.ThoriumModSupport;

[JITWhenModsEnabled(_modName)]
internal sealed class ThoriumModLevelPlusSystem : CrossModHandler
{
	private const string _modName = ModNames.ThoriumMod;
	public override string ModName => _modName;
	private static ThoriumModLevelPlusConfig Config => ModContent.GetInstance<ThoriumModLevelPlusConfig>();

	private static DamageClass _bardDamage;
	private static DamageClass _healerDamage;

	internal override void PostSetupContent()
	{
		if (!RandomModCompat.SupportEnabled(_modName, ModNames.levelplus))
		{
			return;
		}

		if (!CrossMod.TryFind("BardDamage", out _bardDamage) || !CrossMod.TryFind("HealerDamage", out _healerDamage))
		{
			Mod.Logger.Warn("Cannot find Thorium's damage types for Level+ compatibility!");
			return;
		}

		// Damage and crit
		LevelplusSupportSystem.AddDamageAndCritEffects(LevelplusSupportSystem.Stat.Charisma, _bardDamage, () => Config.BardDamagePerPoint, () => Config.PointsPerBardCrit);
		LevelplusSupportSystem.AddDamageAndCritEffects(LevelplusSupportSystem.Stat.Intelligence, _healerDamage, () => Config.HealerDamagePerPoint, () => Config.PointsPerHealerCrit);

		// TODO: Inspiration is capped at 60.
		// TODO: Bonus healing? Things other than just damage.
	}
}