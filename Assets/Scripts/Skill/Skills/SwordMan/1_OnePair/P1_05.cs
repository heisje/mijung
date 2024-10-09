public class P1_05 : Rage
{
    public P1_05(SkillData skillData) : base(skillData)
    {
    }

    public override int OnChangedSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int damage = resolved[0];
        int changedDamage = resolved[1];
        int shield = resolved[2];

        var hpDamage = c.Owner.Attack(c.Target, changedDamage);
        c.Owner.Shield += shield;
        return hpDamage;
    }

    public override int OnDefaultSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int damage = resolved[0];

        var hpDamage = c.Owner.Attack(c.Target, damage);
        return hpDamage;
    }
}