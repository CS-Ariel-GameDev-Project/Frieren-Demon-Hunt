using UnityEngine;

/// <summary>
/// Moves a player projectile and resolves its first collision.
/// Damages objects with Health and disappears on impact or after its lifetime.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class MagicProjectile : MonoBehaviour
{
    [SerializeField, Min(0.1f)] private float speed = 12f;
    [SerializeField, Min(0.1f)] private float lifetime = 3f;
    [SerializeField, Min(1)] private int damage = 1;

    private Rigidbody2D body;
    private bool hasHit;
    private Vector2 flightDirection;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();

        // Remove missed shots so projectiles cannot remain indefinitely.
        Destroy(gameObject, lifetime);
    }

    /// <summary>
    /// Launches the projectile in a world-space direction.
    /// Called immediately after creation, with the shooter as the owner.
    /// </summary>
    public void Launch(Vector2 direction, GameObject owner)
    {
        Collider2D[] projectileColliders =
            GetComponentsInChildren<Collider2D>();

        Collider2D[] ownerColliders =
            owner.GetComponentsInChildren<Collider2D>();

        // Ignore every collider pair so the projectile cannot hit its shooter,
        // including colliders attached to child objects.
        foreach (Collider2D projectileCollider in projectileColliders)
        {
            foreach (Collider2D ownerCollider in ownerColliders)
            {
                Physics2D.IgnoreCollision(
                    projectileCollider, ownerCollider);
            }
        }

        // Preserve the incoming direction for knockback, since the physics
        // collision response may change the projectile's velocity.
        flightDirection = direction.normalized;
        body.linearVelocity = flightDirection * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destruction is deferred; prevent additional callbacks from dealing damage.
        if (hasHit)
            return;

        hasHit = true;

        // The hit collider may belong to a child of the object holding Health.
        Health health =
            collision.collider.GetComponentInParent<Health>();

        if (health != null)
            health.TakeDamage(damage, flightDirection);

        Destroy(gameObject);
    }
}