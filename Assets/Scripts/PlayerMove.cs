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
        moveAction.Disable();
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
        // update curSpeed based on the sprinting state
        if (IsSprinting)
        {
            curSpeed = sprintSpeed;
        }
        else
        {
            curSpeed = forwardSpeed;
        }

        MoveAndRotatePlayer();
    }

    private void MoveAndRotatePlayer()
    {

        float forwardInput = MoveValue.y;

        //uses curSpeed instead of forwardSpeed for movement calculation
        Vector3 forwardMove = transform.forward * forwardInput * curSpeed;

        //modify the X and Z velocity components to preserve any vertical velocity
        rb.linearVelocity = new Vector3(forwardMove.x, rb.linearVelocity.y, forwardMove.z);


        float rotateInput = MoveValue.x;

        //rotation logic remains the same
        float yaw = rotateInput * rotationSpeed * Time.fixedDeltaTime;
        Quaternion rotation = Quaternion.Euler(0f, yaw, 0f);
        rb.MoveRotation(rb.rotation * rotation);
    }
}