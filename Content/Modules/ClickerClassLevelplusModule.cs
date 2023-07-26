using ClickerClass;
using RandomModCompat.Common.APIs;
using RandomModCompat.Common.Configs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class ClickerClassLevelplusModule : CrossModModule<LevelplusAPI>
{
	public override string CrossModName => ModNames.ClickerClass;

	protected internal override void PostSetupContent()
	{
		if (!CrossMod.TryFind(nameof(ClickerDamage), out DamageClass clickerDamage))
		{
			Mod.Logger.Warn("Cannot find clicker damage for Level+ support!");
			return;
		}

		LevelPlusValuesConfig config = LevelPlusValuesConfig.Instance;
		API.AddDamageAndCritEffects(LevelplusAPI.Stat.Dexterity, clickerDamage, () => config.ClickDamagePerPoint, () => config.PointsPerClickCrit);

		// TODO: More stats.
	}
}