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
public sealed class HardInjuryPower() : MoyaPowers
#pragma warning restore STS001 // Symbol missing localization
{
    public override PowerType Type => PowerType.Debuff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterReceiveDamage(DamageInfo info, CombatState combatState)
    {
        // 1. 只处理持有者本人受到的伤害
        if (info.Target != Owner.Creature)
            return;

        // 2. 获取实际伤害值（已减伤后）
        int damageTaken = info.Damage;
        if (damageTaken <= 0)
            return;

        // 3. 计算降低的最大生命值：伤害的1/3，向下取整
        int maxHpLoss = damageTaken / 3;
        if (maxHpLoss <= 0)
            return;

        // 4. 根据能力层数，可叠加降低量（例如2层则降低 2/3 的伤害值）
        //    如果希望无论多少层都固定降低1/3，可以注释掉下面这行
        maxHpLoss *= Amount;

        // 5. 调用游戏指令降低最大生命值
        await combatState.PlayerCmd.ModifyMaxHp(Owner.Creature, -maxHpLoss);

        // 6. 闪光提示能力生效
        await Flash();
    }
}