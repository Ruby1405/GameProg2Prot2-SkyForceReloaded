using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    [Header("Prefab options")]
    [SerializeField] private string poolName = "";
    [SerializeField] private GameObject enemyPrefab = null;
    [Header("Spawn options")]
    [SerializeField] private EnemyPathedSpawnVariables spawnVariables = new EnemyPathedSpawnVariables();
    void OnCollisionEnter(Collision collision)
    {
        if (poolName != "")
        {
            GameObject enemy = ObjectPooler.Instance.GetPooledObject(poolName);
            if (!enemy.TryGetComponent<EnemyPathedMovement>(out var enemyMovement))
            {
                Debug.LogWarning("Pooled object does not have an EnemyPathedMovement component.");
                return;
            }
            else
            {
                enemy.SetActive(true);
                enemy.transform.position = transform.position;
                enemyMovement.Initialize((EnemyPathedSpawnVariables)spawnVariables);
            }
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
}
