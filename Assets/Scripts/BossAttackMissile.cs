using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(BossAttackMissile))]
public class BossAttackMissileEditor : Editor
{
    public void OnSceneGUI()
    {
        BossAttackMissile attack = (BossAttackMissile)target;
        if (!attack.EditMode) return;
        List<BossAttackMissile.MissilePath> paths = attack.Paths;

        if (paths.Count == 0) return;

        EditorGUI.BeginChangeCheck();
        for (int i = 0; i < paths.Count; i++)
        {
            Vector3 worldStart = new Vector3(paths[i].start.x, 0, paths[i].start.y);
            Vector3 newWorldStart = Handles.PositionHandle(worldStart, Quaternion.identity);
            if (worldStart != newWorldStart)
            {
                paths[i] = new BossAttackMissile.MissilePath
                {
                    start = new Vector2(newWorldStart.x, newWorldStart.z),
                    direction = paths[i].direction
                };
            }

            Vector3 worldEnd = worldStart + new Vector3(paths[i].direction.x, 0, paths[i].direction.y).normalized * attack.DirectionLength;
            Vector3 newWorldEnd = Handles.PositionHandle(worldEnd, Quaternion.identity);
            if (worldEnd != newWorldEnd)
            {
                paths[i] = new BossAttackMissile.MissilePath
                {
                    start = paths[i].start,
                    direction = new Vector2(newWorldEnd.x - worldStart.x, newWorldEnd.z - worldStart.z)
                };
            }
        }
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(attack, "Move Missile Path");
            attack.SetPaths(paths);
            EditorUtility.SetDirty(attack);
        }
    }
}
#endif

public class BossAttackMissile : BossAttack
{
    [Header("Attack Pools")]
    [SerializeField] private string missilePoolName;
    [SerializeField] private string warningPoolName;
    private List<GameObject> warnings = new List<GameObject>();


    [Header("Attack Settings")]
    [SerializeField] private float warningLineLength = 100f;
    [SerializeField] private float warningTime = 1.5f;
    [SerializeField] private float gracePeriod = 0.5f;
    private float attackTime = 0f;
    private bool isAttacking = false;


    [Header("Gizmos")]
    [SerializeField] private bool showGizmos = true;
    [SerializeField] private Color gizmoColor = Color.red;
    [SerializeField] private float directionLength = 3f;
    public float DirectionLength => directionLength;
    [SerializeField] private bool edit = false;
    public bool EditMode => edit;

    
    [System.Serializable]
    public struct MissilePath
    {
        public Vector2 start;
        public Vector2 direction;
    }
    [SerializeField] private List<MissilePath> paths;
    public List<MissilePath> Paths => paths;

    void OnValidate()
    {
        // normalize directions
        for (int i = 0; i < paths.Count; i++)
        {
            paths[i] = new MissilePath
            {
                start = paths[i].start,
                direction = paths[i].direction.normalized
            };
        }
    }
    public override void StartAttack()
    {
        attackTime = warningTime;
        isAttacking = true;

        // show warnings
        foreach (var path in paths)
        {
            GameObject warning = ObjectPooler.Instance.GetPooledObject(warningPoolName);
            if (warning != null)
            {
                if (warning.TryGetComponent<LineRenderer>(out var lr))
                {
                    lr.positionCount = 2;
                    lr.SetPosition(0, new Vector3(path.start.x, 0.1f, path.start.y));
                    lr.SetPosition(1, new Vector3(path.start.x + path.direction.x * warningLineLength, 0.1f, path.start.y + path.direction.y * warningLineLength));
                }
                else
                {
                    Debug.LogError($"BossAttackMissile: Warning object from pool '{warningPoolName}' does not have a LineRenderer component");
                    continue;
                }
                warning.SetActive(true);
                warnings.Add(warning);
            }
        }
    }
    public void SetPaths(List<MissilePath> newPaths)
    {
        paths = newPaths;
    }
    void FixedUpdate()
    {
        if (isAttacking)
        {
            attackTime -= Time.fixedDeltaTime;
            if (attackTime <= 0f)
            {
                isAttacking = false;
                attackTime = gracePeriod;

                // hide warnings
                foreach (var warning in warnings)
                {
                    warning.SetActive(false);
                }
                warnings.Clear();

                // launch missiles
                foreach (MissilePath path in paths)
                {
                    SpawnMissile(path);
                }
            }
        }
        else if (attackTime > 0f)
        {
            attackTime -= Time.fixedDeltaTime;
            if (attackTime <= 0f)
            {
                OnAttackFinished?.Invoke();
            }
        }
    }

    private void SpawnMissile(MissilePath path)
    {
        GameObject missile = ObjectPooler.Instance.GetPooledObject(missilePoolName);
        if (missile != null)
        {
            missile.transform.position = new Vector3(path.start.x, 0, path.start.y);
            missile.transform.rotation = Quaternion.LookRotation(new Vector3(path.direction.x, 0, path.direction.y), Vector3.up);
            missile.SetActive(true);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (showGizmos)
        {
            Gizmos.color = gizmoColor;
            foreach (var path in paths)
            {
                Vector3 start = new Vector3(path.start.x, 0, path.start.y);
                Vector3 end = start + new Vector3(path.direction.x, 0, path.direction.y).normalized * directionLength;
                Gizmos.DrawLine(start, end);
                Gizmos.DrawSphere(start, 0.2f);
            }
        }
    }
}