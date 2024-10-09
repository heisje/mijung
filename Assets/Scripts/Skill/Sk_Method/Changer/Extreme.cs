public abstract class Extreme : Skill
{
    public Extreme(SkillData skillData) : base(skillData)
    {

    }

    public override bool OnCheck(Sk_Context c)
    {
        IsDisplayChanged = OnCheckChange(c);
        return c.DiceInfo.GetIsCombi(Combi);
    }

    /// <summary>
    /// 변환이 가능한지 체크
    /// </summary>
    /// <returns></returns>
    public bool OnCheckChange(Sk_Context c)
    {
        if (c.DiceInfo.IsContainPip(6)) return true;
        return false;
    }
    public abstract int OnDefaultSkill(Sk_Context c);
    public abstract int OnChangedSkill(Sk_Context c);

    // 스킬 사용 시 효과. 핵심
    public override int OnSkill(Sk_Context c)
    {
        // Default
        if (OnCheckChange(c)) return OnChangedSkill(c);
        else return OnDefaultSkill(c);
    }
}