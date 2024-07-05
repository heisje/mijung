using System.Collections.Generic;

public class TwoPair : Combi
{
    public TwoPair()
    {
        Type = CombinationType.TwoPair;
    }

    protected override long CheckScore(DiceCalculateDto diceCalculateDto)
    {
        return diceCalculateDto.Sum;
    }
    protected override bool CheckPossible(DiceCalculateDto diceCalculateDto)
    {
        return diceCalculateDto.SortedCountList[0].Value >= 2 && diceCalculateDto.SortedCountList[1].Value >= 2;
    }
}
