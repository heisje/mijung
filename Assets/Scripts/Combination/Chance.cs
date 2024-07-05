public class Chance : Combi
{
    public Chance()
    {
        Type = CombinationType.Chance;
    }

    protected override long CheckScore(DiceCalculateDto diceCalculateDto)
    {
        return diceCalculateDto.Sum;
    }
    protected override bool CheckPossible(DiceCalculateDto diceCalculateDto)
    {
        return true;
    }
}
