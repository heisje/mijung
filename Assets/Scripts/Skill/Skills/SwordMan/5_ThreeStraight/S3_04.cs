using System.Linq;

public class S3_04 : Skill
{
    public S3_04(SkillData skillData) : base(skillData)
    {
    }

    public override int OnSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int v0 = resolved[0];

        var ec = c.Enemies.Count();

        c.Owner.TakeDamage(v0);
        c.Owner.UpdateCondition(ECondition.Empowerment, ec);
        return 0;
    }
}