using System.IO;

public class Sample : Changer
{
    public Sample(SkillData skillData) : base(skillData)
    {
    }

    public override bool OnCheckChange(Sk_Context c)
    {
        if (c.DiceInfo.IsContainPip(6)) return true;
        return false;
    }

    public override int OnChangedSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int damage = resolved[0];
        int damageSelf = resolved[1];
        int empower = resolved[2];

        var hpDamage = c.Owner.Attack(c.Target, damage);
        c.Owner.HP -= damageSelf;
        c.Owner.UpdateCondition(ECondition.Empowerment, empower);
        return hpDamage;
    }

    public override int OnDefaultSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int damage = resolved[0];
        int damageSelf = resolved[1];

        var hpDamage = c.Owner.Attack(c.Target, damage);
        c.Owner.HP -= damageSelf;
        return hpDamage;
    }

}