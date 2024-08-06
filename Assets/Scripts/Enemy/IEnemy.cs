using System;
using UnityEngine;

// 적의 인터페이스 정의
public interface IEnemy
{
    CharacterStateType EnemyState { get; set; }
    int Health { get; set; }
    int Damage { get; }
    int Attack();
    void TakeDamage(int damage);
}