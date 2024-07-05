
using System;

public enum SkillID
{
    OnePair = 0,
    TwoPair = 1,
    ThreeOfAKind = 2,
    FourOfAKind = 3,
    SmaillStraight = 4,
    LargeStraight = 5,
    FiveOfAKind = 6,
}


public abstract class Skill
{
    public SkillID ID;
    public string Name;     // 스킬 이름
    public string DamageFormula;
    public string DefaultDescription;   // 기본 설명 텍스트
    public string Description;   // 상태가 변경된 텍스트
    public SkillTargetType SkillTargetType;     // 스킬 타겟을 선택하는 종류
    public int DefaultCooldown;                 // 기존 쿨타임
    public int Cooldown = 0;             // 상태저장 쿨타임
    public bool IsPossible = false;             // 여러 조건으로 인한 스킬 사용가능 여부 판단

    protected void Initialize(SkillID id)
    {
        ID = id;
        SkillData skillData = SkillReaderManager.Instance.GetSkillData(ID);
        if (skillData != null)
        {
            Name = skillData.Name;
            DamageFormula = skillData.DamageFormula;
            SkillTargetType = skillData.SkillTargetType;
            DefaultCooldown = skillData.DefaultCooldown;
        }
    }
    // 오로지 주사위로 가능한지 여부 체크
    public abstract bool OnCheck(DiceCalculateDto diceDto);

    // 스킬 사용 시 효과
    public abstract bool OnSkill<T>(DiceCalculateDto diceDto, T target) where T : ICharacter;

    // 현재 상태에 따라 설명 업데이트
    public abstract void UpdateDescription(DiceCalculateDto diceDto, Player p);

    public int CalculateDamage(int largePip)
    {
        // damageFormula 문자열에서 "{largePip}" 플레이스홀더를 실제 값으로 대체합니다.
        string formula = DamageFormula.Replace("{largePip}", largePip.ToString());

        // EvaluateFormula 메서드를 호출하여 대체된 공식을 계산합니다.
        return EvaluateFormula(formula);
    }

    public int EvaluateFormula(string formula)
    {
        // DataTable을 사용하여 수식을 계산합니다.
        var dataTable = new System.Data.DataTable();

        // Compute 메서드를 사용하여 수식의 결과를 계산하고, 정수로 변환하여 반환합니다.
        return Convert.ToInt32(dataTable.Compute(formula, string.Empty));
    }
}
