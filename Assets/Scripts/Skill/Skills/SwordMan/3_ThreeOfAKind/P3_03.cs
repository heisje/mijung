public class P3_03 : Skill
{
    public P3_03(SkillData skillData) : base(skillData)
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
        if (c.Player.GetCondition(ECondition.Empowerment) >= 1) return true;
        return false;
    }

    public int OnChangedSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int shieldValue0 = resolved[0];
        int shieldValue1 = resolved[1];
        c.Player.ShieldUp(shieldValue1);
        return 0;
    }

    public int OnDefaultSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(diceDto);
        int shieldValue0 = resolved[0];
        int shieldValue1 = resolved[1];
        c.Player.ShieldUp(shieldValue0);
        return 0;
    }

    // 스킬 사용 시 효과. 핵심
    public override int OnSkill(DiceCalculateDto diceDto, Sk_Context c)
    {
        if (IsCheckChange(diceDto, c)) return OnChangedSkill(diceDto, c);
        else return OnDefaultSkill(diceDto, c);
    }
}