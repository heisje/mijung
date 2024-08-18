using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceManager : Singleton<DiceManager>
{
    // 계산용 객체 전달
    public DiceCalculateDto Calculate(int[] values)
    {
        // 조합 결과를 저장할 공간
        var resultCombinations = new SortedDictionary<CombiType, long> { };

        // 객체 변경

        int[] countList = new int[Math.Max(7, 6 + 1)];
        Array.Clear(countList, 0, countList.Length);
        List<KeyValuePair<int, int>> sortedCountList;
        for (int i = 0; i < countList.Length; i++)
        {
            countList[i] = values.Count(v => v == i);
        }
        sortedCountList = countList
            .Select((count, index) => new KeyValuePair<int, int>(index, count))
            .OrderByDescending(item => item.Value)
            .ToList();

        long sum = values.Sum();

        // 쌍의 개수별로 최대 눈금 계산
        // ex. 1: 6, 2: 3, 3: 4,...
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

        // 스트레이트 길이를 재는 함수
        // 스트레이트시 LargePip도 같이 구한다.
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

        return new DiceCalculateDto(countList, sortedCountList, sum, pairMaxPips, maxStraightCount, straightLargePips);
    }
}