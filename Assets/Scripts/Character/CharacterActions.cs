using System;

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

    public static int BoomHurt(this Character owner, Character target, DiceInfo diceInfo)
    {
        var allDamaged = 0;
        if (diceInfo.IsContainPip(GLOBAL_CONST.HURT_PIP))
        {
            var count = target.GetCondition(ECondition.Hurt);

            for (var i = 0; i < count; i++)
            {
                allDamaged += owner.Attack(target, GLOBAL_CONST.HURT_DAMAGE);
            }
            target.SetCondition(ECondition.Hurt, 0);
        }
        return allDamaged;
    }


}