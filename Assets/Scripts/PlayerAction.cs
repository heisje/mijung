using System;

[Serializable]
public class PlayerAction<T> where T : Character
{
    public DiceCalculateDto Dice;
    public T Target;
    private Func<DiceCalculateDto, T, int> OnSkill;

    public PlayerAction(Func<DiceCalculateDto, T, int> onSkill, DiceCalculateDto diceDTO, T target)
    {
        OnSkill = onSkill;
        Target = target;
        Dice = diceDTO;
    }

    public int Execute()
    {
        return OnSkill(Dice, Target);
    }
}