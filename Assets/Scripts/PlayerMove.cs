using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rb;

    public float forwardSpeed = 1f;
    public float sidewaySpeed = 1f;

    InputAction moveAction;
    Vector2 moveValue;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        Debug.Log("PlayerMove script is enabled");
        //this.transform.position = new Vector3(49.0f, 0f, 265.0f);

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
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 forwardMove = transform.forward * moveValue.y * forwardSpeed;
        Vector3 sideMove = transform.right * moveValue.x * sidewaySpeed;
        Vector3 finalMove = (forwardMove + sideMove) * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + finalMove);
    }
}