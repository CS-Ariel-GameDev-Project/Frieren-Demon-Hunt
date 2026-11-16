using UnityEngine;

/// <summary>
/// Tracks enemy health and triggers a death effect when health reaches zero.
/// Player health is handled separately by PlayerHealth.
/// </summary>
public class Health : MonoBehaviour
{
    [SerializeField, Min(1)] private int maxHealth = 3;

    private int currentHealth;
    private bool isDead;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Applies positive damage while the enemy is alive.
    /// The world-space hit direction determines death knockback;
    /// its default value of zero produces no directional knockback.
    /// </summary>
    public void TakeDamage(int damage, Vector2 hitDirection = default)
    {
        if (isDead || damage <= 0)
            return;

        currentHealth = Mathf.Max(0, currentHealth - damage);

        Debug.Log(
            $"{gameObject.name}: {currentHealth}/{maxHealth} HP",
            this);

        if (currentHealth > 0)
            return;

        // Prevent further damage from triggering the death effect again.
        isDead = true;

        EnemyDeathEffect deathEffect =
            GetComponent<EnemyDeathEffect>();

        // Let the effect handle delayed removal, or remove immediately
        // if this enemy has no death effect component.
        if (deathEffect != null)
            deathEffect.Play(hitDirection);
        else
            Destroy(gameObject);
    }
}