using System.Linq;

class S3_04 : Skill
{
    public S3_04(SkillData skillData) : base(skillData)
    {
    }

    public override int OnSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int v0 = resolved[0];

        var ec = c.Enemies.Count();

        c.Player.TakeDamage(v0);
        c.Player.UpdateCondition(ECondition.Empowerment, ec);
        return 0;
    }
}