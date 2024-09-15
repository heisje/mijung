using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Character : MonoBehaviour, ILifeCycle
{
    public int HP = 0;
    public int Shield = 0;
    public Dictionary<EStateCondition, int> StateCondition = new();

    // UI
    public ChangeTMP ShieldDisplay;
    public ChangeTMP HPDisplay;
    public ChangeTMP ExtraDisplay;

    // 상태
    public ECharacterState State;

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
            Shield = 0;
            takeHealthDamage += damage;
            HP -= damage;
        }
        else if (Shield > damage)
        {
            Shield -= damage;
        }

        if (takeHealthDamage >= 10)
        {
            UpdateCondition(EStateCondition.FellDown, 1);
        }

        if (HP <= 0)
        {
            State = ECharacterState.Dead;
            if (GetStateCondition(EStateCondition.CanRebirth) >= 1)
            {
                State = ECharacterState.TempDead;
                SetCondition(EStateCondition.Rebirth, 3);
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

        foreach (var state in StateCondition)
        {
            extraText.AppendLine($"{state.Key}: {state.Value}");
        }

        ExtraDisplay.ChangeText(extraText.ToString());
    }


    // 계산 시 최소값 0을 리턴
    public void UpdateCondition(EStateCondition stateConditionType, int change)
    {
        if (StateCondition.TryGetValue(stateConditionType, out int currentValue))
        {
            StateCondition[stateConditionType] = Math.Max(0, currentValue + change);
        }
        else
        {
            StateCondition[stateConditionType] = Math.Max(0, change);
        }
    }

    public void SetCondition(EStateCondition stateConditionType, int value)
    {
        StateCondition[stateConditionType] = Math.Max(0, value);
    }

    public int GetStateCondition(EStateCondition stateConditionType)
    {
        return StateCondition.TryGetValue(stateConditionType, out int value) ? value : 0;
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
        if (State == ECharacterState.TempDead && GetStateCondition(EStateCondition.CanRebirth) >= 1 && GetStateCondition(EStateCondition.Rebirth) >= 1)
        {
            UpdateCondition(EStateCondition.Rebirth, -1);
            if (GetStateCondition(EStateCondition.Rebirth) == 0)
            {
                State = ECharacterState.Alive;
                HP = 1;
            }
        }
    }

    public void EndTurnConditions()
    {
    }
}

