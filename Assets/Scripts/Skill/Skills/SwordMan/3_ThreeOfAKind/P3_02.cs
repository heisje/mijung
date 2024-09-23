public class P3_02 : Extreme
{
    public P3_02(SkillData skillData) : base(skillData)
    {
    }

    public override int OnChangedSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int shieldValue0 = resolved[0];
        int shieldValue1 = resolved[1];
        c.Player.ShieldUp(shieldValue1);
        return 0;
    }

    public override int OnDefaultSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int shieldValue0 = resolved[0];
        int shieldValue1 = resolved[1];
        c.Player.ShieldUp(shieldValue0);
        return 0;
    }
}