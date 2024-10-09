public class P3_04 : Rage
{
    public P3_04(SkillData skillData) : base(skillData)
    {
    }

    public override int OnChangedSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int attack1 = resolved[0];
        int attack2 = resolved[1];
        return c.Owner.Attack(c.Target, attack2);
    }

    public override int OnDefaultSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int attack1 = resolved[0];
        int attack2 = resolved[1];
        return c.Owner.Attack(c.Target, attack1);
    }
}