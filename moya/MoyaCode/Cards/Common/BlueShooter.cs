using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoeNegiMod.Moya.Cards;

#pragma warning disable STS001 // Symbol missing localization
public class BlueShooter() : MoyaCard(cost: 1,
#pragma warning restore STS001 // Symbol missing localization

    CardType.Attack, CardRarity.Common,
    TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(4, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {

        
        var owner = this.Owner;
        if (owner == null) return;

        var enemies = CombatState.HittableEnemies;
        if (enemies.Count <= 0) return;

        // 2. 随机选敌人（不用扩展方法，用基础Random）
        var random = new Random();
        Creature randomTarget = enemies[random.Next(enemies.Count)];
        decimal originalDmg = DynamicVars.Damage.BaseValue;
        int extraDmg = (int)Math.Ceiling(originalDmg * 0.5m); // 0.5倍额外伤害
        await CommonActions.CardAttack(this, cardPlay.Target).Execute(choiceContext);
        await CommonActions.CardAttack(this, randomTarget).Execute(choiceContext);


    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(2m);
    }
}