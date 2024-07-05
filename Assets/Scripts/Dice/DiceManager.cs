using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // 계산용 객체 전달
    public DiceCalculateDto Calculate(int[] values)
    {
        // 조합 결과를 저장할 공간
        var resultCombinations = new SortedDictionary<CombinationType, long> { };

        // 객체 변경
        int maxPip = values.Max();
        int[] countList = new int[Math.Max(7, maxPip + 1)];
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

        int straightCount = 0;
        int maxStraightCount = 0;
        foreach (int count in countList)
        {
            if (count >= 1)
                maxStraightCount = Math.Max(++straightCount, maxStraightCount);
            else
                straightCount = 0;
        }

        return new DiceCalculateDto(countList, sortedCountList, sum, pairMaxPips, maxStraightCount);
    }
}