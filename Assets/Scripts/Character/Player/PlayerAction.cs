using System;



[Serializable]
public class PlayerAction
{
    public Sk_Context FieldActionContext;
    public DiceCalculateDto Dice;
    private readonly Func<DiceCalculateDto, Sk_Context, int> OnSkill;

    public PlayerAction(Func<DiceCalculateDto, Sk_Context, int> onSkill, DiceCalculateDto diceDTO, Sk_Context fieldActionContext)
    {
        OnSkill = onSkill;
        Dice = diceDTO;
        FieldActionContext = fieldActionContext;
    }

    public int Execute()
    {
        return OnSkill(Dice, FieldActionContext);
    }
}