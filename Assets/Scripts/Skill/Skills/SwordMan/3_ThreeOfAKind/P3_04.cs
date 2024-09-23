public class P3_04 : Rage
{
    public P3_04(SkillData skillData) : base(skillData)
    {
    }

    public override int OnChangedSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int attack1 = resolved[0];
        int attack2 = resolved[1];
        return c.Player.Attack(c.Target, attack2);
    }

    public override int OnDefaultSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int attack1 = resolved[0];
        int attack2 = resolved[1];
        return c.Player.Attack(c.Target, attack1);
    }
}