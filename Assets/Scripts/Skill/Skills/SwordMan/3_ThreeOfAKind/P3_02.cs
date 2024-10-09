public class P3_02 : Extreme
{
    public P3_02(SkillData skillData) : base(skillData)
    {
    }

    public override int OnChangedSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int shieldValue0 = resolved[0];
        int shieldValue1 = resolved[1];
        c.Owner.ShieldUp(shieldValue1);
        return 0;
    }

    public override int OnDefaultSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int shieldValue0 = resolved[0];
        int shieldValue1 = resolved[1];
        c.Owner.ShieldUp(shieldValue0);
        return 0;
    }
}