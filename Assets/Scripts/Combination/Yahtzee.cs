using System.Collections.Generic;
using System.Linq;

public class Yahtzee : Combi
{
    public Yahtzee()
    {
        Type = CombinationType.Yahtzee;
    }

    protected override long CheckScore(DiceCalculateDto diceCalculateDto)
    {
        return 50;
    }
    protected override bool CheckPossible(DiceCalculateDto diceCalculateDto)
    {
        return diceCalculateDto.SortedCountList[0].Value >= 5;
    }

}
