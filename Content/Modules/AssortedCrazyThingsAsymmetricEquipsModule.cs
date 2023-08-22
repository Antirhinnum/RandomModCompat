using AssortedCrazyThings;
using AssortedCrazyThings.Base.DrawLayers;
using AssortedCrazyThings.Items.Accessories.Useful;
using AssortedCrazyThings.Items.Accessories.Vanity;
using RandomModCompat.Common.APIs;
using RandomModCompat.Core;
using Terraria.ModLoader;

namespace RandomModCompat.Content.Modules;

internal sealed class AssortedCrazyThingsAsymmetricEquipsModule : CrossModModule<AsymmetricEquipsAPI>
{
	public override string CrossModName => ModNames.AssortedCrazyThings;

	protected internal override void PostSetupContent()
	{
		if (ModContent.GetInstance<ContentConfig>().Accessories)
		{
			API.AddHiddenEquip<BottledDreams>(EquipType.Waist);

			API.AddBalloon<MassiveBundleOfBalloons>();
			API.AddBalloon<StarInABalloon>();
			API.AddBalloon<StarWispBalloon>();
			API.AddBalloon<WispInABalloon>();

			API.AddBalloon<CrazyBundleOfAssortedBalloons>();
			API.RegisterBalloonDrawLayer<CrazyBundleOfAssortedBalloonsLayer>();
		}

		if (ModContent.GetInstance<ContentConfig>().VanityAccessories)
		{
#if TML_2022_09
			API.AddBalloon<Cobballoon>();
			API.AddBalloon<EyelloonRetinazer>();
			API.AddBalloon<GreenEyelloon>();
			API.AddBalloon<GreenEyelloonFractured>();
			API.AddBalloon<GreenEyelloonMetal>();
			API.AddBalloon<GreenEyelloonMetalFractured>();
			API.AddBalloon<PurpleEyelloon>();
			API.AddBalloon<PurpleEyelloonFractured>();
			API.AddBalloon<PurpleEyelloonMetal>();
			API.AddBalloon<PurpleEyelloonMetalFractured>();
			API.AddBalloon<RedEyelloon>();
			API.AddBalloon<RedEyelloonFractured>();
			API.AddBalloon<RedEyelloonMetal>();
			API.AddBalloon<RedEyelloonMetalFractured>();
			API.AddBalloon<SpazmatismEyelloon>(); 
#else
			API.AddBalloon<SillyBalloonKit>();
#endif
			//TODO: TEST
		}
	}
}