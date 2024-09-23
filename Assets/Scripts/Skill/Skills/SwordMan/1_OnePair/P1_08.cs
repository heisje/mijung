public class P1_08 : Changer
{
    public P1_08(SkillData skillData) : base(skillData)
    {
    }


    public override bool OnCheckChange(DiceCalculateDto diceDto, FieldContext fieldContext)
    {
        if (fieldContext.Player.HP <= 1) return true;
        return false;
    }

    public override int OnChangedSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int damage = resolved[0];

        return c.Player.Attack(c.Target, damage);
    }

    public override int OnDefaultSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int damage = resolved[0];
        int plusDamage = resolved[1];

        return c.Player.Attack(c.Target, damage + plusDamage);
    }
}