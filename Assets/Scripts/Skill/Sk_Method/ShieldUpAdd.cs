public abstract class ShieldUpAdd : Skill
{
    public ShieldUpAdd(SkillData skillData) : base(skillData)
    {

    }

    public override int OnSkill(Sk_Context c)
    {
        int damage = Formulas[0].EvaluateFormula(c.DiceInfo, Combi);
        int value = Formulas[1].EvaluateFormula(c.DiceInfo, Combi);
        AddAction(c, value);
        c.Owner.ShieldUp(damage);
        return 0;
    }

    public abstract void AddAction(Sk_Context c, int value);
}