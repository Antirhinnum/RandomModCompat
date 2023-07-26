namespace RandomModCompat.Core;

/// <summary>
/// Denotes that a type adds support between two mods.
/// </summary>
internal interface IAddSupport
{
	string BaseMod { get; }
	string SupportMod { get; }
}