using System.IO;

public class P2_06 : Extreme
{
    public P2_06(SkillData skillData) : base(skillData)
    {
    }

    public override int OnChangedSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int damage = resolved[0];
        int empower = resolved[1];

        var hpDamage = c.Owner.Attack(c.Target, damage);
        c.Owner.UpdateCondition(ECondition.Empowerment, empower);
        return hpDamage;
    }

    public override int OnDefaultSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int damage = resolved[0];

        var hpDamage = c.Owner.Attack(c.Target, damage);
        return hpDamage;
    }
}