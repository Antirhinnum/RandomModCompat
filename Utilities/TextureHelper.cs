using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
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

	internal static Texture2D GetOverlaidTexture(Texture2D baseTexture, Texture2D textureWithOverlay)
	{
		ArgumentNullException.ThrowIfNull(baseTexture);
		ArgumentNullException.ThrowIfNull(textureWithOverlay);

		if (baseTexture.Bounds != textureWithOverlay.Bounds)
		{
			throw new ArgumentException($"Textures provided had different sizes! Base: {baseTexture.Size()} | With Overlay: {textureWithOverlay.Size()}");
		}

		int pixels = baseTexture.Width * baseTexture.Height;
		Color[] baseColors = new Color[pixels];
		Color[] withOverlayColors = new Color[pixels];
		Span<Color> overlayColors = new Color[pixels];

		baseTexture.GetData(baseColors);
		textureWithOverlay.GetData(withOverlayColors);

		for (int i = 0; i < pixels; i++)
		{
			if (baseColors[i] != withOverlayColors[i])
			{
				overlayColors[i] = withOverlayColors[i];
			}
		}

		Texture2D overlay = new(Main.graphics.GraphicsDevice, baseTexture.Width, baseTexture.Height);
		overlay.SetData(overlayColors.ToArray());
		return overlay;
	}

	internal static Texture2D OverlayTextures(Texture2D baseTexture, Texture2D overlay)
	{
		ArgumentNullException.ThrowIfNull(baseTexture);
		ArgumentNullException.ThrowIfNull(overlay);

		if (baseTexture.Bounds != overlay.Bounds)
		{
			throw new ArgumentException($"Textures provided had different sizes! Base: {baseTexture.Size()} | With Overlay: {overlay.Size()}");
		}

		int pixels = baseTexture.Width * baseTexture.Height;
		Color[] baseColors = new Color[pixels];
		Color[] overlayColors = new Color[pixels];
		Span<Color> overlaidColors = new Color[pixels];

		baseTexture.GetData(baseColors);
		overlay.GetData(overlayColors);

		for (int i = 0; i < pixels; i++)
		{
			overlaidColors[i] = (overlayColors[i] != Color.Transparent) ? overlayColors[i] : baseColors[i];
		}

		Texture2D overlaid = new(Main.graphics.GraphicsDevice, baseTexture.Width, baseTexture.Height);
		overlaid.SetData(overlaidColors.ToArray());
		return overlaid;
	}
}