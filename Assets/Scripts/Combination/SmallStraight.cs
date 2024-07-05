using System.Linq;

public class SmallStraight : Combi
{
    public SmallStraight()
    {
        Type = CombinationType.SmallStraight;
    }

    protected override long CheckScore(DiceCalculateDto diceCalculateDto)
    {
        return 30;
    }
    protected override bool CheckPossible(DiceCalculateDto diceCalculateDto)
    {
        int line = 0;
        foreach (int count in diceCalculateDto.CountList)
        {
            if (count >= 1)
            {
                line++;
                if (line >= 4)
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
