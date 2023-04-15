using RandomModCompat.Common;
using RandomModCompat.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using ThoriumMod.Items;
using ThoriumMod.PlayerLayers;

namespace RandomModCompat.Content.ThoriumModSupport;

[JITWhenModsEnabled("ThoriumMod")]
internal sealed class ThoriumAsymmetricSheathPlayer : ModPlayer
{
	internal Item currentSheath;

	public override bool IsLoadingEnabled(Mod mod)
	{
		return ModLoader.HasMod("ThoriumMod") && ModLoader.HasMod("AsymmetricEquips");
	}

	public override void Load()
	{
		On.Terraria.Player.UpdateVisibleAccessory += TrackVisibleSheath;
	}

	public override void Unload()
	{
		On.Terraria.Player.UpdateVisibleAccessory -= TrackVisibleSheath;
	}

	private void TrackVisibleSheath(On.Terraria.Player.orig_UpdateVisibleAccessory orig, Player self, int itemSlot, Item item, bool modded)
	{
		orig(self, itemSlot, item, modded);

		if (item.ModItem is ThoriumItem tItem && tItem.accessoryType == AccessoryType.SwordSheath && self.TryGetModPlayer<ThoriumAsymmetricSheathPlayer>(out var aPlayer))
		{
			aPlayer.currentSheath = item;
		}
	}

	public override void ResetEffects()
	{
		currentSheath = null;
	}

	public override void ModifyDrawLayerOrdering(IDictionary<PlayerDrawLayer, PlayerDrawLayer.Position> positions)
	{
		// Default position is before BackAcc.
		positions[ModContent.GetInstance<SheathLayer>()] = new PlayerDrawLayer.Multiple()
		{
			{ new PlayerDrawLayer.Between(PlayerDrawLayers.HairBack, PlayerDrawLayers.BackAcc), drawInfo => ShouldDrawSheathBack(drawInfo.drawPlayer) },
			{ new PlayerDrawLayer.Between(PlayerDrawLayers.WaistAcc, PlayerDrawLayers.NeckAcc), drawInfo => !ShouldDrawSheathBack(drawInfo.drawPlayer) }
		};
	}

	private static bool ShouldDrawSheathBack(Player player)
	{
		ModWithCalls.TryGetCaller<AsymmetricEquipsCaller>(out var caller);
		Item playerSheath = player.GetModPlayer<ThoriumAsymmetricSheathPlayer>().currentSheath;
		return (playerSheath == null) || caller.ItemOnDefaultSide(playerSheath, player);
	}
}