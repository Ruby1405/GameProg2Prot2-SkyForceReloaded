using System;
using UnityEngine;

public abstract class BossAttack : MonoBehaviour
{
    public static Action OnAttackFinished;
    public abstract void StartAttack();
}