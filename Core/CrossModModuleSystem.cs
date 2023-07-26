using Terraria.ModLoader;

namespace RandomModCompat.Core;

public sealed class CrossModModuleSystem : ModSystem
{
	public override void OnModLoad()
	{
		foreach (CrossModModule module in Mod.GetContent<CrossModModule>())
		{
			module.LoadAPI();

			if (module.Enabled)
			{
				module.OnModLoad();
			}
		}
	}

	public override void PostSetupContent()
	{
		foreach (CrossModModule module in Mod.GetContent<CrossModModule>())
		{
			if (module.Enabled)
			{
				module.PostSetupContent();
			}
		}
	}

	public override void PostSetupRecipes()
	{
		foreach (CrossModModule module in Mod.GetContent<CrossModModule>())
		{
			if (module.Enabled)
			{
				module.PostSetupEverything();
			}
		}
	}
}