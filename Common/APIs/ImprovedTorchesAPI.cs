using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

internal sealed partial class ImprovedTorchesAPI : ModAPI
{
	protected internal override string ModName => ModNames.ImprovedTorches;

	#region Custom

	internal void RegisterTorch(int tileId)
	{
		ImprovedTorchesSupportSystem.registeredTorches.Add(tileId);
	}

	internal void RegisterTorch<T>() where T : ModTile
	{
		RegisterTorch(ModContent.TileType<T>());
	}

	#endregion Custom
}