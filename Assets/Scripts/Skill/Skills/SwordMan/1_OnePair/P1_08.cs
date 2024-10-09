public class P1_08 : Changer
{
    public P1_08(SkillData skillData) : base(skillData)
    {
    }


    public override bool OnCheckChange(Sk_Context c)
    {
        if (c.Owner.HP <= 1) return true;
        return false;
    }

    public override int OnChangedSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int damage = resolved[0];

        return c.Owner.Attack(c.Target, damage);
    }

    public override int OnDefaultSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int damage = resolved[0];
        int plusDamage = resolved[1];

        return c.Owner.Attack(c.Target, damage + plusDamage);
    }
}