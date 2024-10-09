public class S4_04 : Skill
{
    public S4_04(SkillData skillData) : base(skillData)
    {
    }

    public override int OnSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int v0 = resolved[0];
        int result = c.Owner.Attack(c.Target, v0); ;
        if (c.Target.GetCondition(ECondition.Hurt) > 0)
        {
            result += c.Owner.Attack(c.Target, v0); ;
        }
        return result;
    }
}