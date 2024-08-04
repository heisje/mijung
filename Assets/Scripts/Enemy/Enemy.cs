using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, IClickable
{
    public EnemyStateType EnemyState { get; set; }
    public int Damage { get; set; }
    public int[] DamageGraph;

    // 초기화 메서드
    public void Initialize(int health, int damage)
    {
        Health = health;
        Damage = damage;
        DamageGraph = new int[] { 3, 4, 5, 6, 7 };
        EnemyState = EnemyStateType.Alive;
        transform.GetComponentInChildren<EnemyHP>().transform.GetComponent<ChangeTMP>().ChangeText(health.ToString());
    }

    // 공격 행동 저장
    public void CalculateAttackDamage()
    {
        int randomIndex = Random.Range(0, DamageGraph.Length);
        Damage = DamageGraph[randomIndex]; // 적절한 값 할당
        transform.GetComponentInChildren<EnemyDamage>().transform.GetComponent<ChangeTMP>().ChangeText(Damage.ToString());
    }
    // 공격 메서드 구현
    public int Attack()
    {
        return Damage;
    }

    // 피해를 입고 죽음 처리
    public override void TakeDamage(int damage)
    {
        AttackOrderValue = 0;

        if (Shield <= damage)
        {
            damage -= Shield;
            Shield = 0;
            AttackOrderValue = damage;
            Health -= damage;
        }
        else if (Shield > damage)
        {
            Shield -= damage;
        }

        transform.GetComponentInChildren<EnemyHP>().transform.GetComponent<ChangeTMP>().ChangeText("쉴드:" + Shield.ToString() + "체력:" + Health.ToString());

        if (Health <= 0)
        {
            EnemyState = EnemyStateType.Dead;
            Destroy();
        }
        else
        {
            Debug.Log("Enemy takes " + damage + " damage, remaining health: " + Health);
        }
    }

    // 오브젝트 삭제
    private void Destroy()
    {
        Destroy(gameObject);
    }

    public void OnClick()
    {
        Debug.Log($"{transform.name} 선택됨");
    }
}
