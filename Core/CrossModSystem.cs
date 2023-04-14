using System.Collections.Generic;
using Terraria.ModLoader;

namespace RandomModCompat.Core;

/// <summary>
/// Handles cross-mod call support.
/// </summary>
public sealed partial class CrossModSystem : ModSystem
{
	internal static readonly List<CrossModHandler> _handlers = new();

	public override void OnModLoad()
	{
		foreach (CrossModHandler handler in _handlers)
		{
			if (handler.IsModLoaded)
			{
				handler.OnModLoad();
			}
		}
	}

	public override void SetupContent()
	{
		foreach (CrossModHandler handler in _handlers)
		{
			if (handler.IsModLoaded)
			{
				handler.SetupContent();
			}
		}
	}

	public override void PostSetupContent()
	{
		foreach (CrossModHandler handler in _handlers)
		{
			if (handler.IsModLoaded)
			{
				handler.PostSetupContent();
			}
		}
	}

	public override void PostAddRecipes()
	{
		foreach (CrossModHandler handler in _handlers)
		{
			if (handler.IsModLoaded)
			{
				handler.PostSetupEverything();
			}
		}
	}

	public override void Unload()
	{
		_handlers.Clear();
	}
}