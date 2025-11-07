using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // 1. The Input System "using" statement

//This example project referenced video tutorial list made by Brackeys on Youtube
//https://www.youtube.com/watch?v=Au8oX5pu5u4&list=PLPV2KyIb3jR5QFsefuO2RlAgWEz6EvVi6&index=4

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rb;

    public float forwardSpeed = 1f;
    public float sidewaySpeed = 1f;

    InputAction moveAction;
    Vector2 moveValue;

    // Added Awake() for robustness
    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // This gets the Rigidbody component automatically
        // It prevents a NullReferenceException if you forget to drag it in the Inspector
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        Debug.Log("PlayerMove script is enabled");
        this.transform.position = new Vector3(49.0f,0f, 265.0f);//make sure the player starts at 0 

        moveAction = InputSystem.actions.FindAction("Move");

        // The action must be enabled before you can read from it.
        moveAction.Enable();
    }

    // Added OnDisable() for good practice
    // This disables the action when the script/object is disabled
    void OnDisable()
    {
        moveAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // Input reading is best done in Update()
        moveValue = moveAction.ReadValue<Vector2>();

        /*Older version code that can still work with Unity 6 after system setting change
        if (Input.GetKey("w"))
            rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        if (Input.GetKey("a"))
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0);
        if (Input.GetKey("d"))
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0);
        */
    }

    //All Rigidbody (physics) movement should be in FixedUpdate()
    void FixedUpdate()
    {
        // We call MovePlayer from here now
        MovePlayer();
    }

    //Move player
    private void MovePlayer()
    {
        // This logic now uses both 'forwardSpeed' and 'sidewaySpeed'

        // 1. Get the direction vectors
        Vector3 forwardMove = transform.forward * moveValue.y * forwardSpeed;
        Vector3 sideMove = transform.right * moveValue.x * sidewaySpeed;

        // 2. Combine them and apply Time.fixedDeltaTime (for FixedUpdate)
        Vector3 finalMove = (forwardMove + sideMove) * Time.fixedDeltaTime;

        // 3. Apply the movement to the Rigidbody's position
        rb.MovePosition(rb.position + finalMove);
    }

} 