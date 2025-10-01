using UnityEngine;

[System.Serializable]
public class ObjectPool
{
    [SerializeField] private string poolName;
    public string PoolName => poolName;
    [SerializeField] private GameObject pooledPrefab;
    [SerializeField] private int poolSize = 40;
    private GameObject[] pool;

    public void InitializePool(Transform parent)
    {
        pool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Object.Instantiate(pooledPrefab, parent);
            pool[i].SetActive(false);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
        Debug.LogWarning($"Pool: {poolName} is dry!");
        return null;
    }
}
