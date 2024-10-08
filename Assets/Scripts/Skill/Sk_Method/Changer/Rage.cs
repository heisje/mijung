public abstract class Rage : Skill
{
    public Rage(SkillData skillData) : base(skillData)
    {

    }

    public override bool OnCheck(DiceCalculateDto diceDto, Sk_Context c)
    {
        return diceDto.GetIsCombi(Combi);
    }

    /// <summary>
    /// 변환이 가능한지 체크
    /// </summary>
    /// <returns></returns>
    public virtual bool IsCheckChange(DiceCalculateDto diceDto, Sk_Context c)
    {
        var TurnDamaged = c.Player.GetCondition(ECondition.TakeHP) + c.Player.GetCondition(ECondition.TakeShield);
        if (TurnDamaged >= GLOBAL_CONST.RAGE_DAMAGE) return true;
        return false;
    }
    public abstract int OnDefaultSkill(DiceCalculateDto diceDto, Sk_Context c);
    public abstract int OnChangedSkill(DiceCalculateDto diceDto, Sk_Context c);

    // 스킬 사용 시 효과. 핵심
    public override int OnSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        if (IsCheckChange(diceDto, c)) return OnChangedSkill(diceDto, c);
        else return OnDefaultSkill(diceDto, c);
    }
}