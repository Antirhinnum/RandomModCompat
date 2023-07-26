using Consolaria.Content.Buffs;
using Consolaria.Content.Items.Weapons.Summon;
using Consolaria.Content.Projectiles.Friendly;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class ConsolariaSummonersAssociationModule : CrossModModule<SummonersAssociationAPI>
{
	public override string CrossModName => ModNames.Consolaria;

	protected internal override void PostSetupContent()
	{
		API.AddMinionInfo(ModContent.ItemType<EternityStaff>(), ModContent.BuffType<Consolaria.Content.Buffs.EyeOfEternity>(), ModContent.ProjectileType<Consolaria.Content.Projectiles.Friendly.EyeOfEternity>());
		API.AddMinionInfo(ModContent.ItemType<TurkeyStuff>(), ModContent.BuffType<WeirdTurkey>(), ModContent.ProjectileType<TurkeyHead>());
	}
}