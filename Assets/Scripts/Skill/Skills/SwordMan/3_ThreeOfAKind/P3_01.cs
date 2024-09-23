public class P3_01 : ShieldUpAdd
{
    public P3_01(SkillData skillData) : base(skillData)
    {
    }

    public override void AddAction(Sk_Context c, int value)
    {
        c.Enemies.ForEach(e => e.UpdateCondition(ECondition.Hurt, 1));
    }
}