using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    [SerializeField] private GameObject pooledPrefab;
    [SerializeField] private int poolSize = 40;
    private GameObject[] pool;

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
        pool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            pool[i] = Instantiate(pooledPrefab);
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
        Debug.LogWarning("Your pool is dry!");
        return null;
    }
}
