using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ICharacter
{
    public int Health { get; set; }
    public SortedDictionary<StateConditionType, int> StateCondition { get; set; }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
    }

}
