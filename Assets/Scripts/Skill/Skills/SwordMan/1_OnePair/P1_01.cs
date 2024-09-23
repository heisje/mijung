public class P1_01 : Extreme
{
    public P1_01(SkillData skillData) : base(skillData)
    {
    }

    public override int OnChangedSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int v0 = resolved[0];
        int v1 = resolved[1];
        int v2 = resolved[2];
        int v3 = resolved[3];

        var result = c.Player.Attack(c.Target, v2);
        c.Player.ShieldUp(v3);
        return result;
    }

    public override int OnDefaultSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int v0 = resolved[0];
        int v1 = resolved[1];
        int v2 = resolved[2];
        int v3 = resolved[3];

        var result = c.Player.Attack(c.Target, v0);
        c.Player.ShieldUp(v1);
        return result;
    }
}