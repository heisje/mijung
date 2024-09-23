using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : Singleton<EnemyManager>, ILifeCycle
{
    public GameObject tigerPrefab;
    public GameObject batPrefab;
    public GameObject DefaultPrefab;
    public List<Enemy> Enemies;

    public void BeforeStage()
    {
        var position = 0;
        GameObject tigerObject = Instantiate(tigerPrefab, transform);
        Tiger tiger = tigerObject.GetComponent<Tiger>();
        Enemies.Add(tiger);
        for (var i = 1; i < 3; i++)
        {
            GameObject ghoulPrefab = Instantiate(DefaultPrefab, transform);
            Enemy ghoul = ghoulPrefab.AddComponent<Ghoul>();
            // 현재 위치에서 x축으로 200만큼 이동
            Vector3 newPosition = ghoulPrefab.transform.position;
            newPosition.x += position + 40 * i;
            ghoulPrefab.transform.position = newPosition;

            Enemies.Add(ghoul);
        }

        Enemies.ForEach((enemy) => { enemy.BeforeStage(); });
    }
    public void StartStage()
    {
        Enemies.ForEach((enemy) => { enemy.StartStage(); });
    }
    public void StartTurn()
    {
        Enemies.ForEach((enemy) => { enemy.StartTurn(); });
    }
    public void EndTurn()
    {
        Enemies.ForEach((enemy) => { enemy.EndTurn(); });
    }
    public void EndStage()
    {
        Enemies.ForEach((enemy) => { enemy.EndStage(); });
    }


    public bool CheckAllDeadEnemy()
    {
        foreach (var enemy in Enemies)
        {
            if (enemy.IsAlive == ECharacterState.Alive) return false;
        }
        return true;
    }

}
