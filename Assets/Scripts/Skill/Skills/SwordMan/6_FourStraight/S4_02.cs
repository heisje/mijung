public class S4_02 : Rage
{
    public S4_02(SkillData skillData) : base(skillData)
    {
    }

    public override int OnChangedSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int v0 = resolved[0];
        int damaged = 0;
        damaged += c.Player.Attack(c.Target, v0);
        damaged += c.Player.Attack(c.Target, v0);
        return damaged;
    }

    public override int OnDefaultSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int v0 = resolved[0];
        int damaged = 0;
        damaged += c.Player.Attack(c.Target, v0);
        return damaged;
    }
}