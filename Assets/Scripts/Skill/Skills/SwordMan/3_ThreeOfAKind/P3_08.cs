public class P3_08 : Extreme
{
    public P3_08(SkillData skillData) : base(skillData)
    {
    }

    public override int OnChangedSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int shield = resolved[0];
        int shieldAppend = resolved[1];
        return c.Player.Attack(c.Target, shield + shieldAppend);
    }

    public override int OnDefaultSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int shield = resolved[0];
        int shieldAppend = resolved[1];
        return c.Player.Attack(c.Target, shield);
    }
}