using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Displays the start and end panels and pauses gameplay while they are open.
/// Listens for death and victory events without requiring gameplay code to
/// reference the UI. Keep this component on the Canvas that remains active.
/// </summary>
public class GameScreens : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private WaveSpawner waveSpawner;

    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TMPro.TMP_Text endTitle;

    private bool hasStarted;
    private bool hasEnded;

    private void OnEnable()
    {
        if (playerHealth != null)
            playerHealth.Died += ShowDefeat;

        if (waveSpawner != null)
            waveSpawner.Won += ShowVictory;
    }

    private void Start()
    {
        startPanel.SetActive(true);
        endPanel.SetActive(false);

        // Pause physics and scaled waits, including the delay before the first wave.
        // Disable player input separately because Update still runs while paused.
        SetPlayerControls(false);
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        // Pair each subscription with cleanup when this component is disabled.
        if (playerHealth != null)
            playerHealth.Died -= ShowDefeat;

        if (waveSpawner != null)
            waveSpawner.Won -= ShowVictory;
    }

    /// <summary>
    /// Start button callback. Hides the opening panel and resumes gameplay.
    /// Repeated calls cannot restart an active or completed run.
    /// </summary>
    public void StartGame()
    {
        if (hasStarted || hasEnded)
            return;

        hasStarted = true;
        startPanel.SetActive(false);

        SetPlayerControls(true);
        Time.timeScale = 1f;
    }

    private void ShowVictory()
    {
        ShowEndScreen("Victory!");
    }

    private void ShowDefeat()
    {
        ShowEndScreen("Game Over");
    }

    /// <summary>
    /// Freezes gameplay and displays the shared end panel with the result.
    /// Only the first result is accepted for the current run.
    /// </summary>
    private void ShowEndScreen(string title)
    {
        if (hasEnded)
            return;

        hasEnded = true;

        SetPlayerControls(false);
        Time.timeScale = 0f;

        startPanel.SetActive(false);
        endTitle.text = title;
        endPanel.SetActive(true);
    }

    /// <summary>
    /// Play Again button callback. Reloads the scene to reset gameplay and
    /// return to the start panel. The scene must be in the build scene list.
    /// </summary>
    public void RestartScene()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name);
    }

    // Input components are disabled explicitly so menus cannot trigger movement
    // or shooting while physics and scaled timers are paused.
    private void SetPlayerControls(bool active)
    {
        PlayerMovement movement =
            playerHealth.GetComponent<PlayerMovement>();

        PlayerShooting shooting =
            playerHealth.GetComponent<PlayerShooting>();

        if (movement != null)
            movement.enabled = active;

        if (shooting != null)
            shooting.enabled = active;
    }

    private void OnDestroy()
    {
        // Time scale is global; restore it when this component is destroyed.
        Time.timeScale = 1f;
    }
}