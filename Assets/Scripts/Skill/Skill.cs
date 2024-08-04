
using System;
using System.Collections.Generic;
using System.Diagnostics;



[Serializable]
public class Skill
{
    // csv에서 받아오는 데이터 --------------------------------------------
    public SkillID ID;
    public string Name;     // 스킬 이름
    public string Ko;       // 한글
    public string DefaultDescription;   // 기본 설명 텍스트
    public CombiType Combi;
    public int Unique;
    public int DefaultCooldown;                 // 기존 쿨타임
    public SkillTargetType SkillTarget;     // 스킬 타겟을 선택하는 종류
    public List<Formula<FormulaType>> Formulas;

    // 추가적인 상태 ------------------------------------------------------

    // public string DamageFormula;    // 상태가 변경된 텍스트
    public string Description;   // 상태가 변경된 텍스트
    public int Cooldown = 0;             // 상태저장 쿨타임
    public bool IsPossible = false;             // 여러 조건으로 인한 스킬 사용가능 여부 판단

    // 조건에 따라 효과가 변경되는 경우
    public ChangerType Changer;
    public string ChangerValue;
    public List<Formula<FormulaType>> ChangerFormulas;


    public Skill(SkillID id)
    {
        ID = id;
        SkillData skillData = SkillReaderManager.Instance.GetSkillData(id);

        if (skillData == null)
        {
            throw new ArgumentException($"Invalid SkillID: {id}");
        }

        Name = skillData.Ko;
        Ko = skillData.Ko;
        DefaultDescription = skillData.Description;
        Combi = skillData.Combi;
        Unique = skillData.Unique;
        DefaultCooldown = skillData.DefaultCooldown;
        SkillTarget = skillData.Target;
        Formulas = skillData.FormulaList;
        Changer = skillData.Changer;
        ChangerValue = skillData.ChangerValue;
        ChangerFormulas = skillData.ChangerFormulaList;
    }
    // 오로지 주사위로 가능한지 여부 체크
    public virtual bool OnCheck(DiceCalculateDto diceDto)
    {
        if (SkillCalManager.Instance.CheckCombi(Combi, diceDto))
        {
            IsPossible = true;
        }
        else
        {
            IsPossible = false;
        }
        return IsPossible;
    }

    // 스킬 사용 시 효과
    public virtual bool OnSkill<T>(DiceCalculateDto diceDto, T target) where T : Character
    {
        return SkillCalManager.Instance.OnDefinedSkill<T>(this, diceDto, target);
    }

    // 현재 상태에 따라 설명 업데이트
    public virtual void UpdateDescription(DiceCalculateDto diceDto, Player p)
    {
        // if (OnCheck(diceDto))
        // {
        //     if (diceDto.PairLargePips.TryGetValue(5, out int largePip))
        //     {
        //         Description = DefaultDescription.Replace("{LargePip}", largePip.ToString());
        //         return;
        //     }
        // }
        Description = DefaultDescription;
    }

    public virtual int CalculateDamage(string formula, int largePip)
    {
        // damageFormula 문자열에서 "{largePip}" 플레이스홀더를 실제 값으로 대체합니다.
        string formulaResult = formula.Replace("{largePip}", largePip.ToString());

        // EvaluateFormula 메서드를 호출하여 대체된 공식을 계산합니다.
        return EvaluateFormula(formulaResult);
    }

    public virtual int EvaluateFormula(string formula)
    {
        // DataTable을 사용하여 수식을 계산합니다.
        var dataTable = new System.Data.DataTable();

        // Compute 메서드를 사용하여 수식의 결과를 계산하고, 정수로 변환하여 반환합니다.
        return Convert.ToInt32(dataTable.Compute(formula, string.Empty));
    }
}
