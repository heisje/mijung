public class Top : Combi
{
    public Top(CombinationType combinationType, int pip)
    {
        Type = combinationType;
        Pip = pip;
    }

    protected override long CheckScore(DiceCalculateDto diceCalculateDto)
    {
        return diceCalculateDto.CountList[Pip] * Pip;
    }

    protected override bool CheckPossible(DiceCalculateDto diceCalculateDto)
    {
        return true;
    }
}
