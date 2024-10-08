using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Character
{
    public Dice[] HaveDices;
    public int NOfRoll = 3; // 최대 굴리기 횟수
    public ECharacter CharacterType;
    public Sk_StartPack SkillStartPack;

    // 스킬카드를 저장해두는 곳. 
    [SerializeField]
    public List<Skill> HaveSkillList;

    public int[] GetDiceValues()
    {
        return HaveDices.Select(dice => dice.Value).ToArray();
    }

    public int[] GetSelectedDiceValues()
    {
        return HaveDices.Where(dice => dice.State == DiceState.Keeped).Select(dice => dice.Value).ToArray();
    }

    public int[] GetAllDiceValues()
    {
        return HaveDices.Select(dice => dice.Value).ToArray();
    }
    public Dice[] GetSelectedDice()
    {
        return HaveDices.Where(dice => dice.State == DiceState.Keeped).ToArray();
    }

    // 캐릭터를 선택 할 때 캐릭터의 스킬을 초기화한다.
    public void SelectCharacter(int i)
    {
        if (Enum.TryParse(i.ToString(), out ECharacter characterType))
        {
            switch (characterType)
            {
                case ECharacter.SwordMan:
                    CharacterType = ECharacter.SwordMan;
                    break;
                case ECharacter.Spirit:
                    CharacterType = ECharacter.Spirit;
                    break;
                case ECharacter.Gunner:
                    CharacterType = ECharacter.Gunner;
                    break;
                default:
                    break;
            }
            SkillInit();
            HP = 150;
        }
        else
        {
            Debug.LogError("캐릭터 선택 실패");
        }
    }
    // 스킬 초기화
    public void SkillInit()
    {
        HaveSkillList = new();
        SkillStartPack.IDs.ForEach((skillId) =>
            {
                HaveSkillList.Add(SkillManager.Ins.CreateSkill(skillId));
            });
    }

    public override void BeforeStage()
    {
        SetCondition(ECondition.MaxFellDown, 3);
    }

    public List<Skill> GetHaveSkillList()
    {
        return HaveSkillList;
    }
}