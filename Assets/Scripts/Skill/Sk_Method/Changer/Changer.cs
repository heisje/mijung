public abstract class Changer : Skill
{
    public Changer(SkillData skillData) : base(skillData)
    {

    }

    public override bool OnCheck(DiceCalculateDto diceDto, Sk_Context c)
    {
        IsDisplayChanged = OnCheckChange(diceDto, c);
        return diceDto.GetIsCombi(Combi);
    }

    /// <summary>
    /// 변환이 가능한지 체크
    /// </summary>
    /// <returns></returns>
    public abstract bool OnCheckChange(DiceCalculateDto diceDto, Sk_Context c);
    public abstract int OnDefaultSkill(DiceCalculateDto diceDto, Sk_Context c);
    public abstract int OnChangedSkill(DiceCalculateDto diceDto, Sk_Context c);

    // 스킬 사용 시 효과. 핵심
    public override int OnSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        // Default
        if (OnCheckChange(diceDto, c)) return OnChangedSkill(diceDto, c);
        else return OnDefaultSkill(diceDto, c);
    }
}