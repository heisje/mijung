public abstract class Rage : Skill
{
    public Rage(SkillData skillData) : base(skillData)
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
        var TurnDamaged = c.Owner.GetCondition(ECondition.TakeHP) + c.Owner.GetCondition(ECondition.TakeShield);
        if (TurnDamaged >= GLOBAL_CONST.RAGE_DAMAGE) return true;
        return false;
    }
    public abstract int OnDefaultSkill(Sk_Context c);
    public abstract int OnChangedSkill(Sk_Context c);

    // 스킬 사용 시 효과. 핵심
    public override int OnSkill(Sk_Context c)
    {
        if (IsCheckChange(c)) return OnChangedSkill(c);
        else return OnDefaultSkill(c);
    }
}