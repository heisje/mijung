using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public GameObject tigerPrefab;
    public List<Enemy> Enemies;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 적 선택
    public void SelectEnemy()
    {
        SpawnEnemy();
    }

    // 적 생성
    public void SpawnEnemy()
    {
        GameObject tigerObject = Instantiate(tigerPrefab, transform);
        Tiger tiger = tigerObject.GetComponent<Tiger>();
        tiger.Initialize(100, 20);
        Enemies.Add(tiger);
    }

    public void AllEnemyCalculateAttackDamage()
    {
        foreach (var enemy in Enemies)
        {
            enemy.CalculateAttackDamage();
        }
    }

    public bool CheckAllDeadEnemy()
    {
        foreach (var enemy in Enemies)
        {
            if (enemy.EnemyState == EnemyStateType.Alive) return false;
        }
        return true;
    }
}
