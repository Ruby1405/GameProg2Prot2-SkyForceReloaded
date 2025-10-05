using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    private List<BossAttack> attacks;
    [SerializeField] private float startGracePeriod = 2f;
    private float gracePeriod;

    void Awake()
    {
        BossAttack.OnAttackFinished += StartAttackCycle;
        attacks = new List<BossAttack>(GetComponents<BossAttack>());
        gracePeriod = startGracePeriod;
    }

    public void StartAttackCycle()
    {
        if (attacks.Count > 0)
        {
            attacks[Random.Range(0, attacks.Count)].StartAttack();
        }
    }

    void FixedUpdate()
    {
        if (gracePeriod > 0)
        {
            gracePeriod -= Time.fixedDeltaTime;
            if (gracePeriod <= 0f)
            {
                StartAttackCycle();
            }
        }
    }
}
