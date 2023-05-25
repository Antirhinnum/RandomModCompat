using System.Reflection;

namespace RandomModCompat.Utilities;

internal static class ReflectionHelper
{
	internal const BindingFlags AllFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
}