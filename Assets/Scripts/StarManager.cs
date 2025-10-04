using System;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public static StarManager Instance;
    [SerializeField] private string poolName = "Stars";
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        EnemyEventManager.OnEnemyDestroyed += DestroyedEnemy;
    }
    private void DestroyedEnemy(EnemyDestroyedEventArgs args)
    {
        SpawnStars(args.position, args.score);
    }
    private void SpawnStars(Vector2 position, int amount)
    {
        // Divide score 
        int size9s = Math.Max((amount - 1) / 9, 0);
        amount -= size9s * 9;
        int size3s = Math.Max((amount - 1) / 3, 0);
        amount -= size3s * 3;
        int size1s = amount;

        for (int i = 0; i < size9s; i++)
        {
            SpawnStar(position, 9);
        }
        for (int i = 0; i < size3s; i++)
        {
            SpawnStar(position, 3);
        }
        for (int i = 0; i < size1s; i++)
        {
            SpawnStar(position, 1);
        }
    }
    private void SpawnStar(Vector2 position, int size)
    {
        GameObject star = ObjectPooler.Instance.GetPooledObject(poolName);
        if (star != null)
        {
            star.SetActive(true);
            star.transform.position = new Vector3(position.x, 0, position.y);
            star.GetComponent<Rigidbody>().AddForce(new Vector3(
                UnityEngine.Random.Range(-1f, 1f),
                0,
                UnityEngine.Random.Range(-1f, 1f)
            ).normalized * 5f, ForceMode.Impulse);
            star.GetComponent<StarBehaviour>().SetSize(size);
        }
    }
}
