using System.Linq;

public class LargeStraight : Combi
{
    public LargeStraight()
    {
        Type = CombinationType.LargeStraight;
    }

    protected override long CheckScore(DiceCalculateDto diceCalculateDto)
    {
        return 40;
    }

    protected override bool CheckPossible(DiceCalculateDto diceCalculateDto)
    {
        int line = 0;
        foreach (int count in diceCalculateDto.CountList)
        {
            if (count >= 1)
            {
                line++;
                if (line >= 5)
                {
                    return true;
                }
            }
            else
            {
                line = 0;
            }
        }
        return false;
    }

}
