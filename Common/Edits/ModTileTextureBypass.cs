using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour.HookGen;
using RandomModCompat.Common.Configs;
using System.Reflection;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace RandomModCompat.Common.Edits;

/// <summary>
/// Allows loading an arbitrary <see cref="Texture2D"/> to a <see cref="ModTile"/>.
/// </summary>
internal sealed class ModTileTextureBypass : ModSystem
{
	internal static bool Failed { get; private set; } = false;
	private static readonly MethodInfo _modTileSetupContent = typeof(ModTile).GetMethod(nameof(ModTile.SetupContent));

	public override bool IsLoadingEnabled(Mod mod)
	{
		if (ModContent.GetInstance<RandomModCompatConfig>().DisableIL)
		{
			Failed = true;
			return false;
		}

		return base.IsLoadingEnabled(mod);
	}

	public override void Load()
	{
		HookEndpointManager.Modify(_modTileSetupContent, BypassTextureLoading);
	}

	public override void Unload()
	{
		HookEndpointManager.Unmodify(_modTileSetupContent, BypassTextureLoading);
	}

	private void BypassTextureLoading(ILContext il)
	{
		ILCursor c = new(il);

		// Match (C#):
		//	TextureAssets.Tile[Type] = ModContent.Request<Texture2D>(Texture);
		// Match (IL):
		//	ldsfld class [ReLogic] ReLogic.Content.Asset`1<class [FNA] Microsoft.Xna.Framework.Graphics.Texture2D>[] Terraria.GameContent.TextureAssets::Tile
		//	ldarg.0
		//	call instance uint16 Terraria.ModLoader.ModBlockType::get_Type()
		//	ldarg.0
		//	callvirt instance string Terraria.ModLoader.ModTexturedType::get_Texture()
		//	ldc.i4.2
		//	call class [ReLogic] ReLogic.Content.Asset`1<!!0> Terraria.ModLoader.ModContent::Request<class [FNA] Microsoft.Xna.Framework.Graphics.Texture2D>(string, valuetype[ReLogic] ReLogic.Content.AssetRequestMode)
		//	stelem.ref
		// Replace With (C#):
		//	TextureAssets.Tile[Type] = (this is ILoadOwnTexture loader) ? loader.GetTexture() : ModContent.Request<Texture2D>(Texture);

		// Will eventually point to the stelem.ref.
		ILLabel assignmentLabel = c.DefineLabel();

		if (!c.TryGotoNext(MoveType.After,
			i => i.MatchLdsfld(typeof(TextureAssets), nameof(TextureAssets.Tile)),
			i => i.MatchLdarg(0),
			i => i.MatchCall(typeof(ModBlockType), "get_" + nameof(ModBlockType.Type))
			))
		{
			Mod.Logger.Debug($"{nameof(ModTileTextureBypass)} failed at edit #1.");
			Failed = true;
			return;
		}

		// Current immediately after the Tile array and index get loaded, and immedately before Request is prepared.
		// Will point at the second ldarg.0.
		ILLabel requestBranch = c.DefineLabel();

		c.Emit(OpCodes.Ldarg_0);
		c.EmitDelegate((ModTile tile) => tile is ILoadOwnTexture);
		c.Emit(OpCodes.Brfalse, requestBranch); // Branch to the original code if this ModTile doesn't load its own texture.

		// Load the ModTile and grab the texture.
		c.Emit(OpCodes.Ldarg_0);
		c.EmitDelegate((ModTile tile) => (tile as ILoadOwnTexture).GetTexture());
		// Then go to stelem.ref
		c.Emit(OpCodes.Br, assignmentLabel);
		// Right at the request code, so mark this label.
		c.MarkLabel(requestBranch);

		// assignmentLabel now points to stelem.ref.
		c.GotoNext(MoveType.Before, i => i.MatchStelemRef());
		c.MarkLabel(assignmentLabel);

		// Psuedo-IL:
		//	ldsfld class [ReLogic] ReLogic.Content.Asset`1<class [FNA] Microsoft.Xna.Framework.Graphics.Texture2D>[] Terraria.GameContent.TextureAssets::Tile
		//	ldarg.0
		//	call instance uint16 Terraria.ModLoader.ModBlockType::get_Type()
		//	ldarg.0
		//	call bool (tile => tile is ILoadOwnTexture)
		//	brfalse {requestBranch}
		//	ldarg.0
		//	call Asset<Texture2D> ((tile as ILoadOwnTexture).GetTexture)
		//	br {assignment}
		//	{requestBranch} ldarg.0
		//	callvirt instance string Terraria.ModLoader.ModTexturedType::get_Texture()
		//	ldc.i4.2
		//	call class [ReLogic] ReLogic.Content.Asset`1<!!0> Terraria.ModLoader.ModContent::Request<class [FNA] Microsoft.Xna.Framework.Graphics.Texture2D>(string, valuetype[ReLogic] ReLogic.Content.AssetRequestMode)
		//	{assignment} stelem.ref
	}
}