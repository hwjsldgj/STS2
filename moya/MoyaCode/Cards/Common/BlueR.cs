using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using MoeNegiMod.Moya.Powers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoeNegiMod.Moya.Cards;



#pragma warning disable STS001 // Symbol missing localization
public class BlueR() : MoyaCard(cost: 1,
#pragma warning restore STS001 // Symbol missing localization

	CardType.Attack, CardRarity.Common,
	TargetType.AnyEnemy)
{
	public override IEnumerable<CardKeyword> CanonicalKeywords => 
	[
		CardKeyword.Retain
	];
	protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(2, ValueProp.Move)];
	
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)

	{
	  
			await CommonActions.CardAttack(this, cardPlay.Target).Execute(choiceContext);
			await CommonActions.CardAttack(this, cardPlay.Target).Execute(choiceContext);
			await CommonActions.CardAttack(this, cardPlay.Target).Execute(choiceContext);
	}

	
	protected override void OnUpgrade()
	{
		DynamicVars.Damage.UpgradeValueBy(1m);
	}
}
