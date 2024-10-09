public class P4_07 : Rage
{
    public P4_07(SkillData skillData) : base(skillData)
    {
    }

    public override int OnChangedSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int v0 = resolved[0];
        int v1 = resolved[1];
        c.Owner.UpdateCondition(ECondition.Empowerment, v1);
        return c.Owner.Attack(c.Target, v0);
    }

    public override int OnDefaultSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int v0 = resolved[0];
        int v1 = resolved[1];
        return c.Owner.Attack(c.Target, v0);
    }
}