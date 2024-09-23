public class S4_03 : Skill
{
    public S4_03(SkillData skillData) : base(skillData)
    {
    }

    public override int OnSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int v0 = resolved[0];
        int result = c.Player.Attack(c.Target, v0); ;
        c.Target.UpdateCondition(ECondition.Hurt, c.Player.GetCondition(ECondition.Empowerment));
        return result;
    }
}