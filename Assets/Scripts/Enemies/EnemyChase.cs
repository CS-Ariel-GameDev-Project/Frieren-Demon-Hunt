using UnityEngine;

/// <summary>
/// Moves an enemy directly toward its target using Rigidbody2D velocity.
/// Stops within the configured distance. Obstacles block movement;
/// this component does not calculate paths around them.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChase : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Min(0f)] private float moveSpeed = 2f;
    [SerializeField, Min(0f)] private float stoppingDistance = 0.6f;

    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            body.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = (Vector2)target.position - body.position;

        // Compare squared distances to avoid calculating a square root
        // on every physics update.
        if (direction.sqrMagnitude <= stoppingDistance * stoppingDistance)
        {
            body.linearVelocity = Vector2.zero;
            return;
        }

        // Normalization keeps movement speed independent of target distance.
        body.linearVelocity = direction.normalized * moveSpeed;
    }

    private void OnDisable()
    {
        // Disabling pursuit must also clear the last assigned velocity.
        if (body != null)
            body.linearVelocity = Vector2.zero;
    }

    /// <summary>
    /// Assigns the target, allowing WaveSpawner to connect a spawned enemy
    /// to the player without storing a scene reference in the prefab.
    /// </summary>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}