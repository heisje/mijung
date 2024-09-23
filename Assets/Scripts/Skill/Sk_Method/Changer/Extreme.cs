public abstract class Extreme : Skill
{
    public Extreme(SkillData skillData) : base(skillData)
    {

    }

    public override bool OnCheck(DiceCalculateDto diceDto, FieldContext fieldContext)
    {
        IsChanged = OnCheckChange(diceDto, fieldContext);
        return diceDto.GetIsCombi(Combi);
    }

    /// <summary>
    /// 변환이 가능한지 체크
    /// </summary>
    /// <returns></returns>
    public bool OnCheckChange(DiceCalculateDto diceDto, FieldContext fieldContext)
    {
        if (diceDto.IsContainPip(6)) return true;
        return false;
    }
    public abstract int OnDefaultSkill(DiceCalculateDto diceDto, Sk_Context c);
    public abstract int OnChangedSkill(DiceCalculateDto diceDto, Sk_Context c);

    // 스킬 사용 시 효과. 핵심
    public override int OnSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        // Default
        if (IsChanged) return OnChangedSkill(diceDto, c);
        else return OnDefaultSkill(diceDto, c);
    }
}