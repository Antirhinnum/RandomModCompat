using CalamityMod.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RandomModCompat.Utilities;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

internal sealed partial class CatalystModAPI
{
	/// <summary>
	/// A general imitation of "Catalyst Mod"'s flashlight lenses.
	/// </summary>
	[JITWhenModsEnabled(ModNames.CatalystMod)]
	[Autoload(false)]
	private sealed class GenericFlashlightLens : ModItem
	{
		private const string _name = "Mods.RandomModCompat.CatalystMod.FlashlightLensName";
		private readonly int _gemItemId;
		private readonly string _gemInternalName;
		private readonly Color _color;

		public GenericFlashlightLens(int gemItemId, string gemInternalName, Color color)
		{
			_gemItemId = gemItemId;
			_gemInternalName = gemInternalName;
			_color = color;
		}

		public override string Name => $"{_gemInternalName}FlashlightLens";
		protected override bool CloneNewInstances => true;

#if !TML_2022_09
		public override LocalizedText DisplayName => Language.GetText(_name).WithFormatArgs(Lang.GetItemName(_gemItemId));
		public override LocalizedText Tooltip => Language.GetText("Mods.CatalystMod.Tooltips.LensTooltip");
#else
		public override void Unload()
		{
			LanguageManager.Instance.OnLanguageChanged -= UpdateItemName;
		}
#endif

		public override void AutoStaticDefaults()
		{
			if (Main.dedServ)
			{
				return;
			}

			Main.RunOnMainThread(() =>
			{
				FlashlightLensItemTextureCreator instance = FlashlightLensItemTextureCreator.Instance;
				instance.UseColor(_color);
				instance.Request();
				instance.PrepareRenderTarget(Main.instance.GraphicsDevice, Main.spriteBatch);
				TextureAssets.Item[Type] = instance.ToAsset($"RandomModCompat/Assets/CatalystMod/{_gemInternalName}FlashlightLens", RandomModCompat.Instance);
			});
		}

#if TML_2022_09
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault(_name);
			Tooltip.SetDefault("{$Mods.CatalystMod.Tooltips.LensTooltip}");
			SacrificeTotal = 1; 
			LanguageManager.Instance.OnLanguageChanged += UpdateItemName;
		}
#endif

		public override void SetDefaults()
		{
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 1;
			Item.consumable = false;
			Item.knockBack = 1.5f;
			Item.value = CalamityGlobalItem.Rarity1BuyPrice;
			Item.rare = ItemRarityID.Blue;

#if TML_2022_09
			UpdateItemName(LanguageManager.Instance); 
#endif
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(_gemItemId, 6)
				.AddTile(TileID.Anvils)
				.Register();
		}

#if TML_2022_09
		private void UpdateItemName(LanguageManager languageManager)
		{
			Item.SetNameOverride(languageManager.GetTextValue(_name, Lang.GetItemNameValue(_gemItemId)));
		} 
#endif
	}

	/// <summary>
	/// Creates textures for Flashlight Lens items from Catalyst Mod.
	/// </summary>
	[Autoload(Side = ModSide.Client)]
	internal sealed class FlashlightLensItemTextureCreator : ARenderTargetContentByRequest, ILoadable
	{
		internal static FlashlightLensItemTextureCreator Instance;

		private const string BaseAssetPath = "RandomModCompat/Assets/CatalystMod/FlashlightLens";
		private static Asset<Texture2D> _baseAsset;
		private Color _color;

		void ILoadable.Load(Mod mod)
		{
			Instance = new();
			Main.ContentThatNeedsRenderTargets.Add(Instance);
			_baseAsset = ModContent.Request<Texture2D>(BaseAssetPath);
		}

		void ILoadable.Unload()
		{
			Main.ContentThatNeedsRenderTargets.Remove(Instance);
			Instance = null;
			_baseAsset = null;
		}

		internal void UseColor(Color color)
		{
			_color = color;
		}

		protected override void HandleUseReqest(GraphicsDevice device, SpriteBatch spriteBatch)
		{
			PrepareARenderTarget_AndListenToEvents(ref _target, device, _baseAsset.Width(), _baseAsset.Height(), RenderTargetUsage.PreserveContents);
			device.SetRenderTarget(_target);
			device.Clear(Color.Transparent);
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, Main.DefaultSamplerState, DepthStencilState.Default, Main.Rasterizer);

			spriteBatch.Draw(_baseAsset.Value, Vector2.Zero, _color);

			spriteBatch.End();
			device.SetRenderTarget(null);
			_wasPrepared = true;
		}
	}
}