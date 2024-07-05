public class FourOfAKind : Combi
{
    public FourOfAKind()
    {
        Type = CombinationType.FourOfAKind;
    }

    protected override long CheckScore(DiceCalculateDto diceCalculateDto)
    {
        return diceCalculateDto.Sum;
    }

    protected override bool CheckPossible(DiceCalculateDto diceCalculateDto)
    {
        return diceCalculateDto.SortedCountList[0].Value >= 4;
    }
}
