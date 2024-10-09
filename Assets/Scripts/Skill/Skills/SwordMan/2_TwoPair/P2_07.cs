public class P2_07 : Skill
{
    public P2_07(SkillData skillData) : base(skillData)
    {

    }

    public override int OnSkill(Sk_Context c)
    {
        int damage = Formulas[0].EvaluateFormula(c.DiceInfo, Combi);
        int multiple = Formulas[1].EvaluateFormula(c.DiceInfo, Combi);

        var allDamage = 0;
        for (int i = 0; i < multiple; i++)
        {
            allDamage += c.Owner.Attack(c.Target, damage);
        }
        return allDamage;
    }
}