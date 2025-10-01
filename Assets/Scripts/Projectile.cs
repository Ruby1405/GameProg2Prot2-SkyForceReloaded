using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnEnable()
    {
        rb.linearVelocity = transform.up * speed;
    }

    void Update()
    {

    }
}
