using UnityEngine;

public class LevelBounds : MonoBehaviour
{
    public static LevelBounds Instance;
    private BoxCollider boxCollider;
    [SerializeField] private Color gizmoColor = Color.blue;

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

        boxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            other.gameObject.SetActive(false);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider>();

        Gizmos.DrawWireCube(transform.position + boxCollider.center, boxCollider.size);
    }
}
