public class Attack : Skill
{
    public Attack(SkillData skillData) : base(skillData)
    {

    }

    public override int OnSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        int damage = Formulas[0].EvaluateFormula(diceDto, Combi);
        return c.Player.Attack(c.Target, damage);
    }
}