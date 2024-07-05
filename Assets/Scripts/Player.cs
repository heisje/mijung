using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter
{
    public Dice[] HaveDices;
    public int NOfRoll = 3; // 최대 굴리기 횟수
    public SortedDictionary<CombinationType, Combi> HaveCombiDict;   // 처음 가지고 있는 조합
    public SkillCardSet initialSkillCardSet; // 초기 카드 팩

    // 스킬카드를 저장해두는 곳. 
    public List<Skill> HaveSkillList;

    public int Health { get; set; }
    public SortedDictionary<StateConditionType, int> StateCondition { get; set; }

    private void Awake()
    {
        HaveCombiDict = new SortedDictionary<CombinationType, Combi>
        {
            { CombinationType.Aces, new Top(CombinationType.Aces, 1) },
            { CombinationType.Twos, new Top(CombinationType.Twos, 2) },
            { CombinationType.Threes, new Top(CombinationType.Threes, 3) },
            { CombinationType.Fours, new Top(CombinationType.Fours, 4) },
            { CombinationType.Fives, new Top(CombinationType.Fives, 5) },
            { CombinationType.Sixes, new Top(CombinationType.Sixes, 6) },
            { CombinationType.Chance, new Chance() },
            { CombinationType.TwoPair, new TwoPair() },
            { CombinationType.ThreeOfAKind, new ThreeOfAKind() },
            { CombinationType.FourOfAKind, new FourOfAKind() },
            { CombinationType.FullHouse, new FullHouse() },
            { CombinationType.SmallStraight, new SmallStraight() },
            { CombinationType.LargeStraight, new LargeStraight() },
            { CombinationType.Yahtzee, new Yahtzee() }
        };

        HaveSkillList = new(){
            new OnePairSkill(),
            new TwoPairSkill(),
            new ThreeOfAKindSkill(),
            new FourOfAKindSkill(),
            new SmaillStraightSkill(),
            new LargeStraightSkill(),
            new FiveOfAKindSkill(),

        };
    }

    public int[] GetDiceValues()
    {
        return HaveDices.Select(dice => dice.Value).ToArray();
    }

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
}