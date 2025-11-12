using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rb;

    public float forwardSpeed = 3f; // Speed for W/S keys
    public float rotationSpeed = 100f; // Speed for A/D keys (in degrees per second)

    InputAction moveAction;
    Vector2 moveValue;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // --- IMPORTANT ---
        // Make sure to freeze rotation in the Rigidbody inspector
        // so the player doesn't tip over!
    }

    void OnEnable()
    {
        Debug.Log("PlayerMove script is enabled");
        this.transform.position = new Vector3(49.0f, 0f, 265.0f);

        moveAction = InputSystem.actions.FindAction("Move");
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        MoveAndRotatePlayer();
    }

    private void MoveAndRotatePlayer()
    {
        // --- Forward/Backward Movement (from W/S keys) ---
        float forwardInput = moveValue.y;
        Vector3 forwardMove = transform.forward * forwardInput * forwardSpeed;

        // Apply forward velocity, but keep the current Y velocity (for gravity)
        rb.linearVelocity = new Vector3(forwardMove.x, rb.linearVelocity.y, forwardMove.z);

        // --- Left/Right Rotation (from A/D keys) ---
        float rotateInput = moveValue.x;

        // Calculate rotation amount in degrees
        float yaw = rotateInput * rotationSpeed * Time.fixedDeltaTime;
        Quaternion rotation = Quaternion.Euler(0f, yaw, 0f);

        // Apply the rotation to the Rigidbody
        rb.MoveRotation(rb.rotation * rotation);
    }
}