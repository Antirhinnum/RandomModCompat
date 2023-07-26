using RandomModCompat.Utilities;
using System;
using Terraria.ModLoader;

namespace RandomModCompat.Core;

/// <summary>
/// A module for cross-mod support.
/// </summary>
public abstract class CrossModModule : ModType, IAddSupport
{
	/// <summary>
	/// The internal name of the mod this module adds support for.
	/// </summary>
	public abstract string CrossModName { get; }

	/// <summary>
	/// The <see cref="ModAPI"/> this module uses.
	/// </summary>
	public abstract ModAPI GeneralAPI { get; }

	/// <summary>
	/// The mod this module adds support for.
	/// </summary>
	protected Mod CrossMod => ModLoader.TryGetMod(CrossModName, out Mod result) ? result : null;

	/// <summary>
	/// If <see langword="true"/>, this module is allowed to add compatibility.
	/// </summary>
	internal bool Enabled => ModLoader.HasMod(CrossModName) && ModLoader.HasMod(GeneralAPI.ModName) && GeneralAPI.Active && RandomModCompat.SupportEnabled(CrossModName, GeneralAPI.ModName);

	string IAddSupport.BaseMod => CrossModName;
	string IAddSupport.SupportMod => GetAPIModName();

	public override sealed bool IsLoadingEnabled(Mod mod)
	{
		return ModLoader.HasMod(CrossModName);
	}

	protected override void ValidateType()
	{
		if (ReflectionHelper.IsCrossModModule(GetType(), out Type apiType))
		{
			ModAPI api = Activator.CreateInstance(apiType) as ModAPI;
			if (CrossModName == api.ModName)
			{
				throw new Exception($"{Name}: CrossModName is the same as the API's ModName! {CrossModName} - {api.ModName}");
			}
		}
	}

	/// <summary>
	/// Assigns <see cref="GeneralAPI"/>. Called before <see cref="OnModLoad"/>.
	/// </summary>
	internal abstract void LoadAPI();

	/// <inheritdoc cref="ModSystem.OnModLoad"/>
	protected internal virtual void OnModLoad()
	{ }

	/// <inheritdoc cref="ModSystem.PostSetupContent"/>
	protected internal virtual void PostSetupContent()
	{ }

	/// <summary>
	/// Allows loading after everything else has been loaded, including localization.<br/>
	/// This runs after <see cref="ModSystem.PostAddRecipes"/>, so some mods may disallow calls at this point.
	/// </summary>
	protected internal virtual void PostSetupEverything()
	{ }

	private string GetAPIModName()
	{
		if (GeneralAPI is not null)
		{
			return GeneralAPI.ModName;
		}

		if (ReflectionHelper.IsCrossModModule(GetType(), out Type apiType))
		{
			return (Activator.CreateInstance(apiType) as ModAPI).ModName;
		}

		throw new InvalidOperationException($"Can't get API mod name from {GetType().Name}!");
	}

	#region Boilerplate

	protected override sealed void Register()
	{
		ModTypeLookup<CrossModModule>.Register(this);
	}

	protected override sealed void InitTemplateInstance()
	{
		base.InitTemplateInstance();
	}

	public override sealed void SetupContent()
	{
		SetStaticDefaults();
	}

	#endregion Boilerplate
}

/// <inheritdoc/>
/// <typeparam name="T">The type of the <see cref="ModAPI"/> to use.</typeparam>
public abstract class CrossModModule<T> : CrossModModule
	where T : ModAPI
{
	public override ModAPI GeneralAPI => API;

	/// <summary>
	/// The typed <see cref="ModAPI"/> this module uses.
	/// </summary>
	protected T API { get; private set; }

	internal override sealed void LoadAPI()
	{
		API = ModContent.GetInstance<T>();
	}
}