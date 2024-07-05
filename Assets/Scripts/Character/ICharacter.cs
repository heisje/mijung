using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    int Health { get; set; }
    SortedDictionary<StateConditionType, int> StateCondition { get; set; }
    void TakeDamage(int damage);
}