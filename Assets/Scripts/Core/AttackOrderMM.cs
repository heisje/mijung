
using System.Collections.Generic;
using System.Linq;

class AttackOrderMM : Singleton<AttackOrderMM>, ILifeCycle
{
    public List<Character> AttackOrder { get; set; }
    public List<PlayerAction<Character>> PlayerActionList { get; set; }

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
            var MaxFellDown = character.GetStateCondition(StateConditionType.MaxFellDown);
            if (character.GetStateCondition(StateConditionType.FellDown) >= MaxFellDown)
            {
                character.SetCondition(StateConditionType.FellDown, 0);
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
                if (enemy.State == CharacterStateType.Alive)
                {
                    enemy.AttackOrderValue += GameSession.Ins.Player.TakeDamage(enemy.Attack());
                }
            }
        }

        AttackOrder = NewAttackOrder
                    .OrderByDescending(x => x.AttackOrderValue)
                    .ThenBy(x => x.BeforeAttackOrder)
                    .ToList();
    }

    public void EndTurn() { }
    public void EndStage() { }



}