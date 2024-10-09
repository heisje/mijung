public class P4_04 : Skill
{
    public P4_04(SkillData skillData) : base(skillData)
    {
    }

    public override int OnSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int v0 = resolved[0];
        int v1 = resolved[1];

        c.Enemies.ForEach((e) => e.UpdateCondition(ECondition.Hurt, v0));
        c.Owner.ShieldUp(v1);
        return 0;
    }
}