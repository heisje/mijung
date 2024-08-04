using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int Health = 0;
    public int Shield = 0;
    public SortedDictionary<StateConditionType, int> StateCondition = new();
    public int AttackOrderValue;

    public virtual void TakeDamage(int damage)
    {
        AttackOrderValue = 0;

        if (Shield <= damage)
        {
            damage -= Shield;
            Shield = 0;
            AttackOrderValue = damage;
            Health -= damage;
        }
        else if (Shield > damage)
        {
            Shield -= damage;
        }
    }


    // 계산 시 최소값 0을 리턴
    public void ChangeCondition(StateConditionType stateConditionType, int i)
    {
        if (!StateCondition.ContainsKey(stateConditionType))
        {
            StateCondition[stateConditionType] = 0;
        }
        StateCondition[stateConditionType] += i;
        if (StateCondition[stateConditionType] <= 0) StateCondition[stateConditionType] = 0;
    }

    public int GetStateCondition(StateConditionType stateConditionType)
    {
        if (!StateCondition.ContainsKey(stateConditionType))
        {
            StateCondition[stateConditionType] = 0;
        }
        if (StateCondition[stateConditionType] <= 0) StateCondition[stateConditionType] = 0;
        return StateCondition[stateConditionType];
    }

    public virtual void Act()
    {

    }
}

