using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Aims at the mouse position on the gameplay plane and fires while left-click
/// is held. Controls projectile creation, spawn offset, and firing cooldown.
/// </summary>
public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private MagicProjectile projectilePrefab;
    [SerializeField] private Camera aimCamera;

    [SerializeField, Min(0.01f)]
    private float shotCooldown = 0.3f;

    [SerializeField, Min(0f)]
    private float spawnDistance = 0.6f;

    private float nextShotTime;

    private const float MinimumAimSqrDistance = 0.001f;

    private void Awake()
    {
        if (aimCamera == null)
            aimCamera = Camera.main;
    }

    private void Update()
    {
        if (Mouse.current == null ||
            aimCamera == null ||
            projectilePrefab == null)
        {
            return;
        }

        if (!Mouse.current.leftButton.isPressed ||
            Time.time < nextShotTime)
        {
            return;
        }

        Vector2 mousePosition = Mouse.current.position.ReadValue();

        // Convert the screen position into a world-space ray, then intersect
        // it with the Z = 0 gameplay plane. This does not test scene colliders.
        Ray ray = aimCamera.ScreenPointToRay(
            new Vector3(mousePosition.x, mousePosition.y, 0f));

        Plane gamePlane = new Plane(Vector3.forward, Vector3.zero);

        if (!gamePlane.Raycast(ray, out float distance))
            return;

        Vector2 targetPosition = ray.GetPoint(distance);
        Vector2 playerPosition = transform.position;
        Vector2 direction = targetPosition - playerPosition;

        // Avoid normalizing a near-zero direction when aiming at the player.
        if (direction.sqrMagnitude < MinimumAimSqrDistance)
            return;

        direction.Normalize();

        // Spawn slightly ahead of the player along the firing direction.
        Vector2 spawnPosition =
            playerPosition + direction * spawnDistance;

        MagicProjectile projectile = Instantiate(
            projectilePrefab,
            new Vector3(spawnPosition.x, spawnPosition.y, 0f),
            Quaternion.identity);

        // Pass the shooter as owner so the projectile ignores its colliders.
        projectile.Launch(direction, gameObject);

        nextShotTime = Time.time + shotCooldown;
    }
}