using System;

public class ShieldUp : Skill
{
    public ShieldUp(SkillData skillData) : base(skillData)
    {
    }

    public override int OnSkill(DiceCalculateDto diceDto, Sk_Context context)
    {
        int value = Formulas[0].EvaluateFormula(diceDto, Combi);
        context.Player.ShieldUp(value);
        return 0;
    }
}