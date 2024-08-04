using System;

[Serializable]
public class PlayerAction<T>
{
    public DiceCalculateDto Dice;
    public T Target;
    private Func<DiceCalculateDto, T, bool> OnSkill;

    public PlayerAction(Func<DiceCalculateDto, T, bool> onSkill, DiceCalculateDto diceDTO, T target)
    {
        OnSkill = onSkill;
        Target = target;
        Dice = diceDTO;
    }

    public bool Execute()
    {
        return OnSkill(Dice, Target);
    }
}