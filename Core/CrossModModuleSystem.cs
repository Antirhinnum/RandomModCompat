using System.Linq;
using Terraria.ModLoader;

namespace RandomModCompat.Core;

public sealed class CrossModModuleSystem : ModSystem
{
	public override void OnModLoad()
	{
		// If this is kept as an IEnumerable, then trying to add content in OnModLoad() throws an error (modfied collection).
		CrossModModule[] modules = Mod.GetContent<CrossModModule>().ToArray();
		foreach (CrossModModule module in modules)
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