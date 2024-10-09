public class P1_06 : Rage
{
    public P1_06(SkillData skillData) : base(skillData)
    {
    }

    public override int OnChangedSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int damage = resolved[0];

        var attackDamage = 0;
        foreach (var enemy in c.Enemies)
        {
            attackDamage += c.Owner.Attack(enemy, damage);
        }
        return attackDamage;
    }

    public override int OnDefaultSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int damage = resolved[0];

        var hpDamage = c.Owner.Attack(c.Target, damage);
        return hpDamage;
    }
}