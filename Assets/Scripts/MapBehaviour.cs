using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MapBehaviour : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.1f;
    [SerializeField] private GameVariables gameVariables;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = new Vector3(0, 0, -scrollSpeed);

        if (gameVariables != null)
        {
            gameVariables.OnLivesChanged += CheckIfDead;
        }
    }

    private void OnDestroy()
    {
        if (gameVariables != null)
        {
            gameVariables.OnLivesChanged -= CheckIfDead;
        }
    }

    private void CheckIfDead(int lives)
    {
        if (lives <= 0)
        {
            StopScrolling();
        }
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
