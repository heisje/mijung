public class FullHouse : Combi
{
    public FullHouse()
    {
        Type = CombinationType.FullHouse;
    }
    protected override long CheckScore(DiceCalculateDto diceCalculateDto)
    {
        return 25;
    }
    protected override bool CheckPossible(DiceCalculateDto diceCalculateDto)
    {
        return diceCalculateDto.SortedCountList[0].Value >= 3 && diceCalculateDto.SortedCountList[1].Value >= 2;
    }
}
