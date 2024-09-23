public class P2_07 : Skill
{
    public P2_07(SkillData skillData) : base(skillData)
    {

    }

    public override int OnSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        int damage = Formulas[0].EvaluateFormula(diceDto, Combi);
        int multiple = Formulas[1].EvaluateFormula(diceDto, Combi);

        var allDamage = 0;
        for (int i = 0; i < multiple; i++)
        {
            allDamage += c.Player.Attack(c.Target, damage);
        }
        return allDamage;
    }
}