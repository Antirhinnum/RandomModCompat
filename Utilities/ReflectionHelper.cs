using RandomModCompat.Core;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RandomModCompat.Utilities;

internal static class ReflectionHelper
{
	internal const BindingFlags AllFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
	private static readonly Type _moduleType = typeof(CrossModModule);

	/// <summary>
	/// Determines if a <see cref="Type"/> is a subclass of <see cref="CrossModModule{T}"/>.
	/// </summary>
	/// <param name="typeToCheck">The <see cref="Type"/> to check.</param>
	/// <param name="apiType">The type parameter of <see cref="CrossModModule{T}"/>.</param>
	/// <returns><see langword="true"/> if <paramref name="typeToCheck"/> is a non-abstract, non-interface subclass of <see cref="CrossModModule{T}"/>, <see langword="false"/> otherwise.</returns>
	internal static bool IsCrossModModule(Type typeToCheck, [NotNullWhen(true)] out Type apiType)
	{
		if (typeToCheck is null)
		{
			apiType = null;
			return false;
		}

		if (typeToCheck.IsSubclassOf(_moduleType)
			&& !typeToCheck.IsGenericTypeDefinition // Exclude the generic base class CrossModModule<T>
			&& typeToCheck.BaseType.IsConstructedGenericType)
		{
			// Get T from CrossModModule<T>
			apiType = typeToCheck.BaseType.GetGenericArguments()[0];
			return true;
		}

		apiType = null;
		return false;
	}
}