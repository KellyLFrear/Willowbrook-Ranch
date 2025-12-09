using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Movement Settings")]
    public float forwardSpeed = 3f; // Normal speed
    public float rotationSpeed = 100f;// Speed for A/D keys
    public float sprintSpeed = 6f;// Speed when sprinting
    public float curSpeed;// Tracks the active movement speed

    [Header("Speed Mode Settings")]
    [Tooltip("Enable SPEED MODE for ultra-fast movement")]
    [SerializeField] private bool SPEED_MODE = false;
    public bool SPEEED { get => SPEED_MODE; set => SPEED_MODE = value; }
    public float speedModeForwardSpeed = 30f; // Speed mode forward speed
    public float speedModeRotationSpeed = 280f; // Speed mode rotation speed

    [Header("Grounding Settings")]
    public float groundingForce = 100f; // Force to keep player grounded at high speeds
    public LayerMask groundLayer = -1; // Layer mask for ground detection (default: Everything)
    public float groundCheckDistance = 0.25f; // Distance to check for ground

    // Input Actions
    InputAction moveAction;
    InputAction sprintAction;//Action for sprinting

    public Vector2 MoveValue { get; private set; }
    public bool IsSprinting { get; private set; } 

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        curSpeed = forwardSpeed; // current speed initialized
    }

    void OnEnable()
    {
        Debug.Log("PlayerMove script is enabled");

        // Enable Move action
        moveAction = InputSystem.actions.FindAction("Move");
        moveAction.Enable();

        // Enable Sprint action
        sprintAction = InputSystem.actions.FindAction("Sprint");
        sprintAction.Enable();
    }

    void OnDisable()
    {
        if (moveAction != null)
            moveAction.Disable();
        if (sprintAction != null)
            sprintAction.Disable();
    }

    void Update()
    {
        MoveValue = moveAction.ReadValue<Vector2>();

        //read the state of the Sprint action
        IsSprinting = sprintAction.ReadValue<float>() > 0;
    }

    void FixedUpdate()
    {
        // update curSpeed based on the sprinting state and speed mode
        if (SPEED_MODE)
        {
            curSpeed = speedModeForwardSpeed;
        }
        else if (IsSprinting)
        {
            curSpeed = sprintSpeed;
        }
        else
        {
            curSpeed = forwardSpeed;
        }

        MoveAndRotatePlayer();
        ApplyGroundingForce();
    }

    private void MoveAndRotatePlayer()
    {

        float forwardInput = MoveValue.y;

        //uses curSpeed instead of forwardSpeed for movement calculation
        Vector3 forwardMove = transform.forward * forwardInput * curSpeed;

        //modify the X and Z velocity components to preserve any vertical velocity
        rb.linearVelocity = new Vector3(forwardMove.x, rb.linearVelocity.y, forwardMove.z);


        float rotateInput = MoveValue.x;

        // Use speed mode rotation speed if enabled
        float currentRotationSpeed = SPEED_MODE ? speedModeRotationSpeed : rotationSpeed;

        //rotation logic with dynamic rotation speed
        float yaw = rotateInput * currentRotationSpeed * Time.fixedDeltaTime;
        Quaternion rotation = Quaternion.Euler(0f, yaw, 0f);
        rb.MoveRotation(rb.rotation * rotation);
    }

    private void ApplyGroundingForce()
    {
        if (!SPEED_MODE) return;

        // Raycast downward to check if player is near ground
        RaycastHit hit;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, groundLayer);

        // Kill upward velocity to prevent launches
        if (rb.linearVelocity.y > 0.01f)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        }

        if (isGrounded)
        {
            // Only apply force if we're starting to lift off, not if we're already on ground
            if (hit.distance > 0.15f)
            {
                rb.AddForce(Vector3.down * groundingForce, ForceMode.Force);
            }
        }
        else
        {
            // If airborne, apply moderate force to bring back down
            rb.AddForce(Vector3.down * groundingForce * 2f, ForceMode.Force);
        }
    }
}