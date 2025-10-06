using UnityEngine;
public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;
    void Awake()
    {
        currentHealth = maxHealth;
    }
    private void Die()
    {
        EnemyEventManager.TriggerEnemyDestroyed(new EnemyDestroyedEventArgs
        {
            position = new Vector2(transform.position.x, transform.position.z),
            score = scoreValue
        });
        PoolReset();
    }
    public void PoolReset()
    {
        currentHealth = maxHealth;
        gameObject.SetActive(false);
    }
    public void Hit(GameObject projectile)
    {
        if (projectile.TryGetComponent<Projectile>(out var proj))
        {
            Damage(proj.Damage);
            projectile.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Projectile does not have a Projectile component.");
            return;
        }
    }
    private void Damage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
}