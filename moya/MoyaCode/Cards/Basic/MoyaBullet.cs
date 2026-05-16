using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using MoeNegiMod.Moya.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoeNegiMod.Moya.Cards;





#pragma warning disable STS001 // Symbol missing localization
public class MoyaBullet() : MoyaCard(cost: 1,
#pragma warning restore STS001 // Symbol missing localization

	CardType.Skill, CardRarity.Common,
	TargetType.Self)
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(8, ValueProp.Move),new DynamicVar("CoinsPower",2m)];

	

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<CoinsPower>()
	];

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await CommonActions.CardBlock(this, cardPlay);
		await PowerCmd.Apply<CoinsPower>(base.Owner.Creature, base.DynamicVars["CoinsPower"].BaseValue, base.Owner.Creature, this);
	}
   
	protected override void OnUpgrade()
	{
		EnergyCost.UpgradeBy(-1);
	}
}
