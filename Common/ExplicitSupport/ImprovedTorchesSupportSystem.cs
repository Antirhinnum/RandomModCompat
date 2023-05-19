using ImprovedTorches;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace RandomModCompat.Common.ExplicitSupport;

[JITWhenModsEnabled(ModName)]
internal sealed class ImprovedTorchesSupportSystem : GlobalTile
{
	internal const string ModName = ModNames.ImprovedTorches;

	private static readonly HashSet<int> _registeredTorches = new();

	public override bool IsLoadingEnabled(Mod mod)
	{
		return ModLoader.HasMod(ModName);
	}

	// Always runs after ModTile.ModifyLight, so multiplying should always work.
	public override void ModifyLight(int i, int j, int type, ref float r, ref float g, ref float b)
	{
		if (_registeredTorches.Contains(type) // Registered
			&& !ModContent.GetInstance<ImprovedTorchesConfigClient>().DisableTorchFlickering // Not disabled
			&& Main.tile[i, j].TileFrameX < 66) // Not put out
		{
			// From ImprovedTorches' own impl.
			float multiplier = Utils.SelectRandom(Main.rand, 0.93f, 1f);
			r *= multiplier;
			g *= multiplier;
			b *= multiplier;
		}
	}

	internal static void RegisterTorch(int tileId)
	{
		_registeredTorches.Add(tileId);
	}

	internal static void RegisterTorch<T>() where T : ModTile
	{
		RegisterTorch(ModContent.TileType<T>());
	}
}