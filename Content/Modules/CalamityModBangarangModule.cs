using CalamityMod;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Projectiles.Rogue;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class CalamityModBangarangModule : CrossModModule<BangarangAPI>
{
	public override string CrossModName => ModNames.CalamityMod;

	protected internal override void PostSetupContent()
	{
		// Melee
		API.RegisterSimpleBoomerang<FallenPaladinsHammer>(-1);
		API.RegisterSimpleBoomerang<GalaxySmasher>(-1);
		API.RegisterSimpleBoomerang<SeekingScorcher>(-1);
		API.RegisterSimpleBoomerang<StellarContempt>(-1);
		API.RegisterSimpleBoomerang<TriactisTruePaladinianMageHammerofMightMelee>(-1);

		// Rogue
		API.RegisterSimpleBoomerang<BlazingStar>(3);
		API.RegisterSimpleBoomerang<Brimblade>(-1);
		API.RegisterSimpleBoomerang<Celestus>(-1);
		API.RegisterSimpleBoomerang<DynamicPursuer>(-1);
		API.RegisterSimpleBoomerang<ElementalDisk>(-1);
		API.RegisterSimpleBoomerang<EnchantedAxe>(-1);
		API.RegisterSimpleBoomerang<EpidemicShredder>(-1);
		API.RegisterSimpleBoomerang<Equanimity>(-1);
		API.RegisterSimpleBoomerang<Eradicator>(-1);
		API.RegisterSimpleBoomerang<FishboneBoomerang>(1);
		API.RegisterSimpleBoomerang<FrostcrushValari>(-1);
		API.RegisterSimpleBoomerang<GhoulishGouger>(-1);
		API.RegisterSimpleBoomerang<Glaive>(3);
		API.RegisterSimpleBoomerang<Icebreaker>(-1);
		API.RegisterSimpleBoomerang<InfestedClawmerang>(-1);
		API.RegisterSimpleBoomerang<KelvinCatalyst>(-1);
		API.RegisterSimpleBoomerang<Kylie>(-1);
		API.RegisterSimpleBoomerang<MangroveChakram>(-1);
		API.RegisterSimpleBoomerang<MoltenAmputator>(-1);
		API.RegisterSimpleBoomerang<NanoblackReaper>(-1);
		API.RegisterSimpleBoomerang<SubductionSlicer>(-1);
		API.RegisterSimpleBoomerang<TerraDisk>(-1);
		API.RegisterSimpleBoomerang<ToxicantTwister>(-1);
		API.RegisterSimpleBoomerang<Valediction>(-1);

		// Draedon
		API.RegisterSimpleBoomerang<TrackingDisk>(-1);

		// Complex
		static bool SandDollarCheck(Player p, Item i, int n) => p.Calamity().StealthStrikeAvailable() || p.ownedProjectileCounts[i.shoot] < 2 + n;
		static bool DefectiveSphereCheck(Player p, Item i, int n)
		{
			int[] projectileTypes = new int[]
			{
				ModContent.ProjectileType<SphereSpiked>(),
				ModContent.ProjectileType<SphereBladed>(),
				ModContent.ProjectileType<SphereYellow>(),
				ModContent.ProjectileType<SphereBlue>()
			};
			return p.Calamity().StealthStrikeAvailable() || projectileTypes.Select(type => p.ownedProjectileCounts[type]).Sum() < 5 + n;
		}

		API.RegisterSimpleBoomerang<SandDollar>(2, SandDollarCheck);
		API.RegisterSimpleBoomerang<DefectiveSphere>(5, DefectiveSphereCheck);
	}
}