public class P3_05 : ShieldUpAdd
{
    public P3_05(SkillData skillData) : base(skillData)
    {
    }

    public override void AddAction(Sk_Context c, int value)
    {
        c.Owner.HurtBurst(c.Enemies.ToArray(), c.DiceInfo, value);
    }
}