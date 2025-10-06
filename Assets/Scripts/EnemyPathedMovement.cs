using UnityEngine;

[System.Serializable]
public struct EnemyPathedSpawnVariables : EnemySpawnVariables
{
    [SerializeField] public CatmullRomSpline newSpline;
    [SerializeField] public float speed;
    [SerializeField] public float startT;
    [SerializeField] public float tangentPrediction;
    public EnemyPathedSpawnVariables(
        CatmullRomSpline newSpline,
        float speed,
        float startT = 0f,
        float tangentPrediction = 0.01f)
    {
        this.newSpline = newSpline;
        this.speed = speed;
        this.startT = startT;
        this.tangentPrediction = tangentPrediction;
    }
}

[RequireComponent(typeof(EnemyBehavior))]
public class EnemyPathedMovement : MonoBehaviour
{
    [SerializeField] private CatmullRomSpline path;
    [SerializeField] private float t = 0f;
    [SerializeField] private float speed = 1f;
    [SerializeField][Range(0.0001f, 0.1f)] private float tangentPrediction = 0.01f;

    public void Initialize(EnemyPathedSpawnVariables vars)
    {
        path = vars.newSpline;
        this.speed = vars.speed;
        t = vars.startT;
        this.tangentPrediction = vars.tangentPrediction;
    }

    void FixedUpdate()
    {
        if (path == null) return;
        t += speed * Time.fixedDeltaTime;
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
