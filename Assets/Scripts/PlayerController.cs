using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField]
    [Tooltip("Player speed")]
    private float moveSpeed = 12f;
    [SerializeField]
    [Tooltip("Multiplier to apply on moving if player is in air")]
    private float speedInAirMultiplier = 0.55f;

    [Header("Jump")]
    [SerializeField]
    [Tooltip("How much player jumps")]
    private float jumpForce = 15f;
    [SerializeField]
    [Tooltip("Transform of player feet to check if grounded")]
    private Transform feet;

    [Header("Looking")]
    [SerializeField]
    [Tooltip("Sensitivity of player turn")]
    private float lookSpeed = 3f;
    [SerializeField]
    [Tooltip("Camera component")]
    private Camera cam;
    [SerializeField]
    [Tooltip("X angle constraint for look. Both positive and negative")]
    private float lookUpConstraint = 89f;
    [SerializeField]
    private LayerMask groundMask;
    private float xLook = 0f;

    private Input input;
    private Rigidbody playerRb;
    private Vector3 velocity;

    /// <summary>
    /// If player is on ground or in air
    /// </summary>
    public bool IsGrounded { get; private set; } = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<Input>();
        playerRb = GetComponent<Rigidbody>();
    }

    // Called every fixed time frame
    private void FixedUpdate()
    {
        // Handle jump
        Jump();

        // Handle movement
        Move();

        // Handle looking
        Look();
    }

    // Handles jump behaviour
    private void Jump()
    {
        // Get Player input
        if (input.GetJump())
        {
            // Jump only if player is grounded
            if (IsGrounded)
            {
                // Add up force
                //IsGrounded = false;
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            }
        }
    }

    // Handles look behaviour
    private void Look()
    {
        // Get player input
        Vector2 look = input.GetLook() * Time.fixedDeltaTime;

        // Horizontal look (rotate player)
        transform.Rotate(new Vector3(0f, look.x * lookSpeed, 0f));

        // Vertical look (rotate camera)
        xLook -= look.y * lookSpeed;
        xLook = Mathf.Clamp(xLook, -lookUpConstraint, lookUpConstraint);
        cam.transform.localRotation = Quaternion.Euler(xLook, 0f, 0f);
    }

    // Handles move behnaviour
    private void Move()
    {
        // Get player input
        Vector2 move = input.GetMove();

        // Get current and target velocity
        Vector3 currentVelocity = playerRb.linearVelocity;
        Vector3 targetVelocity = new Vector3(move.x, 0f, move.y);
        
        // Multiply target velocity by speed
        // if player is in air, multiply speed by multiplier
        targetVelocity *= IsGrounded? moveSpeed:moveSpeed*speedInAirMultiplier;

        // Transform target velocity to world space
        targetVelocity = transform.TransformDirection(targetVelocity);

        // Get velocity change
        Vector3 velocityChange = targetVelocity- currentVelocity;
        velocityChange = new Vector3(velocityChange.x, 0f, velocityChange.z);

        //Vector3.ClampMagnitude(velocityChange, maxForce);
        playerRb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    /// <summary>
    /// Set Is Grounded property
    /// </summary>
    /// <param name="grounded"></param>
    public void SetGrounded(bool grounded) => IsGrounded = grounded;
}
