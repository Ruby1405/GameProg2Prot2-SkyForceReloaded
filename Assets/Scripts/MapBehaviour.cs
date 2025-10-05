using UnityEngine;

public class MapBehaviour : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.1f;

    void Awake()
    {
        GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, -scrollSpeed);
    }
}
