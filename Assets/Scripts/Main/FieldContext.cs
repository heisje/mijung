using System.Collections.Generic;
using System.Linq;


// 필드 상태를 저장하는 Context
public class FieldContext
{
    public Player Player;
    public List<Enemy> Enemies;

    public FieldContext(Player player, List<Enemy> enemies)
    {
        Player = player;
        Enemies = enemies;
    }
}