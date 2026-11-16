using UnityEngine;

/// <summary>
/// Tracks player health, temporary damage immunity, and the visual hit response.
/// Publishes health and death events so UI components can react independently.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [SerializeField, Min(1)] private int maxHealth = 3;
    [SerializeField, Min(0f)] private float invulnerabilityDuration = 1f;

    private int currentHealth;
    private float invulnerableUntil;

    private SpriteRenderer visual;
    private Color originalColor;

    public bool IsDead { get; private set; }
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    /// <summary>
    /// Raised after accepted damage with current and maximum health.
    /// Initial health is available through the public properties.
    /// </summary>
    public event System.Action<int, int> HealthChanged;

    /// <summary>
    /// Raised once after death has disabled movement and shooting.
    /// </summary>
    public event System.Action Died;

    private void Awake()
    {
        currentHealth = maxHealth;

        visual = GetComponentInChildren<SpriteRenderer>();

        if (visual != null)
            originalColor = visual.color;
    }

    private void Update()
    {
        if (IsDead || visual == null)
            return;

        Color color = originalColor;

        // Alternate opacity during immunity. Outside that window,
        // the original color and opacity are restored.
        if (Time.time < invulnerableUntil)
        {
            bool dimmed = Mathf.FloorToInt(Time.time * 12f) % 2 == 0;
            color.a = dimmed ? originalColor.a * 0.3f : originalColor.a;
        }

        visual.color = color;
    }

    /// <summary>
    /// Accepts positive damage only while alive and outside the immunity window.
    /// Each accepted hit starts a new immunity window and notifies listeners.
    /// </summary>
    public void TakeDamage(int damage)
    {
        if (IsDead || damage <= 0 || Time.time < invulnerableUntil)
            return;

        currentHealth = Mathf.Max(0, currentHealth - damage);
        invulnerableUntil = Time.time + invulnerabilityDuration;

        // Report the final health change, including zero, before signaling death.
        HealthChanged?.Invoke(currentHealth, maxHealth);

        Debug.Log($"Frieren: {currentHealth}/{maxHealth} HP", this);

        if (currentHealth == 0)
            Die();
    }

    private void Die()
    {
        // Block further damage before notifying any external listeners.
        IsDead = true;

        PlayerMovement movement = GetComponent<PlayerMovement>();
        PlayerShooting shooting = GetComponent<PlayerShooting>();

        if (movement != null)
            movement.enabled = false;

        if (shooting != null)
            shooting.enabled = false;

        Rigidbody2D body = GetComponent<Rigidbody2D>();

        if (body != null)
            body.linearVelocity = Vector2.zero;

        if (visual != null)
            visual.color = Color.red;

        Debug.Log("Game Over — Frieren was defeated.", this);

        // Screen presentation belongs to listeners, not this health component.
        Died?.Invoke();
    }
}