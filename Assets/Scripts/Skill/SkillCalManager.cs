using System;
using System.Collections.Generic;

public class SkillCalManager : Singleton<SkillCalManager>
{
    public delegate bool SkillHandler(DiceCalculateDto diceDto);    // 람다식 연결용, 매개변수와 리턴값이 중요함
    public delegate int LargePipHandler(DiceCalculateDto diceDto);    // 람다식 연결용, 매개변수와 리턴값이 중요함

    // CSV에 저장된 Type별 행동 방침을 설정
    protected Dictionary<CombiType, SkillHandler> CheckCombiDict = new();
    protected Dictionary<CombiType, LargePipHandler> CheckLargePip = new();
    protected Dictionary<CombiType, SkillHandler> OnDefinedSkillDict = new();
    protected override void Instantiation()
    {
        // 콤비 체커
        CheckCombiDict[CombiType.OnePair] = (diceDto) => diceDto.SortedCountList[0].Value >= 2;
        CheckCombiDict[CombiType.TwoPair] = (diceDto) => diceDto.SortedCountList[0].Value >= 2 && diceDto.SortedCountList[1].Value >= 2; ;
        CheckCombiDict[CombiType.ThreeOfAKind] = (diceDto) => diceDto.SortedCountList[0].Value >= 3;
        CheckCombiDict[CombiType.FourOfAKind] = (diceDto) => diceDto.SortedCountList[0].Value >= 4;
        CheckCombiDict[CombiType.FiveOfAKind] = (diceDto) => diceDto.SortedCountList[0].Value >= 5;
        CheckCombiDict[CombiType.SmallStraight] = (diceDto) => diceDto.MaxStraightCount >= 4;
        CheckCombiDict[CombiType.LargeStraight] = (diceDto) => diceDto.MaxStraightCount >= 5;

        // 콤비에 따른 LargePip Checker
        CheckLargePip[CombiType.OnePair] = (diceDto) => diceDto.PairLargePips.TryGetValue(2, out int largePip) ? largePip : 0;
        CheckLargePip[CombiType.TwoPair] = (diceDto) => diceDto.PairLargePips.TryGetValue(2, out int largePip) ? largePip : 0;
        CheckLargePip[CombiType.ThreeOfAKind] = (diceDto) => diceDto.PairLargePips.TryGetValue(3, out int largePip) ? largePip : 0;
        CheckLargePip[CombiType.FourOfAKind] = (diceDto) => diceDto.PairLargePips.TryGetValue(4, out int largePip) ? largePip : 0;
        CheckLargePip[CombiType.FiveOfAKind] = (diceDto) => diceDto.PairLargePips.TryGetValue(5, out int largePip) ? largePip : 0;
        CheckLargePip[CombiType.SmallStraight] = (diceDto) => diceDto.StraightLargePips.TryGetValue(4, out int largePip) ? largePip : 0;
        CheckLargePip[CombiType.LargeStraight] = (diceDto) => diceDto.StraightLargePips.TryGetValue(5, out int largePip) ? largePip : 0;
    }
    public bool CheckCombi(CombiType key, DiceCalculateDto diceDto)
    {
        return CheckCombiDict[key](diceDto);
    }

    // 타겟에 따른 Check는 다른 함수에서 하고,
    // 스킬을 적중시키는 것만 다룸
    public virtual bool OnDefinedSkill<T>(Skill skill, DiceCalculateDto diceDto, T target, Player player, List<Enemy> enemies) where T : Character
    {
        // 해당하는 것만 바꿀 수 있게 최적화
        var largePip = CheckLargePip[skill.Combi](diceDto);
        List<KeyValuePair<FormulaType, string>> replaceFormulas = new();

        // 전처리(텍스트 대치)
        int repeatCount = 1;
        foreach (var formula in skill.Formulas)
        {
            FormulaType f = formula.Key;
            string s = formula.Value;
            KeyValuePair<FormulaType, string> replaceKeyPair = new(f, s.Replace("{largePip}", largePip.ToString()));
            if (f == FormulaType.Repeat)
            {
                repeatCount = EvaluateFormula(replaceKeyPair.Value);
                continue;
            }
            replaceFormulas.Add(replaceKeyPair);
        }

        // 스킬 효과 발동
        for (var i = 0; i < repeatCount; i++)   // 반복만 따로 처리
        {
            foreach (var formula in skill.Formulas) // Formula 순회
            {
                FormulaType f = formula.Key;
                string s = formula.Value;
                string replaceKeyPair = s.Replace("{largePip}", largePip.ToString());

                int value = EvaluateFormula(replaceKeyPair);
                switch (formula.Key)
                {
                    case FormulaType.TargetAttack:
                        target.TakeDamage(value + player.GetStateCondition(StateConditionType.Empower));
                        break;
                    case FormulaType.AllAttack:
                        foreach (var enemy in enemies)
                        {
                            enemy.TakeDamage(value + player.GetStateCondition(StateConditionType.Empower));
                        }
                        break;
                    case FormulaType.PlayerShieldUp:
                        player.Shield += value;
                        break;
                    case FormulaType.PlayerShieldDown:
                        player.Shield = ChangeMinZero(player.Shield, value);
                        break;
                    case FormulaType.PlayerEmpowerUp:
                        player.ChangeCondition(StateConditionType.Empower, value);
                        break;
                    case FormulaType.PlayerEmpowerDown:
                        player.ChangeCondition(StateConditionType.Empower, -value);
                        break;
                    case FormulaType.PlayerHeal:
                        player.Health += value;
                        break;
                    case FormulaType.TargetMarkUp:
                        target.ChangeCondition(StateConditionType.Mark, value);
                        break;
                    case FormulaType.TargetMarkDown:
                        target.ChangeCondition(StateConditionType.Mark, -value);
                        break;
                }
            }
        }

        return false;
    }
    public int EvaluateFormula(string formula)
    {

        // DataTable을 사용하여 수식을 계산합니다.
        var dataTable = new System.Data.DataTable();

        // Compute 메서드를 사용하여 수식의 결과를 계산하고, 정수로 변환하여 반환합니다.
        return Convert.ToInt32(dataTable.Compute(formula, string.Empty));
    }

    // 계산 시 최소값 0을 리턴
    public int ChangeMinZero(int pre, int append)
    {
        return Math.Max(pre + append, 0);
    }
}