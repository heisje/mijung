using System;



[Serializable]
public class PlayerAction
{
    // 행동을 위해서 필요한 것
    // 스킬
    // 다이스
    // 환경정보
    public Sk_Context SkContext;
    private readonly Skill Skill;

    public PlayerAction(Skill skill, Sk_Context skContext)
    {
        Skill = skill;
        SkContext = skContext;
    }

    public int Execute()
    {

        // if (SkContext.DiceInfo.IsContainPip(6))
        // {
        //     SkContext.Enemies.ForEach(e =>
        //             {
        //                 var hurt = e.GetCondition(ECondition.Hurt);
        //                 for (var i = 0; i < hurt; i++)
        //                 {
        //                     SkContext.Owner.Attack(e, 5);
        //                 }
        //                 e.SetCondition(ECondition.Hurt, 0);
        //             });
        // }
        return Skill.OnSkill(SkContext);
    }
}