using UnityEngine;

/// <summary>
/// Applies spin and directional knockback to a defeated enemy,
/// then removes it after a short delay.
/// Disables combat and collisions while the death effect plays.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyDeathEffect : MonoBehaviour
{
    [SerializeField, Min(0.1f)] private float destroyDelay = 0.6f;
    [SerializeField, Min(0f)] private float spinSpeed = 720f;
    [SerializeField, Min(0f)] private float knockbackImpulse = 3f;

    private const float NegativeSpinProbability = 0.5f;

    /// <summary>
    /// Starts the death effect using the incoming hit's world-space direction.
    /// Called by Health when the enemy receives lethal damage.
    /// </summary>
    public void Play(Vector2 hitDirection)
    {
        // Prevent the dying enemy from dealing further damage.
        EnemyMeleeAttack attack = GetComponent<EnemyMeleeAttack>();

        if (attack != null)
            attack.enabled = false;

        // Stop pursuit before applying the physics impulses.
        EnemyChase chase = GetComponent<EnemyChase>();

        if (chase != null)
            chase.enabled = false;

        // Let the defeated enemy move freely without blocking other objects.
        foreach (Collider2D enemyCollider
                 in GetComponentsInChildren<Collider2D>())
        {
            enemyCollider.enabled = false;
        }

        Rigidbody2D body = GetComponent<Rigidbody2D>();

        // Clear existing motion so the effect starts from a consistent state.
        body.linearVelocity = Vector2.zero;
        body.angularVelocity = 0f;

        // Release the rotation constraint used during normal movement,
        // preserving any other constraints.
        body.constraints &= ~RigidbodyConstraints2D.FreezeRotation;

        // Randomize spin direction and convert degrees per second into an
        // angular impulse using inertia, keeping initial spin speed consistent.
        float direction = Random.value < NegativeSpinProbability ? -1f : 1f;
        float torqueImpulse =
            direction * spinSpeed * Mathf.Deg2Rad * body.inertia;

        body.AddTorque(torqueImpulse, ForceMode2D.Impulse);

        // Push away along the incoming hit direction while the body spins.
        body.AddForce(hitDirection.normalized * knockbackImpulse,
            ForceMode2D.Impulse);

        Destroy(gameObject, destroyDelay);
    }
}