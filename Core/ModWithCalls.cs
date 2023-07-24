using Terraria.ModLoader;

namespace RandomModCompat.Core;

/// <summary>
/// Wraps around another mod's <see cref="Mod.Call(object[])"/> API.
/// </summary>
internal abstract class ModWithCalls : ModType
{
	/// <summary>
	/// The mod to use calls on.
	/// </summary>
	protected Mod CalledMod => _calledMod;

	private Mod _calledMod;

	/// <summary>
	/// The internal name of the mod.
	/// </summary>
	protected internal abstract string ModName { get; }

	/// <summary>
	/// <see langword="true"/> if this caller can be used, <see langword="false"/> otherwise.
	/// </summary>
	internal bool Active => ModLoader.HasMod(ModName) && SupportEnabled();

	public override sealed void Load()
	{
		ModLoader.TryGetMod(ModName, out _calledMod);
	}

	protected override sealed void Register()
	{
		ModTypeLookup<ModWithCalls>.Register(this);
	}

	/// <summary>
	/// Allows conditionally blocking support.
	/// <br/> Returns <see langword="true"/> by default.
	/// </summary>
	/// <returns><see langword="true"/> if support is allowed, <see langword="false"/> otherwise.</returns>
	protected virtual bool SupportEnabled()
	{
		return true;
	}
}