public class S4_01 : Skill
{
    public S4_01(SkillData skillData) : base(skillData)
    {
    }

    public override int OnSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int v0 = resolved[0];
        int result = 0;


        if (c.DiceInfo.IsContainPip(6))
        {
            result += c.Owner.Attack(c.Target, v0);
            result += c.Owner.Attack(c.Target, v0);
            result += c.Owner.Attack(c.Target, v0);
        }
        else if (c.DiceInfo.IsContainPip(5))
        {
            result += c.Owner.Attack(c.Target, v0);
            result += c.Owner.Attack(c.Target, v0);
        }
        return result;
    }
}