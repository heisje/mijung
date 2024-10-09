using System;

public class ShieldUp : Skill
{
    public ShieldUp(SkillData skillData) : base(skillData)
    {
    }

    public override int OnSkill(Sk_Context c)
    {
        int value = Formulas[0].EvaluateFormula(c.DiceInfo, Combi);
        c.Owner.ShieldUp(value);
        return 0;
    }
}