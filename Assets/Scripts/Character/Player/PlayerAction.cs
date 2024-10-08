using System;



[Serializable]
public class PlayerAction
{
    public Sk_Context Context;
    public DiceCalculateDto Dice;
    private readonly Func<DiceCalculateDto, Sk_Context, int> OnSkill;

    public PlayerAction(Func<DiceCalculateDto, Sk_Context, int> onSkill, DiceCalculateDto diceDTO, Sk_Context fieldActionContext)
    {
        OnSkill = onSkill;
        Dice = diceDTO;
        Context = fieldActionContext;
    }

    public int Execute()
    {
        if (Dice.IsContainPip(6))
        {
            Context.Enemies.ForEach(e =>
                    {
                        var hurt = e.GetCondition(ECondition.Hurt);
                        for (var i = 0; i < hurt; i++)
                        {
                            Context.Player.Attack(e, 5);
                        }
                        e.SetCondition(ECondition.Hurt, 0);
                    });
        }
        return OnSkill(Dice, Context);
    }
}