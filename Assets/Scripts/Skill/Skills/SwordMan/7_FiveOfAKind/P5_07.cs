public class P5_07 : Rage
{
    public P5_07(SkillData skillData) : base(skillData)
    {
    }

    public override int OnChangedSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int v0 = resolved[0];
        int v1 = resolved[1];

        return c.Player.Attack(c.Target, v1);
    }

    public override int OnDefaultSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int v0 = resolved[0];
        int v1 = resolved[1];

        return c.Player.Attack(c.Target, v0);
    }
}