using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MapBehaviour : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.1f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = new Vector3(0, 0, -scrollSpeed);
    }

    public void StopScrolling()
    {
        rb.linearDamping = 3f;
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //   Debug.Log("OnCollisionEnter");
    // }
}
