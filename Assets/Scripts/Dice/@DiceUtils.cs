using System.Collections.Generic;
using System.Linq;

public static class DiceUtils
{
    public delegate bool SkillHandler(DiceCalculateDto diceDto);        // 람다식 연결용, 매개변수와 리턴값이 중요함
    public delegate int LargePipHandler(DiceCalculateDto diceDto);      // 람다식 연결용, 매개변수와 리턴값이 중요함
    private static Dictionary<ECombi, SkillHandler> CheckCombiDict = new();      // 콤비네이션 체크
    private static Dictionary<ECombi, LargePipHandler> CheckLargePipDict = new();// 큰 수 체크
    static DiceUtils()
    {
        CheckCombiDict[ECombi.OnePair] = (diceDto) => diceDto.SortedCountList[0].Value >= 2;
        CheckCombiDict[ECombi.TwoPair] = (diceDto) => diceDto.SortedCountList[0].Value >= 4 || (diceDto.SortedCountList[0].Value >= 2 && diceDto.SortedCountList[1].Value >= 2);
        CheckCombiDict[ECombi.ThreeOfAKind] = (diceDto) => diceDto.SortedCountList[0].Value >= 3;
        CheckCombiDict[ECombi.FourOfAKind] = (diceDto) => diceDto.SortedCountList[0].Value >= 4;
        CheckCombiDict[ECombi.FiveOfAKind] = (diceDto) => diceDto.SortedCountList[0].Value >= 5;
        CheckCombiDict[ECombi.ThreeStraight] = (diceDto) => diceDto.MaxStraightCount >= 3;
        CheckCombiDict[ECombi.FourStraight] = (diceDto) => diceDto.MaxStraightCount >= 4;
        CheckCombiDict[ECombi.FiveStraight] = (diceDto) => diceDto.MaxStraightCount >= 5;

        // 콤비에 따른 LargePip Checker
        CheckLargePipDict[ECombi.OnePair] = (diceDto) => diceDto.PairLargePips.TryGetValue(2, out int largePip) ? largePip : 0;
        CheckLargePipDict[ECombi.TwoPair] = (diceDto) =>
        {
            if (diceDto.SortedCountList[0].Value >= 4)
            {
                return diceDto.PairLargePips.TryGetValue(4, out int largePip4) ? largePip4 : 0;
            }
            if (diceDto.SortedCountList[0].Value >= 2 && diceDto.SortedCountList[1].Value >= 2)
            {
                return diceDto.PairLargePips.TryGetValue(2, out int largePip) ? largePip : 0;
            }
            return 0;
        };
        CheckLargePipDict[ECombi.ThreeOfAKind] = (diceDto) => diceDto.PairLargePips.TryGetValue(3, out int largePip) ? largePip : 0;
        CheckLargePipDict[ECombi.FourOfAKind] = (diceDto) => diceDto.PairLargePips.TryGetValue(4, out int largePip) ? largePip : 0;
        CheckLargePipDict[ECombi.FiveOfAKind] = (diceDto) => diceDto.PairLargePips.TryGetValue(5, out int largePip) ? largePip : 0;
        CheckLargePipDict[ECombi.ThreeStraight] = (diceDto) => diceDto.StraightLargePips.TryGetValue(3, out int largePip) ? largePip : 0;
        CheckLargePipDict[ECombi.FourStraight] = (diceDto) => diceDto.StraightLargePips.TryGetValue(4, out int largePip) ? largePip : 0;
        CheckLargePipDict[ECombi.FiveStraight] = (diceDto) => diceDto.StraightLargePips.TryGetValue(5, out int largePip) ? largePip : 0;
    }
    public static bool GetIsCombi(this DiceCalculateDto diceDto, ECombi combi)
    {
        return CheckCombiDict[combi](diceDto);
    }

    public static int GetLargePip(this DiceCalculateDto diceDto, ECombi combi)
    {
        return CheckLargePipDict[combi](diceDto);
    }

    public static bool IsContainPip(this DiceCalculateDto diceDto, int pip)
    {
        if (diceDto.CountList[pip] > 0) return true;
        return false;
    }

    public static bool IsContainPip(this DiceCalculateDto diceDto, params int[] pips)
    {
        foreach (var pip in pips)
        {
            if (pip < 0 || pip >= diceDto.CountList.Count() || diceDto.CountList[pip] <= 0) return false;
        }
        return true;
    }

}