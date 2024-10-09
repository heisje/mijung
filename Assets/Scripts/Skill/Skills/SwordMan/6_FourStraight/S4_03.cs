public class S4_03 : Skill
{
    public S4_03(SkillData skillData) : base(skillData)
    {
    }

    public override int OnSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int v0 = resolved[0];
        int result = c.Owner.Attack(c.Target, v0); ;
        c.Target.UpdateCondition(ECondition.Hurt, c.Owner.GetCondition(ECondition.Empowerment));
        return result;
    }
}