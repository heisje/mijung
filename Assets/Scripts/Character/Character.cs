using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Character : MonoBehaviour, ILifeCycle
{
    // HP 속성
    public int HP
    {
        get => Condition.TryGetValue(ECondition.HP, out var hp) ? hp : 0;
        set => SetCondition(ECondition.HP, value);
    }
    public int Shield
    {
        get => Condition.TryGetValue(ECondition.Shield, out var shield) ? shield : 0;
        set => SetCondition(ECondition.Shield, value);
    }

    public Dictionary<ECondition, int> Condition = new();

    // UI
    public ChangeTMP ShieldDisplay;
    public ChangeTMP HPDisplay;
    public ChangeTMP ExtraDisplay;

    // 상태
    public ECharacterState IsAlive;

    // 우선권 변수
    public int AttackOrderValue = 0;
    public int BeforeAttackOrder { get; set; }

    private void Awake()
    {
        ShieldDisplay = transform.GetComponentInChildren<ShieldDisplay>().transform.GetComponent<ChangeTMP>();
        HPDisplay = transform.GetComponentInChildren<HPDisplay>().transform.GetComponent<ChangeTMP>();
        ExtraDisplay = transform.GetComponentInChildren<ExtraDisplay>().transform.GetComponent<ChangeTMP>();
        DisplayShieldHP();
    }

    public void ResetAttackOrderValue()
    {
        AttackOrderValue = 0;
    }


    // 데미지를 받음
    public virtual int TakeDamage(int damage)
    {

        int takeHealthDamage = 0;
        if (Shield <= damage)
        {
            damage -= Shield;
            UpdateCondition(ECondition.TakeShield, Shield);
            Shield = 0;
            takeHealthDamage += damage;
            HP -= damage;
            UpdateCondition(ECondition.TakeHP, damage);
        }
        else if (Shield > damage)
        {
            Shield -= damage;
            UpdateCondition(ECondition.TakeShield, Shield);
        }

        if (takeHealthDamage >= 10)
        {
            UpdateCondition(ECondition.FellDown, 1);
        }

        if (HP <= 0)
        {
            IsAlive = ECharacterState.Dead;
            if (GetCondition(ECondition.CanRebirth) >= 1)
            {
                IsAlive = ECharacterState.TempDead;
                SetCondition(ECondition.RebirthTime, 3);
            }
        }
        DisplayShieldHP();
        return takeHealthDamage;
    }

    public virtual void DisplayShieldHP()
    {
        ShieldDisplay.ChangeText("쉴드:" + Shield.ToString());
        HPDisplay.ChangeText("HP:" + HP.ToString());
        StringBuilder extraText = new();
        extraText.AppendLine("A: " + AttackOrderValue.ToString());

        foreach (var state in Condition)
        {
            extraText.AppendLine($"{state.Key}: {state.Value}");
        }

        ExtraDisplay.ChangeText(extraText.ToString());
    }


    // 계산 시 최소값 0을 리턴
    public void UpdateCondition(ECondition stateConditionType, int change)
    {
        if (Condition.TryGetValue(stateConditionType, out int currentValue))
        {
            Condition[stateConditionType] = Math.Max(0, currentValue + change);
        }
        else
        {
            Condition[stateConditionType] = Math.Max(0, change);
        }
        DisplayShieldHP();
    }

    public void SetCondition(ECondition stateConditionType, int value)
    {
        Condition[stateConditionType] = Math.Max(0, value);
        DisplayShieldHP();
    }

    public int GetCondition(ECondition stateConditionType)
    {
        var result = Condition.TryGetValue(stateConditionType, out int value) ? value : 0;
        DisplayShieldHP();
        return result;
    }

    public virtual void Act()
    {

    }

    public virtual void BeforeStage() { }
    public virtual void StartStage() { }
    public virtual void StartTurn()
    {
        StartTurnConditions();

    }

    public virtual void EndTurn() { }
    public virtual void EndStage() { }

    public void StartTurnConditions()
    {
        // 부활
        if (IsAlive == ECharacterState.TempDead && GetCondition(ECondition.CanRebirth) >= 1 && GetCondition(ECondition.RebirthTime) >= 1)
        {
            UpdateCondition(ECondition.RebirthTime, -1);
            if (GetCondition(ECondition.RebirthTime) == 0)
            {
                IsAlive = ECharacterState.Alive;
                HP = 1;
            }
        }

        // 턴 초기화
        SetCondition(ECondition.TakeHP, 0);
        SetCondition(ECondition.TakeShield, 0);
        SetCondition(ECondition.TurnDamageHP, 0);
        SetCondition(ECondition.TurnDamageShield, 0);
    }

    public void EndTurnConditions()
    {
    }
}
