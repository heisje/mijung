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
        var position = 0;
        for (var i = 0; i < 3; i++)
        {
            GameObject tigerObject = Instantiate(tigerPrefab, transform);
            Tiger tiger = tigerObject.GetComponent<Tiger>();
            tiger.Initialize(150, 20);

            // 현재 위치에서 x축으로 200만큼 이동
            Vector3 newPosition = tigerObject.transform.position;
            newPosition.x += position + 30 * i;
            tigerObject.transform.position = newPosition;

            Enemies.Add(tiger);
        }

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
