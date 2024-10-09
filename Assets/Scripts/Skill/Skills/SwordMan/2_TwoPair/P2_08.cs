public class P2_08 : AttackAdd
{
    public P2_08(SkillData skillData) : base(skillData)
    {
    }

    public override void AddAction(Sk_Context c, int value)
    {
        c.Owner.HurtBurst(c.Enemies.ToArray(), c.DiceInfo, value);
    }
}