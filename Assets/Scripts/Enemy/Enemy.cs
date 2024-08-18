using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character, IClickable
{
    public abstract EnemyType EnemyId { get; set; }
    public int Damage { get; set; }
    public int[] DamageGraph { get; set; }

    public override void BeforeStage()
    {
        base.BeforeStage();
        HP = StartDataManager.ENEMY_HP[EnemyId];
        DamageGraph = StartDataManager.ENEMY_DamageGraph[EnemyId];
        var MaxFellDown = StartDataManager.ENEMY_MaxFellDown[EnemyId];
        SetCondition(StateConditionType.MaxFellDown, MaxFellDown);
    }
    // 초기화 메서드
    public override void StartStage()
    {
        base.StartStage();
        State = CharacterStateType.Alive;
    }
    public override void StartTurn()
    {
        base.StartTurn();
        int randomIndex = Random.Range(0, DamageGraph.Length);
        Damage = DamageGraph[randomIndex]; // 적절한 값 할당
        transform.GetComponentInChildren<EnemyDamage>().transform.GetComponent<ChangeTMP>().ChangeText(Damage.ToString());
    }

    // 데미지 연산 후 저장
    public void CalculateAttackDamage()
    {

    }

    // 공격 메서드 구현
    public int Attack()
    {
        return Damage;
    }

    public void OnClick()
    {
        Debug.Log($"{transform.name} 선택됨");
    }


}
