using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MoeNegiMod.Moya.Powers;

#pragma warning disable STS001 // Symbol missing localization
public sealed class CoinsPower() : MoyaPowers
#pragma warning restore STS001 // Symbol missing localization
{
	public override PowerType Type => PowerType.Buff;

	public override PowerStackType StackType => PowerStackType.Counter;

	public override bool AllowNegative => true;

	public override decimal ModifyDamageAdditive(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
	{
		if (base.Owner != dealer)
		{
			return 0m;
		}

		if (!props.IsPoweredAttack())
		{
			return 0m;
		}

		return base.Amount;
	}
	public override async Task AfterDamageGiven(
	PlayerChoiceContext choiceContext,
	Creature dealer,
	DamageResult result,
	ValueProp props,
	Creature target,
	CardModel cardSource)
	{
		await base.AfterDamageGiven(choiceContext, dealer, result, props, target, cardSource);

		// 触发条件
		if (Amount != 1) return;           // 必须层数=1
		if (dealer != Owner) return;       // 必须是自己造成的伤害
		if (cardSource == null) return;    // 必须是卡牌打出来的
		if (!props.IsPoweredAttack()) return;// 判断攻击牌
		if (!target.IsEnemy) return;       // 目标必须是敌人

		// 获取所有活着的敌人
		var enemies = CombatState.HittableEnemies;
		if (enemies.Count <= 1) return;    // 只有一个敌人就不触发

		
		Creature randomTarget = null;
		int safety = 10;
		do
		{
			randomTarget = enemies[GD.RandRange(0, enemies.Count - 1)];
			safety--;
		} while (randomTarget == target && safety > 0);

		
		if (randomTarget != null && randomTarget != target && !randomTarget.IsDead)
		{
			await CreatureCmd.Damage(
				choiceContext,
				randomTarget,
				result.TotalDamage,
				props,
				dealer
			);
		}

	}

	public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
	{
		if (side == CombatSide.Enemy)
		{
			await PowerCmd.TickDownDuration(this);
		}
	}

}

		
