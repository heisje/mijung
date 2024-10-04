
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class AttackOrderMM : Singleton<AttackOrderMM>, ILifeCycle
{
    public List<Character> AttackOrder { get; set; }
    public List<PlayerAction> PlayerActionList { get; set; }
    public ChangeTMP AttackOrderTMP;
    public void BeforeStage() { }

    public void StartStage()
    {
        AttackOrder = new();
        AttackOrder.Add(GameSession.Ins.Player);

        foreach (var enemy in EnemyManager.Ins.Enemies)
        {
            AttackOrder.Add(enemy);
        }
        AttackOrder.ForEach((character) =>
        {
            character.ResetAttackOrderValue();
            character.DisplayShieldHP();
        });
    }

    public void StartTurn()
    {
        PlayerActionList = new();
        AttackOrder.ForEach((character) =>
        {
            character.DisplayShieldHP();
        });

    }

    public void Act()
    {
        List<Character> NewAttackOrder = new();
        NewAttackOrder.Add(GameSession.Ins.Player);

        foreach (var enemy in EnemyManager.Ins.Enemies)
        {
            NewAttackOrder.Add(enemy);
        }
        NewAttackOrder.ForEach((character) =>
        {
            character.ResetAttackOrderValue();
        });


        // 우선권대로 행동 실시
        for (int index = 0; index < AttackOrder.Count; index++)
        {
            Character character = AttackOrder[index];

            // TODO: 죽으면 넘기기
            character.BeforeAttackOrder = index;

            // 무너짐 설정
            var MaxFellDown = character.GetCondition(ECondition.MaxFellDown);
            if (character.GetCondition(ECondition.FellDown) >= MaxFellDown)
            {
                character.SetCondition(ECondition.FellDown, 0);
                character.SetCondition(ECondition.Empowerment, 0);
                continue;
            }

            // 실제 행동
            if (character is Player player)
            {
                PlayerActionList.ForEach((playerAction) =>
                {
                    player.AttackOrderValue += playerAction.Execute();
                });
            }
            else if (character is Enemy enemy)
            {
                if (enemy.IsAlive == ECharacterState.Alive)
                {
                    enemy.AttackOrderValue += GameSession.Ins.Player.TakeDamage(enemy.Attack());
                }
            }
        }

        AttackOrder = NewAttackOrder
                    .OrderByDescending(x => x.AttackOrderValue)
                    .ThenBy(x => x.BeforeAttackOrder)
                    .ToList();

        var txt = new StringBuilder();
        foreach (var ch in AttackOrder)
        {
            txt.Append(ch.name + " ");
        }

        AttackOrderTMP.ChangeText(txt.ToString());
    }

    public void EndTurn() { }
    public void EndStage() { }



}