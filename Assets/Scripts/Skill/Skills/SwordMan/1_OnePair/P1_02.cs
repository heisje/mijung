public class P1_02 : AttackAdd
{
    public P1_02(SkillData skillData) : base(skillData)
    {
    }

    public override void AddAction(Sk_Context c, int value)
    {
        c.Target.UpdateCondition(ECondition.Hurt, 1);
    }
}