using Terraria;

namespace RandomModCompat.Common.APIs;

internal sealed partial class BangarangAPI
{
	internal delegate bool BangarangUseCheck(Player player, Item item, int extraBoomerangs);
}