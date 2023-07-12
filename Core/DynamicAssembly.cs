using System.Reflection;
using System.Reflection.Emit;

namespace RandomModCompat.Core;

/// <summary>
/// Wraps a simple dynamic assembly.
/// </summary>
public sealed class DynamicAssembly
{
	private readonly string _assemblyName;

	/// <summary>
	/// The <see cref="System.Reflection.Emit.AssemblyBuilder"/> this assembly controls.
	/// </summary>
	public AssemblyBuilder AssemblyBuilder { get; init; }

	/// <summary>
	/// The default <see cref="System.Reflection.Emit.ModuleBuilder"/> that all types should be defined from.
	/// </summary>
	public ModuleBuilder ModuleBuilder { get; init; }

	public DynamicAssembly(string name)
	{
		_assemblyName = name;
		AssemblyName assemblyName = new(_assemblyName);
		AssemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
		ModuleBuilder = AssemblyBuilder.DefineDynamicModule(assemblyName.Name ?? _assemblyName);
	}
}