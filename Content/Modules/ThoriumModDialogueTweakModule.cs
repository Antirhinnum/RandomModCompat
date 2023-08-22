// Thorium Mod has it sown Dialogue Panel Rework support in 1.4.4.
#if TML_2022_09
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.Consumable;
using ThoriumMod.Items.HealerItems;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.Tracker;
using ThoriumMod.NPCs;

namespace RandomModCompat.Content.Modules;

internal sealed class ThoriumModDialogueTweakModule : CrossModModule<DialogueTweakAPI>
{
	public override string CrossModName => ModNames.ThoriumMod;

	protected internal override void PostSetupContent()
	{
		const string BestiaryIconsPath = "Terraria/Images/UI/Bestiary/Icon_Tags_Shadow";
		static Func<Rectangle> BestiaryIconFrame(int index) => () => ModContent.Request<Texture2D>(BestiaryIconsPath).Frame(16, 5, index % 16, index / 16);

		API.ReplaceExtraButtonIcon(new List<int> { ModContent.NPCType<Cobbler>() }, "Terraria/Images/Item_" + ItemID.ArmorPolish);
		API.ReplaceExtraButtonIcon(new List<int> { ModContent.NPCType<ConfusedZombie>() }, ModContent.GetInstance<ZombieRepellent>().Texture);
		API.ReplaceExtraButtonIcon(new List<int> { ModContent.NPCType<Cook>() }, "Terraria/Images/Item_" + ItemID.ChefHat);
		API.ReplaceExtraButtonIcon(new List<int> { ModContent.NPCType<DesertAcolyte>() }, BestiaryIconsPath, null, BestiaryIconFrame(43));
		API.ReplaceExtraButtonIcon(new List<int> { ModContent.NPCType<Diverman>() }, "Terraria/Images/Bubble");
		API.ReplaceExtraButtonIcon(new List<int> { ModContent.NPCType<Spiritualist>() }, ModContent.GetInstance<PurityShards>().Texture);
		API.ReplaceShopButtonIcon(new List<int> { ModContent.NPCType<Tracker>() }, ModContent.GetInstance<VanquisherMedal>().Texture);
		API.ReplaceExtraButtonIcon(new List<int> { ModContent.NPCType<WeaponMaster>() }, ModContent.GetInstance<ExileHelmet>().Texture);
	}
}
#endif