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
    public new int Health;
    public SortedDictionary<CombinationType, Combi> HaveCombiDict;   // 처음 가지고 있는 조합
    public SkillCardSet initialSkillCardSet; // 초기 카드 팩

    // 스킬카드를 저장해두는 곳. 
    public List<Skill> HaveSkillList;

    // Hp
    public ChangeTMP ChangeTMP;

    public int[] GetDiceValues()
    {
        return HaveDices.Select(dice => dice.Value).ToArray();
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

            HaveSkillList = new(){
                new OnePairSkill(),
                new TwoPairSkill(),
                new ThreeOfAKindSkill(),
                new FourOfAKindSkill(),
                new SmallStraightSkill(),
                new LargeStraightSkill(),
                new FiveOfAKindSkill(),
            };
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