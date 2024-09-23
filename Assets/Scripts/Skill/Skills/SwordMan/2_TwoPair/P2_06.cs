using System.IO;

public class P2_06 : Extreme
{
    public P2_06(SkillData skillData) : base(skillData)
    {
    }

    public override int OnChangedSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int damage = resolved[0];
        int empower = resolved[1];

        var hpDamage = c.Player.Attack(c.Target, damage);
        c.Player.UpdateCondition(ECondition.Empowerment, empower);
        return hpDamage;
    }

    public override int OnDefaultSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int damage = resolved[0];

        var hpDamage = c.Player.Attack(c.Target, damage);
        return hpDamage;
    }
}