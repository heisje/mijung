public class P2_03 : AttackAdd
{
    public P2_03(SkillData skillData) : base(skillData)
    {
    }

    public override void AddAction(Sk_Context c, int value)
    {
        c.Target.UpdateCondition(ECondition.Hurt, value);
    }
}