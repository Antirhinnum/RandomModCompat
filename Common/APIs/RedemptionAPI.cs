using RandomModCompat.Core;
using Terraria;
using Terraria.ModLoader;

namespace RandomModCompat.Common.APIs;

[JITWhenModsEnabled(ModNames.Redemption)]
internal sealed partial class RedemptionAPI : ModAPI
{
	protected internal override string ModName => ModNames.Redemption;

	protected override bool SupportEnabled()
	{
		return Main.assemblyVersionNumber.StartsWith("1.4.4");
	}

	#region Default

	internal bool AddElementNPC(Element element, int npcId)
	{
		return (bool)WrappedMod.Call("addElementNPC", (int)element, npcId);
	}

	internal bool AddElementItem(Element element, int itemId)
	{
		return (bool)WrappedMod.Call("addElementItem", (int)element, itemId);
	}

	internal bool AddElementProjectile(Element element, int projectileId)
	{
		return (bool)WrappedMod.Call("addElementProj", (int)element, projectileId);
	}

	internal void AddItemToBluntSwing(int itemId)
	{
		WrappedMod.Call("addItemToBluntSwing", itemId);
	}

	internal void AddNPCToElementTypeList(NPCList list, int npcId)
	{
		WrappedMod.Call("addNPCToElementTypeList", list.ToString(), npcId);
	}

	#endregion Default
}