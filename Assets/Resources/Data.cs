using System.Collections.Generic;

public static class StartDataManager
{
    public static SkillID[] PLYER_START_SKILL = {
        SkillID.OnePair,
        SkillID.TwoPair,
        SkillID.ThreeOfAKind,
        SkillID.FourOfAKind,
        SkillID.SmallStraight,
        SkillID.Straight,
        SkillID.LargeStraight,
        SkillID.FiveOfAKind
    };

    public static Dictionary<EnemyType, int> ENEMY_HP = new()
    {
        { EnemyType.Bat, 10 },
        { EnemyType.Tiger, 150 },
        { EnemyType.Ghoul, 10 },
    };

    public static Dictionary<EnemyType, int[]> ENEMY_DamageGraph = new()
    {
        { EnemyType.Bat, new int[] { 1, 2, 3, 4, 5, 6 } },
        { EnemyType.Tiger, new int[] { 3, 6, 9, 12, 15, 18 } },
        { EnemyType.Ghoul, new int[] { 1, 2, 3, 4, 5, 6 } },
    };

    public static Dictionary<EnemyType, int> ENEMY_MaxFellDown = new()
    {
        { EnemyType.Bat, 1 },
        { EnemyType.Tiger, 3 },
        { EnemyType.Ghoul, 1 },
    };

}
