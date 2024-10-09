
public class P1_12 : Skill
{
    public P1_12(SkillData skillData) : base(skillData)
    {
    }

    public override int OnSkill(Sk_Context c)
    {
        int value = Formulas[0].EvaluateFormula(c.DiceInfo, Combi);
        c.Owner.ShieldUp(value);
        return 0;
    }
}