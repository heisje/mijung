using System;
using System.Collections.Generic;

public static class CharacterActions
{
    // Attack,ShieldUp,Heal,HurtBurst,Extreme
    // Attack
    public static int Attack(this Character Owner, Character target, int damage)
    {
        return target.TakeDamage(damage + Owner.GetCondition(ECondition.Empowerment));
    }


    // ShieldUp
    public static int ShieldUp(this Character Owner, int value)
    {
        Owner.Shield = Math.Max(Owner.Shield + value, 0);
        return 0;
    }

    // Heal
    public static int Heal(this Character Owner, int value)
    {
        Owner.Shield = Math.Max(Owner.Shield + value, 0);
        return 0;
    }

    public static int BoomHurt(this Character owner, Character target, DiceInfo diceInfo, int hurtDamage = GLOBAL_CONST.HURT_DAMAGE)
    {
        var allDamaged = 0;
        if (diceInfo.IsContainPip(GLOBAL_CONST.HURT_PIP))
        {
            var count = target.GetCondition(ECondition.Hurt);

            for (var i = 0; i < count; i++)
            {
                allDamaged += owner.Attack(target, hurtDamage);
            }
            target.SetCondition(ECondition.Hurt, 0);
        }
        return allDamaged;
    }

    public static int HurtBurst(this Character owner, Character[] targets, DiceInfo diceInfo, int hurtDamage = GLOBAL_CONST.HURT_DAMAGE)
    {
        var allDamaged = 0;
        foreach (var target in targets)
        {
            var count = target.GetCondition(ECondition.Hurt);

            for (var i = 0; i < count; i++)
            {
                allDamaged += owner.Attack(target, hurtDamage);
            }
            target.SetCondition(ECondition.Hurt, 0);
        }
        return allDamaged;
    }
}