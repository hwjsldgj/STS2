using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MoeNegiMod.Moya.Character;
using MoeNegiMod.Moya.Extensions;
using System;
using System.Collections.Generic;

namespace MoeNegiMod.Moya.Cards;

[Pool(typeof(MoyaCardPool))]
public abstract class MoyaCard(int cost, CardType type, CardRarity rarity, TargetType target) :
	CustomCardModel(cost, type, rarity, target)
{
	private bool PreviewDegenerate;

	//Image size:
	//Normal art: 1000x760 (Using 500x380 should also work, it will simply be scaled.)
	//Full art: 606x852
	//public override string CustomPortraitPath
	//{
	//    get
	//    {
	//       var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
	//      Log.Info(">>>[MoyaMod]CardPath=" + path, 2);
	//      return ResourceLoader.Exists(path) ? path : "card.png".BigCardImagePath();
	//}
	//}

	//Smaller variants of card images for efficiency:
	//Smaller variant of fullart: 250x350
	//Smaller variant of normalart: 250x190

	//Uses card_portraits/card_name.png as image path. These should be smaller images.
	public override string PortraitPath
	{
		get
		{
			var degenerateType = PreviewDegenerate ? "_degenerate" : "";
			var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant() + degenerateType}.png".CardImagePath();
			Log.Info(">>>[MoeNegiMod]CardPath=" + path, 2);
			return ResourceLoader.Exists(path) ? path : $"card{degenerateType}.png".CardImagePath();
		}
	}
}
 
