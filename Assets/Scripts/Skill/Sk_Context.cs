using System.Collections.Generic;


/// <summary>
/// Action시 상태를 저장하기 위함
/// </summary>
public class Sk_Context
{
    public Player Player;
    public Character Target;
    public List<Enemy> Enemies;
    public Sk_Context(Character target)
    {
        Player = GameSession.Ins.Player;
        Target = target;
        Enemies = EnemyManager.Ins.Enemies;
    }
}