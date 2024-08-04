using System;
using System.Collections.Generic;

[Serializable]
public class DiceCalculateDto
{
    public int[] CountList;

    // Key: 눈금, Value: 눈금의 개수
    // [(3, 3), (2, 2), (4, 2), (1, 1), (5, 1), (0, 0), (6, 0)]
    public List<KeyValuePair<int, int>> SortedCountList;
    public long Sum;
    public int MaxStraightCount;

    // Key: Pair의 개수, Value: MaxPip
    public SortedDictionary<int, int> PairLargePips;
    public SortedDictionary<int, int> StraightLargePips;

    public DiceCalculateDto(int[] countList, List<KeyValuePair<int, int>> sortedCountList, long sum, SortedDictionary<int, int> pairMaxPips, int maxStraightCount, SortedDictionary<int, int> straightLargePips)
    {
        CountList = countList;
        SortedCountList = sortedCountList;
        Sum = sum;
        PairLargePips = pairMaxPips;
        MaxStraightCount = maxStraightCount;
        StraightLargePips = straightLargePips;
    }
}