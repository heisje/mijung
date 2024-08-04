using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, IClickable
{
    public EnemyStateType EnemyState { get; set; }
    public int Damage { get; set; }
    public int[] DamageGraph;


    // 초기화 메서드
    public void Initialize(int hp, int damage)
    {
        HP = hp;
        Damage = damage;
        DamageGraph = new int[] { 6, 9, 12, 15, 18 };
        EnemyState = EnemyStateType.Alive;
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

    private void CheckDestroy()
    {
        if (HP <= 0)
        {
            EnemyState = EnemyStateType.Dead;
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
