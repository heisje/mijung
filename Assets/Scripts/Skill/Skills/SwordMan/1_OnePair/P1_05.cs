public class P1_05 : Rage
{
    public P1_05(SkillData skillData) : base(skillData)
    {
    }

    public override int OnChangedSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int damage = resolved[0];
        int changedDamage = resolved[1];
        int shield = resolved[2];

        var hpDamage = c.Player.Attack(c.Target, changedDamage);
        c.Player.Shield += shield;
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