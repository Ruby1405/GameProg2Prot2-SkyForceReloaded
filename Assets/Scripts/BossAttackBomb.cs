using UnityEngine;
using System.Collections.Generic;
public class BossAttackBomb : BossAttack
{
    [Header("Attack Pools")]
    [SerializeField] private string bombPoolName;


    [Header("Attack Settings")]
    [SerializeField] private Vector2 source;
    private bool isAttacking = false;
    private float attackTime = 0f;
    private int attackPhase = 0;
    [SerializeField] private float firingArc = 270f;
    private float minAngle => (-90f - firingArc / 2f) * Mathf.Deg2Rad;
    private float maxAngle => (-90f + firingArc / 2f) * Mathf.Deg2Rad;

    [Header("Gizmos")]
    [SerializeField] private bool showGizmos = true;
    [SerializeField] private Color gizmoColor = Color.red;
    [SerializeField] private bool edit = false;
    public bool EditMode => edit;

    public override void StartAttack()
    {
        isAttacking = true;
        attackPhase = 0;
    }

    void FixedUpdate()
    {
        if (isAttacking)
        {
            if (0f < attackTime)
            {
                attackTime -= Time.fixedDeltaTime;
            }
            else
            {
                switch (attackPhase)
                {
                    case 0:
                        {
                            const int iterations = 8;
                            for (int i = 0; i < iterations; i++)
                            {
                                float angle = (50f + i * (240f / (iterations + 1)) + 90f) * Mathf.Deg2Rad;
                                Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                                SpawnBomb(direction);
                            }
                            attackTime = 0.1f;
                            break;
                        }
                    case 1:
                        {
                            const int iterations = 8;
                            for (int i = 0; i < iterations; i++)
                            {
                                float angle = (55f + i * (240f / (iterations + 1)) + 90f) * Mathf.Deg2Rad;
                                Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                                SpawnBomb(direction);
                            }
                            attackTime = 0.1f;
                            break;
                        }
                    case 2:
                        {
                            const int iterations = 8;
                            for (int i = 0; i < iterations; i++)
                            {
                                float angle = (60f + i * (240f / (iterations + 1)) + 90f) * Mathf.Deg2Rad;
                                Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                                SpawnBomb(direction);
                            }
                            attackTime = 2f;
                            break;
                        }
                    case 3:
                        {
                            const int iterations = 8;
                            for (int i = 0; i < iterations; i++)
                            {
                                float angle = (-50f - i * (240f / (iterations + 1)) + 90f) * Mathf.Deg2Rad;
                                Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                                SpawnBomb(direction);
                            }
                            attackTime = 0.1f;
                            break;
                        }
                    case 4:
                        {
                            const int iterations = 8;
                            for (int i = 0; i < iterations; i++)
                            {
                                float angle = (-55f - i * (240f / (iterations + 1)) + 90f) * Mathf.Deg2Rad;
                                Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                                SpawnBomb(direction);
                            }
                            attackTime = 0.1f;
                            break;
                        }
                    case 5:
                        {
                            const int iterations = 8;
                            for (int i = 0; i < iterations; i++)
                            {
                                float angle = (-60f - i * (240f / (iterations + 1)) + 90f) * Mathf.Deg2Rad;
                                Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                                SpawnBomb(direction);
                            }
                            attackTime = 5f;
                            break;
                        }
                    default:
                        isAttacking = false;
                        OnAttackFinished?.Invoke();
                        break;
                }
                attackPhase++;
            }
        }
    }

    private void SpawnBomb(Vector2 direction)
    {
        GameObject bomb = ObjectPooler.Instance.GetPooledObject(bombPoolName);
        if (bomb != null)
        {
            bomb.transform.position = new Vector3(source.x, 0, source.y);
            bomb.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y), Vector3.up);
            bomb.SetActive(true);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!showGizmos) return;
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(new Vector3(source.x, 0f, source.y), 0.3f);
        Gizmos.DrawLine(
            new Vector3(source.x, 0f, source.y),
            new Vector3(
                source.x + Mathf.Cos(minAngle) * 2f,
                0f,
                source.y + Mathf.Sin(minAngle) * 2f));
        Gizmos.DrawLine(
            new Vector3(source.x, 0f, source.y),
            new Vector3(
                source.x + Mathf.Cos(maxAngle) * 2f,
                0f,
                source.y + Mathf.Sin(maxAngle) * 2f));
    }
}