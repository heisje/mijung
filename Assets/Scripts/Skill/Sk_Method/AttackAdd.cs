public abstract class AttackAdd : Skill
{
    public AttackAdd(SkillData skillData) : base(skillData)
    {

    }

    public override int OnSkill(Sk_Context c)
    {
        int damage = Formulas[0].EvaluateFormula(c.DiceInfo, Combi);
        int value = Formulas[1].EvaluateFormula(c.DiceInfo, Combi);
        AddAction(c, value);
        return c.Owner.Attack(c.Target, damage);
    }

    public abstract void AddAction(Sk_Context c, int value);
}