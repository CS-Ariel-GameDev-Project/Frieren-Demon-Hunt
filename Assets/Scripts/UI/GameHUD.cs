using UnityEngine;

/// <summary>
/// Displays player health and wave progress by listening to gameplay events.
/// Gameplay components do not need references to the HUD.
/// </summary>
public class GameHUD : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private WaveSpawner waveSpawner;

    [SerializeField] private TMPro.TMP_Text healthText;
    [SerializeField] private TMPro.TMP_Text waveText;

    private void OnEnable()
    {
        if (playerHealth != null)
            playerHealth.HealthChanged += UpdateHealth;

        if (waveSpawner != null)
            waveSpawner.WaveChanged += UpdateWave;

        // Read current values in case events occurred while the HUD was disabled.
        RefreshDisplay();
    }

    private void Start()
    {
        // All Awake methods have finished, so initial health is ready.
        RefreshDisplay();
    }

    private void OnDisable()
    {
        // Remove listeners when disabled to prevent duplicate subscriptions
        // when the HUD is enabled again.
        if (playerHealth != null)
            playerHealth.HealthChanged -= UpdateHealth;

        if (waveSpawner != null)
            waveSpawner.WaveChanged -= UpdateWave;
    }

    /// <summary>
    /// Synchronizes the display with current gameplay values without requiring
    /// a new health or wave event.
    /// </summary>
    private void RefreshDisplay()
    {
        if (playerHealth != null)
        {
            UpdateHealth(
                playerHealth.CurrentHealth,
                playerHealth.MaxHealth);
        }

        if (waveSpawner != null)
        {
            UpdateWave(
                waveSpawner.CurrentWave,
                waveSpawner.TotalWaves);
        }
    }

    private void UpdateHealth(int current, int maximum)
    {
        if (healthText != null)
            healthText.text = $"HP: {current} / {maximum}";
    }

    private void UpdateWave(int current, int total)
    {
        if (waveText != null)
            waveText.text = $"Wave: {current} / {total}";
    }
}