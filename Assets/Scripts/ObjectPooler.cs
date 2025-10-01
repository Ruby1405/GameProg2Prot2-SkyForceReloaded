using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    [SerializeField] private List<ObjectPool> pools;
    private Dictionary<string, int> poolIndexMap;

    void Awake()
    {
        // Singleton instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        poolIndexMap = new Dictionary<string, int>();
        foreach (var pool in pools)
        {
            pool.InitializePool(transform);
            poolIndexMap[pool.PoolName] = pools.IndexOf(pool);
        }
    }

    public GameObject GetPooledObject(string poolName = "", int poolIndex = -1)
    {
        if (poolIndex == -1 && poolName == "")
        {
            Debug.LogError("No pool name or index provided!");
            return null;
        }

        int index = poolIndex;
        if (poolName != "")
        {
            if (poolIndexMap.ContainsKey(poolName))
                index = poolIndexMap[poolName];
            else
            {
                Debug.LogError($"No pool found with name: {poolName}");
                return null;
            }
        }
        if (index < 0 || index >= pools.Count)
        {
            Debug.LogError($"Pool index {index} is out of range!");
            return null;
        }
        return pools[index].GetPooledObject();
    }
}
