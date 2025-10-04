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
    private void PoolReset()
    {
        currentHealth = maxHealth;
        gameObject.SetActive(false);
    }
    public void Hit(GameObject projectile)
    {
        Projectile proj;
        if (!projectile.TryGetComponent<Projectile>(out proj))
        {
            Debug.LogWarning("Projectile does not have a Projectile component.");
            return;
        }
        Damage(proj.Damage);
        projectile.SetActive(false);
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