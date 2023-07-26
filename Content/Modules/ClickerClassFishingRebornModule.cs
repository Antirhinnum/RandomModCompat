using ClickerClass.Items.Accessories;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class ClickerClassFishingRebornModule : CrossModModule<FishingRebornAPI>
{
	public override string CrossModName => ModNames.ClickerClass;

	protected internal override void PostSetupContent()
	{
		API.AddToPool("LavaCatchPool", ModContent.ItemType<HotKeychain>(), 0.05f);
	}
}