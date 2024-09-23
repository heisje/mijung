
public class P1_14 : Skill
{
    public P1_14(SkillData skillData) : base(skillData)
    {
    }

    public override int OnSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int damage = resolved[0];
        int damageSelf = resolved[1];

        var hpDamage = c.Player.Attack(c.Target, damage);
        c.Player.HP -= damageSelf;
        return hpDamage;
    }
}