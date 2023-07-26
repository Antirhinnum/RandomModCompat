using Terraria.ModLoader;

namespace RandomModCompat.Core;

/// <summary>
/// Wraps around another mod's API, either through <see cref="Mod.Call(object[])"/> or through an API explicitly implemented by this mod.
/// </summary>
public abstract class ModAPI : ModType
{
	/// <summary>
	/// The mod to call <see cref="Mod.Call(object[])"/> on.
	/// </summary>
	protected Mod WrappedMod => _wrappedMod;

	private Mod _wrappedMod;

	/// <summary>
	/// The internal name of the mod to wrap.
	/// </summary>
	protected internal abstract string ModName { get; }

	/// <summary>
	/// <see langword="true"/> if this API can be used, <see langword="false"/> otherwise.
	/// </summary>
	internal bool Active => ModLoader.HasMod(ModName) && SupportEnabled();

	// APIs should always be loaded so that TryGetAPI doesn't fail.
	// Usage is determined by Active.
	public override sealed bool IsLoadingEnabled(Mod mod)
	{
		return true;
	}

	public override sealed void Load()
	{
		ModLoader.TryGetMod(ModName, out _wrappedMod);
	}

	protected override sealed void Register()
	{
		ModTypeLookup<ModAPI>.Register(this);
	}

	public override sealed void SetupContent()
	{
		SetStaticDefaults();
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