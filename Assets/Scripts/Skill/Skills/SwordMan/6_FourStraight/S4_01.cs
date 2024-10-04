public class S4_01 : Skill
{
    public S4_01(SkillData skillData) : base(skillData)
    {
    }

    public override int OnSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int v0 = resolved[0];
        int result = 0;


        if (diceDto.IsContainPip(6))
        {
            result += c.Player.Attack(c.Target, v0);
            result += c.Player.Attack(c.Target, v0);
            result += c.Player.Attack(c.Target, v0);
        }
        else if (diceDto.IsContainPip(5))
        {
            result += c.Player.Attack(c.Target, v0);
            result += c.Player.Attack(c.Target, v0);
        }
        return result;
    }
}