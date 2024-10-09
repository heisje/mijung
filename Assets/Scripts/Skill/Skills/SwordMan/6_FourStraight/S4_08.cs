public class S4_08 : Skill
{
    public S4_08(SkillData skillData) : base(skillData)
    {
    }

    public override int OnSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int v0 = resolved[0];

        return c.Owner.Attack(c.Target, v0 + c.Owner.GetCondition(ECondition.Shield));
    }
}