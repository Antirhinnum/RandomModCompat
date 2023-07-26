using Terraria;

namespace RandomModCompat.Common.APIs;

internal sealed partial class LevelplusAPI
{
	internal delegate void ApplyBonus(Player player, ushort statValue);

	internal delegate string GetDescription(ushort statValue);

	internal readonly struct StatBonus
	{
		internal Stat Stat { get; init; }
		internal ApplyBonus Apply { get; init; }
		internal GetDescription Description { get; init; }

		public StatBonus(Stat stat, ApplyBonus apply, GetDescription description)
		{
			Stat = stat;
			Apply = apply;
			Description = description;
		}
	}

	internal enum Stat
	{
		Constitution,
		Strength,
		Intelligence,
		Charisma,
		Dexterity,
		Mobility,
		Excavation,
		Animalia,
		Luck,
		Mysticism
	}
}