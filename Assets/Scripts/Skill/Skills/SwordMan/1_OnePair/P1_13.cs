
public class P1_13 : Skill
{
    public P1_13(SkillData skillData) : base(skillData)
    {
    }

    public override int OnSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int damage = resolved[0];
        int shield = resolved[1];

        var hpDamage = c.Owner.Attack(c.Target, damage);
        c.Owner.Shield += shield;
        return hpDamage;
    }
}