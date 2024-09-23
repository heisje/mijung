public abstract class ShieldUpAdd : Skill
{
    public ShieldUpAdd(SkillData skillData) : base(skillData)
    {

    }

    public override int OnSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        int damage = Formulas[0].EvaluateFormula(diceDto, Combi);
        int value = Formulas[1].EvaluateFormula(diceDto, Combi);
        AddAction(c, value);
        c.Player.ShieldUp(damage);
        return 0;
    }

    public abstract void AddAction(Sk_Context c, int value);
}