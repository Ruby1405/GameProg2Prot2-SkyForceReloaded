using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private string poolName = "PlayerProjectiles";
    [SerializeField] private float cooldown = 0.5f;
    private float lastSpawnTime = 0f;
    private bool firing = false;
    [SerializeField] private UnityEvent onFire;

    void FixedUpdate()
    {
        if (lastSpawnTime > 0f) lastSpawnTime -= Time.fixedDeltaTime;

        if (!firing) return;

        if (lastSpawnTime > 0f) return;
        lastSpawnTime = cooldown;

        GameObject projectile = ObjectPooler.Instance.GetPooledObject(poolName);
        if (projectile != null)
        {
            projectile.transform.position = transform.position;
            projectile.SetActive(true);
            onFire?.Invoke();
        }
    }
    public void UpdateFiringState(InputAction.CallbackContext context)
    {
        firing = context.performed;
    }
}
