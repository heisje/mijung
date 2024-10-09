using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class DiceInfo
{
    public int[] CountList;

    // Key: 눈금, Value: 눈금의 개수
    // [(3, 3), (2, 2), (4, 2), (1, 1), (5, 1), (0, 0), (6, 0)]
    public List<KeyValuePair<int, int>> SortedCountList;
    public long Sum;                    // 합계
    public int MaxStraightCount;        // 스트레이트 길이

    // Key: Pair의 개수, Value: MaxPip
    public SortedDictionary<int, int> PairLargePips;
    public SortedDictionary<int, int> StraightLargePips;

    public DiceInfo(int[] pips)
    {
        // 객체 변경

        this.CountList = CalculateCountList(pips);

        // 높은 순 정렬
        this.SortedCountList = CountList
            .Select((count, index) => new KeyValuePair<int, int>(index, count))
            .OrderByDescending(item => item.Value)
            .ToList();

        this.Sum = pips.Sum();

        // 쌍의 개수별로 최대 눈금 계산
        // ex. 1: 6, 2: 3, 3: 4,...
        this.PairLargePips = CalculatePairLargePips(CountList, SortedCountList);

        (this.MaxStraightCount, this.StraightLargePips) = CalculateStraight(CountList);
    }


    ///////////////////////////////////////////////////
    // 아래는 계산을 위한 함수들
    ///////////////////////////////////////////////////
    public static int[] CalculateCountList(int[] pips)
    {
        int N = Math.Max(7, pips.Length);
        int[] countList = new int[N];
        foreach (var pip in pips)
        {
            countList[pip] += 1;
        }
        return countList;
    }

    public static SortedDictionary<int, int> CalculatePairLargePips(int[] countList, List<KeyValuePair<int, int>> sortedCountList)
    {
        var pairMaxPips = new SortedDictionary<int, int>();
        for (int i = 1; i < countList.Length; i++)
        {
            int maxPipForPair = sortedCountList
                .Where(item => item.Value >= i)
                .Select(item => item.Key)
                .DefaultIfEmpty(0)
                .Max();
            pairMaxPips[i] = maxPipForPair;
        }
        return pairMaxPips;
    }

    public static (int maxStraightCount, SortedDictionary<int, int> straightLargePips) CalculateStraight(int[] countList)
    {
        int straightCount = 0;
        int maxStraightCount = 0;
        SortedDictionary<int, int> straightLargePips = new();
        for (int i = 0; i < countList.Length; i++)
        {
            if (countList[i] >= 1)
            {
                maxStraightCount = Math.Max(++straightCount, maxStraightCount);
                int findStraightLargePip = straightCount;
                while (findStraightLargePip >= 1)
                {
                    straightLargePips[straightCount] = i;
                    findStraightLargePip -= 1;
                }
            }
            else
                straightCount = 0;
        }
        return (maxStraightCount, straightLargePips);
    }
}

