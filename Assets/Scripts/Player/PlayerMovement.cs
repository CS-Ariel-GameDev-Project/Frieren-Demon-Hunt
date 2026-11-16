using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Reads WASD or arrow-key input and applies top-down Rigidbody2D movement.
/// Manages its input action throughout the component's lifetime.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Min(0f)] private float moveSpeed = 4f;

    private Rigidbody2D body;
    private InputAction moveAction;
    private Vector2 moveInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();

        // Create keyboard bindings at runtime without an Input Actions asset.
        moveAction = new InputAction("Move", InputActionType.Value);

        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/rightArrow");
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void Update()
    {
        // Sample input each frame and prevent faster diagonal movement.
        moveInput = Vector2.ClampMagnitude(
            moveAction.ReadValue<Vector2>(), 1f);
    }

    private void FixedUpdate()
    {
        // Apply the latest input at the physics timestep.
        // Velocity is measured in units per second, so no delta-time factor is needed.
        body.linearVelocity = moveInput * moveSpeed;
    }

    private void OnDisable()
    {
        // Menus and death disable this component. Clear both input and velocity
        // so the player stops instead of retaining the last movement command.
        moveAction.Disable();
        moveInput = Vector2.zero;
        body.linearVelocity = Vector2.zero;
    }

    private void OnDestroy()
    {
        // Release the input action created and owned by this component.
        moveAction.Dispose();
    }
}