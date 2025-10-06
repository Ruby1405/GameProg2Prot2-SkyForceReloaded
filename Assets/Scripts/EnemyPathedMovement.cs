using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
public class EnemyPathedMovement : MonoBehaviour
{
    [SerializeField] private CatmullRomSpline path;
    private float t = 0f;
    [SerializeField] private float speed = 1f;
    [SerializeField][Range(0.0001f, 0.1f)] private float tangentPrediction = 0.01f;

    public void Initialize(CatmullRomSpline path)
    {
        this.path = path;
        t = 0f;
    }

    void FixedUpdate()
    {
        if (path == null) return;
        t += speed * path.Speed * Time.fixedDeltaTime;
        if (t > 1f)
        {
            gameObject.GetComponent<EnemyBehavior>().PoolReset();
        }
        transform.position = path.GetWorldPoint(t);

        // rotate to face the direction of movement
        Vector3 tangent = (path.GetWorldPoint(Mathf.Min(t + tangentPrediction, 1f)) - transform.position).normalized;
        if (tangent != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(tangent);
    }
}
