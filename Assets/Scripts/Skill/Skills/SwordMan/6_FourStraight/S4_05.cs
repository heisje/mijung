using System.Linq;
public class S4_05 : Skill
{
    public S4_05(SkillData skillData) : base(skillData)
    {
    }

    public override int OnSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int v0 = resolved[0];

        int result = c.Player.Attack(c.Target, v0); ;
        if (c.Target.GetCondition(ECondition.Hurt) > 0)
        {
            result += c.Player.Attack(c.Target, v0); ;
        }
        return result;
    }
}