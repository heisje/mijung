using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacter, IClickable
{
    public EnemyStateType EnemyState { get; set; }
    public int Damage { get; set; }
    public int Health { get; set; }
    public SortedDictionary<StateConditionType, int> StateCondition { get; set; }
    public List<int> DamageGraph { get; private set; }
    public int DamageIndex { get; private set; }

    // 초기화 메서드
    public void Initialize(int health, int damage)
    {
        Health = health;
        Damage = damage;
        EnemyState = EnemyStateType.Alive;
        DamageGraph = new List<int> { 10, 10, 10, 10, 10 };
        DamageIndex = 0;
        transform.GetComponentInChildren<EnemyHP>().transform.GetComponent<ChangeTMP>().ChangeText(health.ToString());
    }

    // 공격 메서드 구현
    public int Attack()
    {
        return DamageGraph[DamageIndex++ % DamageGraph.Count];
    }

    // 피해를 입고 죽음 처리
    public void TakeDamage(int damage)
    {
        Health -= damage;
        transform.GetComponentInChildren<EnemyHP>().transform.GetComponent<ChangeTMP>().ChangeText(Health.ToString());

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
