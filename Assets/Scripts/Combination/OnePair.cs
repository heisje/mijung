using System.Collections.Generic;

public class OnePair : Combi
{
    public OnePair()
    {
        Type = CombinationType.OnePair;
    }

    protected override long CheckScore(DiceCalculateDto diceCalculateDto)
    {
        return diceCalculateDto.Sum;
    }
    protected override bool CheckPossible(DiceCalculateDto diceCalculateDto)
    {
        return diceCalculateDto.SortedCountList[0].Value >= 2;
    }
}
