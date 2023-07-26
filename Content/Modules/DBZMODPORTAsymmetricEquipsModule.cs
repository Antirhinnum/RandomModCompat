using DBZMODPORT.Items.Accessories;
using DBZMODPORT.Items.Accessories.Vanity;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class DBZMODPORTAsymmetricEquipsModule : CrossModModule<AsymmetricEquipsAPI>
{
	public override string CrossModName => ModNames.DBZMODPORT;

	protected internal override void PostSetupContent()
	{
		API.AddHiddenEquip<GreenPotara>(EquipType.Face);
		API.AddHiddenEquip<BattleKit>(EquipType.Face);
		API.AddGlove<BattleKit>();
		API.AddGlove<WornGloves>();

		void AddScouter<T>() where T : ModItem
		{
			API.AddSmallHead<T>();
			API.AddHiddenEquip<T>(EquipType.Face);
		}

		AddScouter<ScouterT2>();
		AddScouter<ScouterT3>();
		AddScouter<ScouterT4>();
		AddScouter<ScouterT5>();
		AddScouter<ScouterT6>();
	}
}