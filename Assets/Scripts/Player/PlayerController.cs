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

    [Header("Interaction")]
    [SerializeField]
    [Tooltip("Distance to interact with objects")]
    private float interactRange = 1.5f;

    [Header("Grabbing")]
    [SerializeField]
    [Tooltip("How much strength to apply when throwing objects")]
    private float throwStrength = 25f;
    [SerializeField]
    [Tooltip("Speed of object rotation when objects are grabbed")]
    private float grabbedObjectRotation = 15f;
    [SerializeField]
    [Tooltip("Layer to display grabbed objects when they stuck in static objects")]
    private LayerMask grabLayer;
    [SerializeField]
    [Tooltip("Hom much distance from the camera to the grabbed object")]
    private Vector3 grabbedObjectOffset = new Vector3(0f, 0.5f, 1f);

    [Header("Sliding")]
    [Tooltip("Increases speed of the player when on ice")]
    [SerializeField]
    private float iceSpeedIncrease = 2f;

    public GrabbedObject GrabbedObject { get; private set; }
    //private Rigidbody grabbedBody;

    private Input input;
    private Rigidbody playerRb;
    private Vector3 velocity;

    public delegate void OnDisplayebleLook(IDisplayable target);
    public event OnDisplayebleLook DisplaybleLook;

    private ForceMode movingForce = ForceMode.VelocityChange;

    public bool ConstainedMovement { get; set; }

    /// <summary>
    /// If player is on ground or in air
    /// </summary>
    public bool IsGrounded { get; private set; } = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Get references to player components
        input = GetComponent<Input>();
        playerRb = GetComponent<Rigidbody>();
    }

    // Called every frame
    private void Update()
    {
        HandleInteraction();

        LookForDisplayable();
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

        // Handle throw
        if (input.GetThrow())
        {
            Throw();
        }

        // Handle grabbed object rotation
        RotateGrabbedObject();

        if (ConstainedMovement) playerRb.linearVelocity = Vector3.zero;
    }

    private void RotateGrabbedObject()
    {
        // Can't rotate nothig
        if (GrabbedObject != null)
        {
            // Get rotation input and rotate on it
            Vector2 rotateGrabbed = input.GetRotateGrabbed();
            GrabbedObject.transform.Rotate(Vector3.back, rotateGrabbed.x * grabbedObjectRotation * Time.deltaTime);
            GrabbedObject.transform.Rotate(Vector3.right, rotateGrabbed.y * grabbedObjectRotation * Time.deltaTime);
        }
    }

    // Interation
    private void HandleInteraction()
    {
        // Check if interaction is pressed 
        if (input.GetInteractDown())
        {
            // Check if can hit interactible object whithin interact range
            RaycastHit hit;
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            if (Physics.Raycast( ray, out hit, interactRange))
            {
                IInteractible interactible = hit.collider.GetComponent<IInteractible>();

                // Check if object is interactible
                if (interactible != null && interactible.CanInteract(this))
                {
                    // Interact
                    interactible.Interact(this);
                }
            }
        }
        else if (input.GetInteractUp()) // check if player re
        {
            // Drop
            Drop();
        }
    }

    // Handles jump behaviour
    private void Jump()
    {
        if (ConstainedMovement == true) return;

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
        if (ConstainedMovement == true) return;

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
        playerRb.AddForce(velocityChange, movingForce);
    }

    /// <summary>
    /// Set Is Grounded property
    /// </summary>
    /// <param name="grounded"></param>
    public void SetGrounded(bool grounded) => IsGrounded = grounded;

    /// <summary>
    ///  Attach object to the player
    /// </summary>
    /// <param name="grabbed">Object to grab</param>
    public void Grab(GrabbedObject grabbed)
    {
        // Get rigidbody of grabbed object
        var grabbedRb = grabbed.GetComponent<Rigidbody>();

        // Check if grabbed object has rigidbody
        // cannot attach objects without rigidbodies
        if (grabbedRb == null) return;

        // Drop previous grabbed object if not null
        if (GrabbedObject != null) Drop();

        // Reset Grabbed Object
        GrabbedObject = grabbed;
        grabbedRb.isKinematic = true;

        // Disable collision for the object
        Physics.IgnoreCollision(GetComponent<Collider>(), GrabbedObject.GetComponent<Collider>(), true);

        // Attach GrabbedObject to camera component
        GrabbedObject.transform.parent = cam.transform;

        GrabbedObject.transform.rotation = Quaternion.identity;

        // Set layer of Grabbed object to grab layer
        // TODO HARDCODED LAYER
        GrabbedObject.gameObject.layer = 7;
    }

    /// <summary>
    /// Simple drop attached object
    /// </summary>
    public void Drop()
    {
        // Can't drop nothing
        if(GrabbedObject ==null) return;

        // Re-enable collision between player and Grabbed object
        Physics.IgnoreCollision(GetComponent<Collider>(), GrabbedObject.GetComponent<Collider>(), false);

        // Get rigidbody of grabbed object
        var grabbedRb = GrabbedObject.GetComponent<Rigidbody>();

        // Disable is kinematic property and enable gravity
        grabbedRb.isKinematic = false;
        grabbedRb.useGravity = true;

        // Detach Grabbed object
        GrabbedObject.transform.parent = null;

        // Re-set layer of Grabbed object to default
        GrabbedObject.gameObject.layer = 0;

        // set Grabbed Object to null
        GrabbedObject = null;
    }

    /// <summary>
    /// Throw attached object
    /// </summary>
    public void Throw()
    {
        // Check if has grabbed object
        if (GrabbedObject == null)
        {
            var throwComponent = GetComponent<ThrowProjectileInventory>();

            // If has throw component
            if(throwComponent != null)
            {
                if (!throwComponent.HasProjectile) return;

                throwComponent.ThrowProjectile();
            }

            return;
        }

        // Re-enable collision between player and Grabbed object
        Physics.IgnoreCollision(GetComponent<Collider>(), this.GrabbedObject.GetComponent<Collider>(), false);

        // Get rigidbody of grabbed object
        var grabbedRb = this.GrabbedObject.GetComponent<Rigidbody>();

        // Disable is kinematic property and enable gravity
        grabbedRb.isKinematic = false;
        grabbedRb.useGravity = true;

        // Detach Grabbed object
        this.GrabbedObject.transform.parent = null;

        grabbedRb.AddForce(cam.transform.forward * throwStrength, ForceMode.Impulse);

        // Re-set layer of Grabbed object to default
        GrabbedObject.gameObject.layer = 0;

        // set Grabbed Object to null
        GrabbedObject = null;
    }

    /// <summary>
    /// Try to detect displayble object, and display it
    /// </summary>
    private void LookForDisplayable()
    {
        if (GrabbedObject != null)
        {
            DisplaybleLook(null);
            return;
        }

        // Look for a displayble object
        RaycastHit hit;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out hit, interactRange))
        {
            IDisplayable displayable = hit.collider.GetComponent<IDisplayable>();

            if (displayable != null)
            {
                DisplaybleLook(displayable);
            }
            else
            {
                DisplaybleLook(null);
            }
        }
        else
        {
            DisplaybleLook(null);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Ice")
        {
            movingForce = ForceMode.Acceleration;
            moveSpeed += iceSpeedIncrease;

            var surface = collision.gameObject.GetComponent<IceSurface>();
            surface.OnDestroyHandler += FromIceToGround;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ice")
        {
            var surface = collision.gameObject.GetComponent<IceSurface>();
            surface.OnDestroyHandler -= FromIceToGround;

            FromIceToGround();
        }
    }

    private void FromIceToGround()
    {
        moveSpeed -= iceSpeedIncrease;
        movingForce = ForceMode.VelocityChange;
    }
}
