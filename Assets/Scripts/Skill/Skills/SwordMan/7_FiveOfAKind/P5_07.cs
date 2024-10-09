public class P5_07 : Rage
{
    public P5_07(SkillData skillData) : base(skillData)
    {
    }

    public override int OnChangedSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int v0 = resolved[0];
        int v1 = resolved[1];

        return c.Owner.Attack(c.Target, v1);
    }

    public override int OnDefaultSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int v0 = resolved[0];
        int v1 = resolved[1];

        return c.Owner.Attack(c.Target, v0);
    }
}