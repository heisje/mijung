using System.Collections.Generic;


/// <summary>
/// Action시 상태를 저장하기 위함
/// </summary>
public class Sk_Context
{
    public Character Owner;
    public Character Target;
    public List<Enemy> Enemies;
    public DiceInfo DiceInfo;
    public Sk_Context(Character owner, Character target, DiceInfo diceInfo)
    {
        this.Owner = owner;
        this.Target = target;
        this.Enemies = EnemyManager.Ins.Enemies;
        this.DiceInfo = diceInfo;
    }
}