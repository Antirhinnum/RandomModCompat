using System.Diagnostics.CodeAnalysis;
using Terraria.ModLoader;

namespace RandomModCompat.Core;

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
	protected abstract string ModName { get; }

	internal bool Active => ModLoader.HasMod(ModName);

	public override sealed void Load()
	{
		ModLoader.TryGetMod(ModName, out _calledMod);
	}

	protected override sealed void Register()
	{
		ModTypeLookup<ModWithCalls>.Register(this);
	}

	internal static bool TryGetCaller<T>([NotNullWhen(true)] out T caller) where T : ModWithCalls
	{
		caller = ModContent.GetInstance<T>();
		return caller.Active;
	}
}