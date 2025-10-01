using UnityEngine;

public class ProjectileBounds : MonoBehaviour
{
    public static ProjectileBounds Instance;

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
