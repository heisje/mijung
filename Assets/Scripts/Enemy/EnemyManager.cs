using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public GameObject tigerPrefab;
    public GameObject batPrefab;
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
        GameObject tigerObject = Instantiate(tigerPrefab, transform);
        Tiger tiger = tigerObject.GetComponent<Tiger>();
        tiger.Initialize(20, 20);

        Enemies.Add(tiger);
        for (var i = 1; i < 3; i++)
        {
            GameObject batObject = Instantiate(batPrefab, transform);
            Bat bat = batObject.GetComponent<Bat>();
            bat.Initialize(20, 20);

            // 현재 위치에서 x축으로 200만큼 이동
            Vector3 newPosition = batObject.transform.position;
            newPosition.x += position + 30 * i;
            batObject.transform.position = newPosition;

            Enemies.Add(bat);
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
            if (enemy.HP <= 0)
            {
                enemy.State = CharacterStateType.Dead;
            }
            //if (enemy.EnemyState == EnemyStateType.Alive) return false;
        }
        return true;
    }
}
