using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private float cooldown = 0.5f;
    private float lastSpawnTime = 0f;
    private bool firing = false;

    void FixedUpdate()
    {
        if (lastSpawnTime > 0f) lastSpawnTime -= Time.fixedDeltaTime;

        if (!firing) return;

        if (lastSpawnTime > 0f) return;
        lastSpawnTime = cooldown;

        GameObject projectile = ObjectPooler.Instance.GetPooledObject("PlayerProjectiles");
        if (projectile != null)
        {
            projectile.transform.position = transform.position;
            projectile.SetActive(true);
        }
    }
    public void UpdateFiringState(InputAction.CallbackContext context)
    {
        firing = context.performed;
    }
}
