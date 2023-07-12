using RandomModCompat.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Terraria.ModLoader.Config;

namespace RandomModCompat.Common;

/// <summary>
/// Creates this mod's main config type.
/// </summary>
internal static class SupportConfigBuilder
{
	private const string _configName = "ModSupportConfig";
	private const string _typeName = "RandomModCompat.Common." + _configName;
	private const MethodAttributes _defaultConstructorAttributes = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.RTSpecialName;
	private const MethodAttributes _overriddenMethodAttributes = MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig;
	private const string _localizationHeader = "$Mods.RandomModCompat.DynamicConfig.";
	private const string _modNamesHeader = _localizationHeader + "ModNames.";
	private static TypeBuilder _current = null;

	/// <summary>
	/// Creates the config.
	/// </summary>
	/// <param name="moduleBuilder">The module that this config is defined in.</param>
	/// <param name="supportedMods">The mods this config should support.</param>
	/// <returns>The type of the new config. This type is a subclass of <see cref="ModConfig"/></returns>
	internal static Type Create(ModuleBuilder moduleBuilder, Dictionary<string, string[]> supportedMods)
	{
		// Also AutoLayout and Ansi, but those are default.
		const TypeAttributes ConfigAttributes = TypeAttributes.Public | TypeAttributes.Sealed;
		Type modConfigType = typeof(ModConfig);
		_current = moduleBuilder.DefineType(_typeName, ConfigAttributes, modConfigType);

		// Create the type.
		AddLabel(_current, _localizationHeader + "Name");
		OverrideMode();
		Dictionary<TypeBuilder, ConstructorBuilder> nestedTypes = CreateNestedTypes(supportedMods);
		FieldBuilder[] fields = AddFieldsToConfig(nestedTypes.Keys, supportedMods.Keys);
		DefineConstructor(fields, nestedTypes.Values);

		// TODO:
		// - Checking for support.

		foreach (TypeBuilder nestedType in nestedTypes.Keys)
		{
			nestedType.CreateType();
		}
		return _current.CreateType();
	}

	/// <summary>
	/// Override <see cref="ModConfig.Mode"/> to always return <see cref="ConfigScope.ServerSide"/>.
	/// </summary>
	private static void OverrideMode()
	{
		const string Name = nameof(ModConfig.Mode);
		const string GetterName = "get_" + Name;
		Type returnType = typeof(ConfigScope);

		MethodBuilder getterBuilder = _current.DefineMethod(GetterName, _overriddenMethodAttributes | MethodAttributes.SpecialName, returnType, Type.EmptyTypes);

		#region get_Mode IL

		ILGenerator gIL = getterBuilder.GetILGenerator();
		gIL.Emit(OpCodes.Ldc_I4, (int)ConfigScope.ServerSide);
		gIL.Emit(OpCodes.Ret);

		#endregion get_Mode IL

		PropertyBuilder modeBuilder = _current.DefineProperty(Name, PropertyAttributes.None, returnType, Type.EmptyTypes);
		modeBuilder.SetGetMethod(getterBuilder);
	}

	/// <summary>
	/// Creates a nested type per base mod.
	/// </summary>
	/// <param name="supportedMods">A map of each base mod to the mods it supports.</param>
	/// <returns>The nested types, plus their constructors.</returns>
	private static Dictionary<TypeBuilder, ConstructorBuilder> CreateNestedTypes(Dictionary<string, string[]> supportedMods)
	{
		Dictionary<TypeBuilder, ConstructorBuilder> builders = new(supportedMods.Count);

		foreach ((string baseMod, string[] supportMods) in supportedMods)
		{
			(TypeBuilder builder, ConstructorBuilder constructor) = CreateNestedType(baseMod, supportMods);
			builders.Add(builder, constructor);
		}

		return builders;
	}

	/// <summary>
	/// Creates a single nested type for a base mod.
	/// </summary>
	/// <param name="baseMod">The mod to create the type for.</param>
	/// <param name="supportMods">The supported mods.</param>
	/// <returns>The created type.</returns>
	private static (TypeBuilder, ConstructorBuilder) CreateNestedType(string baseMod, string[] supportMods)
	{
		string typeName = baseMod + "SupportConfig";
		const TypeAttributes NestedConfigAttributes = TypeAttributes.NestedPublic | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit;
		TypeBuilder builder = _current.DefineNestedType(typeName, NestedConfigAttributes);

		// Attribute
		AddLabel(builder, _modNamesHeader + baseMod);

		// Fields
		Type boolType = typeof(bool);
		List<FieldBuilder> fields = new(supportMods.Length);
		foreach (string supportMod in supportMods)
		{
			FieldBuilder field = builder.DefineField(supportMod, boolType, FieldAttributes.Public);

			AddLabel(field, _modNamesHeader + supportMod);
			AddDefaultTrueValue(field);

			fields.Add(field);
		}

		// Constructor
		ConstructorBuilder con = builder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);

		#region .ctor IL

		ILGenerator cIL = con.GetILGenerator();

		// Default constructor
		cIL.Emit(OpCodes.Ldarg_0);
		cIL.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes));

		// Initialize all fields to true
		foreach (FieldBuilder field in fields)
		{
			cIL.Emit(OpCodes.Ldarg_0);
			cIL.Emit(OpCodes.Ldc_I4_1);
			cIL.Emit(OpCodes.Stfld, field);
		}

		cIL.Emit(OpCodes.Ret);

		#endregion .ctor IL

		// We have to override Object::Equals() because otherwise, it compares by reference and doesn't work properly.
		MethodBuilder equals = builder.DefineMethod(nameof(Equals), _overriddenMethodAttributes, boolType, new Type[] { typeof(object) });

		#region Equals IL

		// Code adapted from compiler output.
		ILGenerator eIL = equals.GetILGenerator();
		int instanceIndex = eIL.DeclareLocal(builder).LocalIndex;
		int returnValueIndex = eIL.DeclareLocal(boolType).LocalIndex;

		Label returnLabel = eIL.DefineLabel();
		Label afterReturnLabel = eIL.DefineLabel();
		Label returnTrueLabel = eIL.DefineLabel();

		// bool::Equals(bool)
		MethodInfo boolEquals = boolType.GetMethod(nameof(Equals), ReflectionHelper.AllFlags, new Type[] { boolType });

		eIL.Emit(OpCodes.Nop);
		eIL.Emit(OpCodes.Ldarg, 1);
		eIL.Emit(OpCodes.Isinst, builder);
		eIL.Emit(OpCodes.Stloc, instanceIndex);
		eIL.Emit(OpCodes.Ldloc, instanceIndex);
		eIL.Emit(OpCodes.Brfalse, returnLabel);

		for (int i = 0; i < fields.Count; i++)
		{
			FieldBuilder field = fields[i];

			eIL.Emit(OpCodes.Ldarg_0);
			eIL.Emit(OpCodes.Ldflda, field);
			eIL.Emit(OpCodes.Ldloc, instanceIndex);
			eIL.Emit(OpCodes.Ldfld, field);
			eIL.Emit(OpCodes.Call, boolEquals);
			if (i != fields.Count - 1)
			{
				eIL.Emit(OpCodes.Brfalse, returnLabel);
			}
			else
			{
				eIL.Emit(OpCodes.Br, afterReturnLabel);
			}
		}

		eIL.MarkLabel(returnLabel);
		eIL.Emit(OpCodes.Ldc_I4_0);
		eIL.MarkLabel(afterReturnLabel);
		eIL.Emit(OpCodes.Stloc, returnValueIndex);
		eIL.Emit(OpCodes.Br, returnTrueLabel);
		eIL.MarkLabel(returnTrueLabel);
		eIL.Emit(OpCodes.Ldloc, returnValueIndex);
		eIL.Emit(OpCodes.Ret);

		#endregion Equals IL

		return (builder, con);
	}

	/// <summary>
	/// Adds all of the provided nested types to the config with the given names.
	/// <br/> The first field will have <see cref="HeaderAttribute"/>. All fields will have <see cref="ReloadRequiredAttribute"/>.
	/// </summary>
	/// <param name="nestedTypes">The types of each field.</param>
	/// <param name="names">The name of each field.</param>
	/// <returns>The added fields.</returns>
	private static FieldBuilder[] AddFieldsToConfig(IEnumerable<TypeBuilder> nestedTypes, IEnumerable<string> names)
	{
		ConstructorInfo reloadRequiredCon = typeof(ReloadRequiredAttribute).GetConstructor(Type.EmptyTypes);
		List<FieldBuilder> fields = new();

		bool firstField = true;
		foreach ((TypeBuilder builder, string name) in nestedTypes.Zip(names))
		{
			FieldBuilder field = _current.DefineField(name, builder, FieldAttributes.Public);
			field.SetCustomAttribute(new(reloadRequiredCon, Array.Empty<object>()));

			if (firstField)
			{
				firstField = false;

				ConstructorInfo headerCon = typeof(HeaderAttribute).GetConstructor(new Type[] { typeof(string) });
				field.SetCustomAttribute(new(headerCon, new object[] { _localizationHeader + "Info" }));
			}

			fields.Add(field);
		}

		return fields.ToArray();
	}

	/// <summary>
	/// Defines the constructor. This constructor initializes all fields to new instances.
	/// </summary>
	/// <param name="fields">The declared fields.</param>
	private static void DefineConstructor(FieldBuilder[] fields, IEnumerable<ConstructorBuilder> constructors)
	{
		ConstructorBuilder builder = _current.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);

		#region .ctor IL

		ILGenerator cIL = builder.GetILGenerator();

		// Call default constructor
		ConstructorInfo con = typeof(ModConfig).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, Type.EmptyTypes);
		cIL.Emit(OpCodes.Ldarg_0);
		cIL.Emit(OpCodes.Call, con!);

		// Assign new instances to all fields.
		foreach ((FieldBuilder field, ConstructorBuilder constructor) in fields.Zip(constructors))
		{
			cIL.Emit(OpCodes.Ldarg_0);
			cIL.Emit(OpCodes.Newobj, constructor);
			cIL.Emit(OpCodes.Stfld, field);
		}

		cIL.Emit(OpCodes.Ret);

		#endregion .ctor IL
	}

	private static readonly ConstructorInfo _labelConstructor = typeof(LabelAttribute).GetConstructor(new Type[] { typeof(string) });
	private static readonly ConstructorInfo _defaultValueConstructor = typeof(DefaultValueAttribute).GetConstructor(new Type[] { typeof(bool) });

	/// <summary>
	/// Adds a <see cref="LabelAttribute"/> to <paramref name="builder"/>.
	/// </summary>
	/// <param name="builder">The type to annotate.</param>
	/// <param name="localizationKey">The key to provide to the label.</param>
	private static void AddLabel(TypeBuilder builder, string localizationKey)
	{
		builder.SetCustomAttribute(new(_labelConstructor, new object[] { localizationKey }));
	}

	// No interface :(
	/// <summary>
	/// Adds a <see cref="LabelAttribute"/> to <paramref name="builder"/>.
	/// </summary>
	/// <param name="builder">The field to annotate.</param>
	/// <param name="localizationKey">The key to provide to the label.</param>
	private static void AddLabel(FieldBuilder builder, string localizationKey)
	{
		builder.SetCustomAttribute(new(_labelConstructor, new object[] { localizationKey }));
	}

	/// <summary>
	/// Adds a <see cref="DefaultValueAttribute"/> to <paramref name="builder"/> with a value of <see langword="true"/>.
	/// </summary>
	/// <param name="builder">The field to annotate.</param>
	private static void AddDefaultTrueValue(FieldBuilder builder)
	{
		builder.SetCustomAttribute(new(_defaultValueConstructor, new object[] { true }));
	}
}