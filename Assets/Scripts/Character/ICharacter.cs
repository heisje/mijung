using System.Collections.Generic;
using UnityEngine;

// 체력을 가지고 있는 오브젝트, 플레이어의 캐릭터만 가진 것이 아님
// 비활성됨
public interface ICharacter
{
    public int Health { get; set; }
    public SortedDictionary<StateConditionType, int> StateCondition { get; set; }
    public void TakeDamage(int damage);
}