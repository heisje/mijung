using System.IO;

public class P1_03 : Rage
{
    public P1_03(SkillData skillData) : base(skillData)
    {
    }

    public override int OnChangedSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int damage = resolved[0];
        int damageSelf = resolved[1];
        int empower = resolved[2];

        var hpDamage = c.Player.Attack(c.Target, damage);
        c.Player.TakeDamage(damageSelf);
        c.Player.UpdateCondition(ECondition.Empowerment, empower);
        return hpDamage;
    }

    public override int OnDefaultSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int damage = resolved[0];
        int damageSelf = resolved[1];

        var hpDamage = c.Player.Attack(c.Target, damage);
        c.Player.TakeDamage(damageSelf);
        return hpDamage;
    }
}