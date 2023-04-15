﻿using Terraria.ModLoader;

namespace RandomModCompat.Core;

/// <summary>
/// Handles basic cross-mod call support.
/// </summary>
internal abstract class CrossModHandler : ModType
{
	/// <summary>
	/// The mod this handler adds support for.
	/// </summary>
	protected Mod CrossMod => ModLoader.TryGetMod(ModName, out Mod result) ? result : null;

	/// <summary>
	/// Checks if the mod named <see cref="ModName"/> is currently loaded.<br/>
	/// <b>If <see langword="false"/>, then no method in this class will be called.</b>
	/// </summary>
	internal bool IsModLoaded => CrossMod != null;

	/// <summary>
	/// The internal name of the mod this handler adds support for.
	/// </summary>
	protected abstract string ModName { get; }

	public override sealed bool IsLoadingEnabled(Mod mod)
	{
		return ModLoader.HasMod(ModName);
	}

	/// <inheritdoc cref="ModSystem.OnModLoad"/>
	internal virtual void OnModLoad()
	{ }

	/// <inheritdoc cref="ModSystem.PostSetupContent"/>
	internal virtual void PostSetupContent()
	{ }

	/// <summary>
	/// Allows loading after everything else has been loaded, including localization.<br/>
	/// This runs after <see cref="ModSystem.PostAddRecipes"/>, so some mods may disallow calls at this point.
	/// </summary>
	/// <param name="mod"></param>
	internal virtual void PostSetupEverything()
	{ }

	protected override void Register()
	{
		ModTypeLookup<CrossModHandler>.Register(this);
		CrossModSystem._handlers.Add(this);
	}
}