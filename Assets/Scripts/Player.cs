using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

public class Player : Character
{
    public Dice[] HaveDices;
    public int NOfRoll = 3; // 최대 굴리기 횟수
    public CharacterType CharacterType;
    public List<SkillID> initialSkillCardList = new() {
        SkillID.OnePair,
        SkillID.TwoPair,
        SkillID.ThreeOfAKind,
        SkillID.FourOfAKind,
        SkillID.SmallStraight,
        SkillID.LargeStraight,
        SkillID.FiveOfAKind
    };

    // 스킬카드를 저장해두는 곳. 
    public List<Skill> HaveSkillList = new();

    // Hp 변환용
    public ChangeTMP ChangeTMP { get; set; }

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
            Health = 100;

            initialSkillCardList.ForEach((skillId) =>
            {
                HaveSkillList.Add(SkillManager.Instance.CreateSkill(skillId));
            });
        }
        else
        {
            Debug.LogError("캐릭터 선택 실패");
        }
    }

    public override void TakeDamage(int damage)
    {
        Health -= damage;
        if (ChangeTMP == null)
        {
            ChangeTMP = GetComponentInChildren<ChangeTMP>();
        }
        ChangeTMP.ChangeText(Health.ToString());
    }

}