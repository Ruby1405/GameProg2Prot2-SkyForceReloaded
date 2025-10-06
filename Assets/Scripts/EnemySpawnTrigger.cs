using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    [Header("Prefab options")]
    [SerializeField] private string poolName = "";
    [SerializeField] private GameObject enemyPrefab = null;
    [Header("Spawn options")]
    [SerializeField] private CatmullRomSpline path = null;
    private Color gizmoColor = Color.black;
    void OnValidate()
    {
        if (path != null)
            gizmoColor = path.SplineColor;
        else
            gizmoColor = Color.black;
    }
    void OnTriggerEnter(Collider colider)
    {
        if (poolName != "")
        {
            GameObject enemy = ObjectPooler.Instance.GetPooledObject(poolName);

            if (enemy.TryGetComponent<EnemyPathedMovement>(out var enemyMovement))
                enemyMovement.Initialize(path);

            enemy.SetActive(true);
            enemy.transform.position = transform.position;
            Destroy(gameObject);
        }
        else if (enemyPrefab != null)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = transform.position;
            enemy.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No pool name or enemy prefab set for EnemySpawnTrigger.");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
