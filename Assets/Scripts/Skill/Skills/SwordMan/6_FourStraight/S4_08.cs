public class S4_08 : Skill
{
    public S4_08(SkillData skillData) : base(skillData)
    {
    }

    public override int OnSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int v0 = resolved[0];

        return c.Player.Attack(c.Target, v0 + c.Player.GetCondition(ECondition.Shield));
    }
}