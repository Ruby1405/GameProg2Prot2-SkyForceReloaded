using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BossBehaviour : EnemyBehavior
{
    private Rigidbody rb;
    private List<BossAttack> attacks;
    [SerializeField] private float startGracePeriod = 2f;
    private bool headingToA = true;
    private float gracePeriod;

    [Header("Movement")]
    [SerializeField] private Vector2 patrolPointA;
    [SerializeField] private Vector2 patrolPointB;
    [SerializeField] private float pointMargin = 1f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private Vector2 lookAtPoint = Vector2.zero;

    void Start()
    {
        BossAttack.OnAttackFinished += StartAttackCycle;
        attacks = new List<BossAttack>();
        foreach (var attack in GetComponents<BossAttack>())
        {
            if (attack.isActiveAndEnabled) attacks.Add(attack);
        }
        gracePeriod = startGracePeriod;

        rb = GetComponent<Rigidbody>();
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
        Move();

        if (gracePeriod > 0)
        {
            gracePeriod -= Time.fixedDeltaTime;
            if (gracePeriod <= 0f)
            {
                StartAttackCycle();
            }
        }
    }

    private void Move()
    {
        Vector3 targetPoint = headingToA ? new Vector3(patrolPointA.x, 0, patrolPointA.y) : new Vector3(patrolPointB.x, 0, patrolPointB.y);

        rb.AddForce((targetPoint - transform.position).normalized * acceleration * Time.fixedDeltaTime, ForceMode.Acceleration);

        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;

        if ((transform.position - targetPoint).magnitude < pointMargin)
            headingToA = !headingToA;

        // Look towards point
        Vector3 lookDirection = (transform.position - new Vector3(lookAtPoint.x, 0, lookAtPoint.y)).normalized;
        if (lookDirection != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);

        // Compensate weird drift?
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(new Vector3(patrolPointA.x, 0, patrolPointA.y), 0.2f);
        Gizmos.DrawWireSphere(new Vector3(patrolPointA.x, 0, patrolPointA.y), pointMargin);
        Gizmos.DrawSphere(new Vector3(patrolPointB.x, 0, patrolPointB.y), 0.2f);
        Gizmos.DrawWireSphere(new Vector3(patrolPointB.x, 0, patrolPointB.y), pointMargin);
        Gizmos.DrawLine(new Vector3(patrolPointA.x, 0, patrolPointA.y), new Vector3(patrolPointB.x, 0, patrolPointB.y));
    }
}
