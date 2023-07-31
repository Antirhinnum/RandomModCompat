using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace RandomModCompat.Common;

/// <summary>
/// Denotes that a <see cref="ModType"/> loads its own texture into <see cref="TextureAssets"/>.
/// <br/> Currently supports: <see cref="ModTile"/>
/// </summary>
internal interface ILoadOwnTexture
{
	/// <summary>
	/// Retrieves the texture to load.
	/// </summary>
	Asset<Texture2D> GetTexture();
}