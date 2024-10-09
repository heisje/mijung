using System.Collections.Generic;

public static class StartDataManager
{
    public static readonly E_Sk_Id[] PLYER_START_SKILL = {
        E_Sk_Id.OnePair,
        E_Sk_Id.TwoPair,
        E_Sk_Id.ThreeOfAKind,
        E_Sk_Id.FourOfAKind,
        E_Sk_Id.SmallStraight,
        E_Sk_Id.Straight,
        E_Sk_Id.LargeStraight,
        E_Sk_Id.FiveOfAKind
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

public static class GLOBAL_CONST
{
    public const int RAGE_DAMAGE = 10;
    public const int HURT_PIP = 6;
    public const int HURT_DAMAGE = 5;
}