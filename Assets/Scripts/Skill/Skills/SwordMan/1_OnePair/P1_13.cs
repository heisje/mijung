
public class P1_13 : Skill
{
    public P1_13(SkillData skillData) : base(skillData)
    {
    }

    public override int OnSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int damage = resolved[0];
        int shield = resolved[1];

        var hpDamage = c.Player.Attack(c.Target, damage);
        c.Player.Shield += shield;
        return hpDamage;
    }
}