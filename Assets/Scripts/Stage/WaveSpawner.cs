using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns the configured sequence of enemy waves and assigns their targets.
/// Waits for each wave to be cleared before advancing, and publishes events
/// so UI components can display progress and victory independently.
/// </summary>
public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private PlayerHealth player;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private int[] enemiesPerWave = { 3, 5, 7 };
    [SerializeField, Min(0.1f)] private float spawnInterval = 0.8f;
    [SerializeField, Min(0f)] private float breakBetweenWaves = 2f;

    private readonly List<GameObject> livingEnemies = new();

    public int CurrentWave { get; private set; }
    public int TotalWaves => enemiesPerWave.Length;
    public bool HasWon { get; private set; }

    /// <summary>
    /// Raised when a wave starts, providing its one-based number and total waves.
    /// </summary>
    public event System.Action<int, int> WaveChanged;

    /// <summary>Raised after the final wave has been cleared.</summary>
    public event System.Action Won;

    private IEnumerator Start()
    {
        // Validate scene references before starting the spawning sequence.
        if (enemyPrefab == null || player == null ||
            spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError(
                "WaveSpawner: assign the prefab, player and spawn points.",
                this);
            yield break;
        }

        foreach (Transform point in spawnPoints)
        {
            if (point == null)
            {
                Debug.LogError("WaveSpawner: a spawn point is missing.", this);
                yield break;
            }
        }

        for (int wave = 0; wave < enemiesPerWave.Length; wave++)
        {
            // Also delays the first wave. Scaled time makes this wait pause
            // while GameScreens shows the start menu.
            yield return new WaitForSeconds(breakBetweenWaves);

            if (player == null || player.IsDead)
                yield break;

            CurrentWave = wave + 1;
            WaveChanged?.Invoke(CurrentWave, TotalWaves);

            Debug.Log($"Wave {CurrentWave}/{TotalWaves}", this);

            // Randomize the first spawn point, then cycle through the others.
            int firstPoint = Random.Range(0, spawnPoints.Length);

            for (int i = 0; i < enemiesPerWave[wave]; i++)
            {
                if (player == null || player.IsDead)
                    yield break;

                int pointIndex = (firstPoint + i) % spawnPoints.Length;
                Transform point = spawnPoints[pointIndex];

                GameObject enemy = Instantiate(
                    enemyPrefab, point.position, Quaternion.identity);

                // Connect the prefab instance to the scene's player.
                enemy.GetComponent<EnemyChase>()
                    .SetTarget(player.transform);

                enemy.GetComponent<EnemyMeleeAttack>()
                    .SetTarget(player);

                livingEnemies.Add(enemy);

                yield return new WaitForSeconds(spawnInterval);
            }

            // Wait for actual removal, including each enemy's delayed death effect.
            while (livingEnemies.Count > 0)
            {
                if (player == null || player.IsDead)
                    yield break;

                // Destroyed Unity objects compare equal to null.
                livingEnemies.RemoveAll(enemy => enemy == null);
                yield return null;
            }

            // Do not award victory or continue waves after the player dies.
            if (player == null || player.IsDead)
                yield break;

            Debug.Log($"Wave {CurrentWave} cleared!", this);
        }

        HasWon = true;
        Debug.Log("Victory! All waves cleared.", this);
        Won?.Invoke();
    }
}