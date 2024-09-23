public abstract class AttackAdd : Skill
{
    public AttackAdd(SkillData skillData) : base(skillData)
    {

    }

    public override int OnSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        int damage = Formulas[0].EvaluateFormula(diceDto, Combi);
        int value = Formulas[1].EvaluateFormula(diceDto, Combi);
        AddAction(c, value);
        return c.Player.Attack(c.Target, damage);
    }

    public abstract void AddAction(Sk_Context c, int value);
}