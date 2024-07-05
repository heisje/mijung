public class ThreeOfAKind : Combi
{
    public ThreeOfAKind()
    {
        Type = CombinationType.ThreeOfAKind;
    }
    protected override long CheckScore(DiceCalculateDto diceCalculateDto)
    {
        return diceCalculateDto.Sum;
    }
    protected override bool CheckPossible(DiceCalculateDto diceCalculateDto)
    {
        return diceCalculateDto.SortedCountList[0].Value >= 3;
    }
}
