using System.IO;

public class P2_05 : Extreme
{
    public P2_05(SkillData skillData) : base(skillData)
    {
    }

    public override int OnChangedSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int damage = resolved[0];
        int multiple = resolved[1];

        var hpDamage = c.Player.Attack(c.Target, damage * multiple);
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