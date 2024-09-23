using System.IO;

public class Sample : Changer
{
    public Sample(SkillData skillData) : base(skillData)
    {
    }

    public override bool OnCheckChange(DiceCalculateDto diceDto, FieldContext fieldContext)
    {
        if (diceDto.IsContainPip(6)) return true;
        return false;
    }

    public override int OnChangedSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int damage = resolved[0];
        int damageSelf = resolved[1];
        int empower = resolved[2];

        var hpDamage = c.Player.Attack(c.Target, damage);
        c.Player.HP -= damageSelf;
        c.Player.UpdateCondition(ECondition.Empowerment, empower);
        return hpDamage;
    }

    public override int OnDefaultSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int damage = resolved[0];
        int damageSelf = resolved[1];

        var hpDamage = c.Player.Attack(c.Target, damage);
        c.Player.HP -= damageSelf;
        return hpDamage;
    }
}