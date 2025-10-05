using System;
using UnityEngine;

[Serializable]
public class BossAttack : MonoBehaviour
{
    public static Action OnAttackFinished;
    public virtual void StartAttack()
    {

    }
}