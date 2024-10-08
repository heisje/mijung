
using System;
using System.Collections.Generic;
using System.Diagnostics;

[Serializable]
public abstract class Skill : ISkill
{
    // csv에서 받아오는 데이터 --------------------------------------------

    public E_Sk_Id ID; // 1
    public string Ko;   // 2
    public string En;   // 3
    public string Ko_Description;   // 4
    public string En_Description;   // 5
    public ECharacter Group;        // 6
    public ECombi Combi;            // 7
    public int Unique;              // 8
    public int Cooldown;            // 9
    public E_Sk_Focus MouseFocus;   // 10
    public E_Sk_Method Method;      // 11
    public List<string> Formulas;   // 12

    // -----------------------------------------------------------------
    public string Name;                 // 스킬 이름
    public string DefaultDescription;   // 기본 설명 텍스트
    public int DefaultCooldown;         // 기존 쿨타임
    public E_Sk_Focus Target;      // 스킬 타겟을 선택하는 종류
    // 추가적인 상태 ------------------------------------------------------
    public string Description;          // 상태가 변경된 텍스트
    public int CurrentCooldown = 0;            // 상태저장 쿨타임
    public bool IsPossible = false;     // 여러 조건으로 인한 스킬 사용가능 여부 판단
    public bool IsGlobalPossible = false;     // 여러 조건으로 인한 스킬 사용가능 여부 판단
    public bool IsDisplayChanged = false;      // 발동 이벤트
    // --------------------------

    public Skill(SkillData skillData)
    {
        // 생성할때 원본을 복사해 가짐. 이유 = 스킬의 원본을 회손하지 않기 위해
        // TODO: JSON
        ID = skillData.ID; // 1
        Ko = skillData.Ko;   // 2
        En = skillData.En;   // 3
        Ko_Description = skillData.Ko_Description;   // 4
        En_Description = skillData.En_Description;   // 5
        Group = skillData.Group;        // 6
        Combi = skillData.Combi;            // 7
        Unique = skillData.Unique;              // 8
        Cooldown = skillData.Cooldown;            // 9
        MouseFocus = skillData.MouseFocus;   // 10
        Formulas = skillData.Formulas;   // 11

        Name = Ko;
        Description = Ko_Description;
    }

    /// <summary>
    /// 스킬 사용이 가능한지 체크 
    /// </summary>
    /// <param name="diceDto"></param>
    /// <param name="c">주의: 타겟은 null일 수 있음</param>
    /// <returns></returns>
    public virtual bool OnCheck(DiceCalculateDto diceDto, Sk_Context c)
    {
        return diceDto.GetIsCombi(Combi);
    }

    public void OnGlobalCheck(DiceCalculateDto diceDto)
    {
        if (diceDto.GetIsCombi(Combi))
        {
            this.IsGlobalPossible = true;
        }
        else
        {
            this.IsGlobalPossible = false;
        }
    }

    // 스킬 사용 시 효과. 핵심
    public abstract int OnSkill(DiceCalculateDto diceDto, Sk_Context c);

    // 현재 상태에 따라 설명 업데이트
    public virtual void UpdateDescription(DiceCalculateDto diceDto, Player p)
    {
        Description = Ko_Description;
    }
}
