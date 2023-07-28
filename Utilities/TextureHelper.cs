using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.IO;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace RandomModCompat.Utilities;

// Once again, I have taken the path of most resistance so I can be lazy later.
// Because why should I spend five minutes making Liber's gem relic item textures when I can spend the whole day learning how to create an asset in memory?
internal static class TextureHelper
{
	/// <summary>
	/// Creates an <see cref="Asset{T}"/> from a <see cref="Texture2D"/>. with the specified name.
	/// </summary>
	/// <param name="texture">The texture to create the asset from. Notably, a <see cref="RenderTarget2D"/> can be used here.</param>
	/// <param name="name">The name of the asset.</param>
	/// <param name="mod">The mod to add the asset to.</param>
	/// <returns>The created asset.</returns>
	internal static Asset<Texture2D> CreateAssetFromTexture(Texture2D texture, string name, Mod mod)
	{
		// .rawimg format: FORMAT (1) | Width | Height | Data
		int imageBytes = texture.Width * texture.Height * 4;
		byte[] imageData = new byte[imageBytes];
		texture.GetData(imageData, 0, imageBytes);

		using MemoryStream stream = new();
		using BinaryWriter writer = new(stream);

		writer.Write(ImageIO.VERSION);
		writer.Write(texture.Width);
		writer.Write(texture.Height);
		writer.Write(imageData, 0, imageBytes);

		// stream now has a complete .rawimg file.

		stream.Position = 0;
		return mod.Assets.CreateUntracked<Texture2D>(stream, name + ".rawimg");
	}

	/// <inheritdoc cref="CreateAssetFromTexture(Texture2D, string, Mod)"/>
	internal static Asset<Texture2D> ToAsset(this Texture2D texture, string name, Mod mod) => CreateAssetFromTexture(texture, name, mod);

	/// <inheritdoc cref="CreateAssetFromTexture(Texture2D, string, Mod)"/>
	internal static Asset<Texture2D> ToAsset(this ARenderTargetContentByRequest texture, string name, Mod mod) => CreateAssetFromTexture(texture.GetTarget(), name, mod);
}