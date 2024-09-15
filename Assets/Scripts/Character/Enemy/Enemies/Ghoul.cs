using System.Collections.Generic;
using UnityEngine;

public class Ghoul : Enemy
{
    public override EnemyType EnemyId { get; set; } = EnemyType.Ghoul;

    public override void BeforeStage()
    {
        base.BeforeStage();
        SetCondition(EStateCondition.CanRebirth, 1);
    }
}
