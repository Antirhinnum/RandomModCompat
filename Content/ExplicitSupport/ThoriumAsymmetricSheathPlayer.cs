using RandomModCompat.Common.APIs;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using ThoriumMod.Items;
using ThoriumMod.PlayerLayers;

namespace RandomModCompat.Content.ExplicitSupport;

[JITWhenModsEnabled(BaseMod)]
[Autoload(false)]
internal sealed class ThoriumAsymmetricSheathPlayer : ModPlayer
{
	private const string BaseMod = ModNames.ThoriumMod;

	/// <summary>
	/// The current <see cref="AccessoryType.SwordSheath"/> that this player has equipped.
	/// </summary>
	internal Item currentSheath;

#if TML_2022_09
	public override void Load()
	{
		On.Terraria.Player.UpdateVisibleAccessory += TrackVisibleSheath;
	}

	public override void Unload()
	{
		On.Terraria.Player.UpdateVisibleAccessory -= TrackVisibleSheath;
	}

	private void TrackVisibleSheath(On.Terraria.Player.orig_UpdateVisibleAccessory orig, Player self, int itemSlot, Item item, bool modded)
#else

	public override void Load()
	{
		On_Player.UpdateVisibleAccessory += TrackVisibleSheath;
	}

	public override void Unload()
	{
		On_Player.UpdateVisibleAccessory -= TrackVisibleSheath;
	}

	private void TrackVisibleSheath(On_Player.orig_UpdateVisibleAccessory orig, Player self, int itemSlot, Item item, bool modded)
#endif
	{
		orig(self, itemSlot, item, modded);

		if (item.ModItem is ThoriumItem tItem && tItem.accessoryType == AccessoryType.SwordSheath && self.TryGetModPlayer(out ThoriumAsymmetricSheathPlayer aPlayer))
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
		AsymmetricEquipsAPI api = ModContent.GetInstance<AsymmetricEquipsAPI>();
		Item playerSheath = player.GetModPlayer<ThoriumAsymmetricSheathPlayer>().currentSheath;
		return playerSheath == null || api.ItemOnDefaultSide(playerSheath, player);
	}
}