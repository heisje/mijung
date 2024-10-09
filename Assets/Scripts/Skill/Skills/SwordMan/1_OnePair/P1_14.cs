
public class P1_14 : Skill
{
    public P1_14(SkillData skillData) : base(skillData)
    {
    }

    public override int OnSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int damage = resolved[0];
        int damageSelf = resolved[1];

        var hpDamage = c.Owner.Attack(c.Target, damage);
        c.Owner.HP -= damageSelf;
        return hpDamage;
    }
}