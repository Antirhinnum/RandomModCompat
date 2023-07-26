namespace RandomModCompat.Common.APIs;

internal sealed partial class TerraTypingAPI
{
	internal enum CallTypes
	{
		AddTypes,
		OverrideTypes
	}

	internal enum TypeToAdd
	{
		Ammo,
		Armor,
		NPC,
		Projectile,
		SpecialItem,
		Weapon
	}
}