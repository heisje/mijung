public class P3_08 : Extreme
{
    public P3_08(SkillData skillData) : base(skillData)
    {
    }

    public override int OnChangedSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int shield = resolved[0];
        int shieldAppend = resolved[1];
        return c.Owner.Attack(c.Target, shield + shieldAppend);
    }

    public override int OnDefaultSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int shield = resolved[0];
        int shieldAppend = resolved[1];
        return c.Owner.Attack(c.Target, shield);
    }
}