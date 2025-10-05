using UnityEngine;

public class LevelBounds : MonoBehaviour
{
    public static LevelBounds Instance;

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
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
