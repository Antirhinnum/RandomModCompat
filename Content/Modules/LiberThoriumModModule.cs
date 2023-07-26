using Liber.Content.NPCs.HardMode.Undead;
using Liber.Content.NPCs.PreHardMode.Animals;
using Liber.Content.NPCs.PreHardMode.Beasts;
using Liber.Content.NPCs.PreHardMode.Undead;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;

namespace RandomModCompat.Content.Modules;

internal sealed class LiberThoriumModModule : CrossModModule<ThoriumModAPI>
{
	public override string CrossModName => ModNames.Liber;

	protected internal override void PostSetupContent()
	{
		API.AddRepelledEnemy<JiangshiA>(ThoriumModAPI.ThoriumRepellentType.Zombie);
		API.AddRepelledEnemy<ZombieBlistered>(ThoriumModAPI.ThoriumRepellentType.Zombie);
		API.AddRepelledEnemy<ZombieBloated>(ThoriumModAPI.ThoriumRepellentType.Zombie);
		API.AddRepelledEnemy<ZombieFrostbite>(ThoriumModAPI.ThoriumRepellentType.Zombie);
		API.AddRepelledEnemy<ZombieFrozenPink>(ThoriumModAPI.ThoriumRepellentType.Zombie);
		API.AddRepelledEnemy<ZombieHive>(ThoriumModAPI.ThoriumRepellentType.Zombie);
		API.AddRepelledEnemy<ZombieHusk>(ThoriumModAPI.ThoriumRepellentType.Zombie);
		API.AddRepelledEnemy<ZombiePirate>(ThoriumModAPI.ThoriumRepellentType.Zombie);
		API.AddRepelledEnemy<ZombieSlimeGreen>(ThoriumModAPI.ThoriumRepellentType.Zombie);
		API.AddRepelledEnemy<ZombieSlimePurple>(ThoriumModAPI.ThoriumRepellentType.Zombie);
		API.AddRepelledEnemy<ZombieTunic>(ThoriumModAPI.ThoriumRepellentType.Zombie);
		API.AddRepelledEnemy<ZombieVile>(ThoriumModAPI.ThoriumRepellentType.Zombie);
		API.AddRepelledEnemy<ZombieFlying>(ThoriumModAPI.ThoriumRepellentType.Zombie);
		API.AddRepelledEnemy<ZombieFlyingBody>(ThoriumModAPI.ThoriumRepellentType.Zombie);
		API.AddRepelledEnemy<ZombieFlyingLegs>(ThoriumModAPI.ThoriumRepellentType.Zombie);

		API.AddRepelledEnemy<GiantSkeleton>(ThoriumModAPI.ThoriumRepellentType.Skeleton);
		API.AddRepelledEnemy<SkeletonSlinger>(ThoriumModAPI.ThoriumRepellentType.Skeleton);
		API.AddRepelledEnemy<SkeletonSoldier>(ThoriumModAPI.ThoriumRepellentType.Skeleton);
		API.AddRepelledEnemy<GiantSkeletonAncient>(ThoriumModAPI.ThoriumRepellentType.Skeleton);

		API.AddRepelledEnemy<BatChill>(ThoriumModAPI.ThoriumRepellentType.Bat);
		API.AddRepelledEnemy<BatSandy>(ThoriumModAPI.ThoriumRepellentType.Bat);

		API.AddRepelledEnemy<Mudfly>(ThoriumModAPI.ThoriumRepellentType.Bug);
		// TODO: Centipede (internal class)
	}
}