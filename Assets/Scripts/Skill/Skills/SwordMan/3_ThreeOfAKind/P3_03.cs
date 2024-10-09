public class P3_03 : Skill
{
    public P3_03(SkillData skillData) : base(skillData)
    {

    }

    public override bool OnCheck(Sk_Context c)
    {
        return c.DiceInfo.GetIsCombi(Combi);
    }

    /// <summary>
    /// 변환이 가능한지 체크
    /// </summary>
    /// <returns></returns>
    public bool IsCheckChange(Sk_Context c)
    {
        if (c.Owner.GetCondition(ECondition.Empowerment) >= 1) return true;
        return false;
    }

    public int OnChangedSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int shieldValue0 = resolved[0];
        int shieldValue1 = resolved[1];
        c.Owner.ShieldUp(shieldValue1);
        return 0;
    }

    public int OnDefaultSkill(Sk_Context c)
    {
        var resolved = this.EvaluateFormulas(c.DiceInfo);
        int shieldValue0 = resolved[0];
        int shieldValue1 = resolved[1];
        c.Owner.ShieldUp(shieldValue0);
        return 0;
    }

    // 스킬 사용 시 효과. 핵심
    public override int OnSkill(Sk_Context c)
    {
        if (IsCheckChange(c)) return OnChangedSkill(c);
        else return OnDefaultSkill(c);
    }
}