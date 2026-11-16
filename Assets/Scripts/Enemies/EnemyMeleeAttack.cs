using UnityEngine;

/// <summary>
/// Attempts melee damage at timed intervals while the player is in range.
/// EnemyChase handles movement; PlayerHealth handles damage immunity.
/// </summary>
public class EnemyMeleeAttack : MonoBehaviour
{
    [SerializeField] private PlayerHealth target;

    [SerializeField, Min(0f)] private float attackRange = 0.9f;
    [SerializeField, Min(0.1f)] private float attackCooldown = 1.2f;
    [SerializeField, Min(1)] private int damage = 1;

    private float nextAttackTime;

    private void OnEnable()
    {
        // Delay the first attack whenever this component is enabled.
        nextAttackTime = Time.time + attackCooldown;
    }

    private void Update()
    {
        if (target == null || target.IsDead)
            return;

        if (Time.time < nextAttackTime)
            return;

        Vector2 difference =
            (Vector2)target.transform.position -
            (Vector2)transform.position;

        // Check range between object origins using squared distances
        // to avoid calculating a square root each frame.
        if (difference.sqrMagnitude > attackRange * attackRange)
            return;

        // PlayerHealth may reject damage during immunity.
        // The attack still consumes its cooldown.
        target.TakeDamage(damage);
        nextAttackTime = Time.time + attackCooldown;
    }

    /// <summary>
    /// Assigns the player's health component after WaveSpawner creates the enemy,
    /// avoiding a scene reference in the enemy prefab.
    /// </summary>
    public void SetTarget(PlayerHealth newTarget)
    {
        target = newTarget;
    }
}