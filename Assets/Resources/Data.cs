using System.Collections.Generic;

public static class StartDataManager
{
    public static readonly ESkillID[] PLYER_START_SKILL = {
        ESkillID.OnePair,
        ESkillID.TwoPair,
        ESkillID.ThreeOfAKind,
        ESkillID.FourOfAKind,
        ESkillID.SmallStraight,
        ESkillID.Straight,
        ESkillID.LargeStraight,
        ESkillID.FiveOfAKind
    };

    public static readonly Dictionary<EnemyType, int> ENEMY_HP = new()
    {
        { EnemyType.Bat, 10 },
        { EnemyType.Tiger, 120 },
        { EnemyType.Ghoul, 16 },
    };

    public static readonly Dictionary<EnemyType, int[]> ENEMY_DamageGraph = new()
    {
        { EnemyType.Bat, new int[] { 1, 2, 3, 4, 5, 6 } },
        { EnemyType.Tiger, new int[] { 3, 6, 9, 12, 15, 18 } },
        { EnemyType.Ghoul, new int[] { 1, 2, 3, 4, 5, 6 } },
    };

    public static readonly Dictionary<EnemyType, int> ENEMY_MaxFellDown = new()
    {
        { EnemyType.Bat, 1 },
        { EnemyType.Tiger, 3 },
        { EnemyType.Ghoul, 1 },
    };

}
