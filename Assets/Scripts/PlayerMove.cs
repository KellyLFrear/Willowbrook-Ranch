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

    
    public Vector2 MoveValue { get; private set; }


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        Debug.Log("PlayerMove script is enabled");
        moveAction = InputSystem.actions.FindAction("Move");
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    void Update()
    {
        // --- CHANGE THIS ---
        // moveValue = moveAction.ReadValue<Vector2>();
        // --- TO THIS ---
        MoveValue = moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        MoveAndRotatePlayer();
    }

    private void MoveAndRotatePlayer()
    {
       
        float forwardInput = MoveValue.y;

        Vector3 forwardMove = transform.forward * forwardInput * forwardSpeed;
        rb.linearVelocity = new Vector3(forwardMove.x, rb.linearVelocity.y, forwardMove.z);

        
        float rotateInput = MoveValue.x;

        float yaw = rotateInput * rotationSpeed * Time.fixedDeltaTime;
        Quaternion rotation = Quaternion.Euler(0f, yaw, 0f);
        rb.MoveRotation(rb.rotation * rotation);
    }
}