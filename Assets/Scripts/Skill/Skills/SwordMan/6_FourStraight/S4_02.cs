public class S4_02 : Rage
{
    public S4_02(SkillData skillData) : base(skillData)
    {
    }

    public override int OnChangedSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int v0 = resolved[0];
        int damaged = 0;
        damaged += c.Owner.Attack(c.Target, v0);
        damaged += c.Owner.Attack(c.Target, v0);
        return damaged;
    }

    public override int OnDefaultSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int v0 = resolved[0];
        int damaged = 0;
        damaged += c.Owner.Attack(c.Target, v0);
        return damaged;
    }
}