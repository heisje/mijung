using System;



[Serializable]
public class PlayerAction
{
    // 행동을 위해서 필요한 것
    // 스킬
    // 다이스
    // 환경정보
    public Sk_Context Context;
    public DiceCalculateDto Dice;
    private readonly Skill Skill;

    public PlayerAction(Skill skill, DiceCalculateDto diceDTO, Sk_Context fieldActionContext)
    {
        Skill = skill;
        Dice = diceDTO ?? throw new Exception("DTO 다시봐라");
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
        return Skill.OnSkill(Dice, Context);
    }
}