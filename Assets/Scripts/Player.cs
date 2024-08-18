using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

public class Player : Character, ILifeCycle
{
    public Dice[] HaveDices;
    public int NOfRoll = 3; // 최대 굴리기 횟수
    public CharacterType CharacterType;
    public List<SkillID> initialSkillCardList;

    // 스킬카드를 저장해두는 곳. 
    public List<Skill> HaveSkillList = new();

    public int[] GetDiceValues()
    {
        return HaveDices.Select(dice => dice.Value).ToArray();
    }

    public int[] GetSelectedDiceValues()
    {
        return HaveDices.Where(dice => dice.State == DiceState.Keeped).Select(dice => dice.Value).ToArray();
    }
    public Dice[] GetSelectedDice()
    {
        return HaveDices.Where(dice => dice.State == DiceState.Keeped).ToArray();
    }

    // 캐릭터를 선택 할 때 캐릭터의 스킬을 초기화한다.
    public void SelectCharacter(int i)
    {
        if (Enum.TryParse(i.ToString(), out CharacterType characterType))
        {
            switch (characterType)
            {
                case CharacterType.Swordsman:
                    CharacterType = CharacterType.Swordsman;
                    break;
                case CharacterType.Spirit:
                    CharacterType = CharacterType.Spirit;
                    break;
                case CharacterType.Gunner:
                    CharacterType = CharacterType.Gunner;
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
        initialSkillCardList.ForEach((skillId) =>
            {
                HaveSkillList.Add(SkillManager.Ins.CreateSkill(skillId));
            });
    }

    public void BeforeStage()
    {
        SetCondition(StateConditionType.MaxFellDown, 3);
    }

    public void StartStage()
    {
    }

    public void StartTurn()
    {
    }

    public void EndTurn()
    {
    }

    public void EndStage()
    {
    }
}