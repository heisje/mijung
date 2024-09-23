public class P2_04 : Rage
{
    public P2_04(SkillData skillData) : base(skillData)
    {
    }

    public override int OnChangedSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int damage = resolved[0];
        int hurtValue = resolved[1];

        var hpDamage = c.Player.Attack(c.Target, damage);
        c.Enemies.ForEach((enemy) => enemy.UpdateCondition(ECondition.Hurt, hurtValue));
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