public class Attack : Skill
{
    public Attack(SkillData skillData) : base(skillData)
    {

    }

    public override int OnSkill(Sk_Context c)
    {
        int damage = Formulas[0].EvaluateFormula(c.DiceInfo, Combi);
        return c.Owner.Attack(c.Target, damage);
    }
}