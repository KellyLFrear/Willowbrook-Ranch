using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerMove playerMove; // <-- ADD THIS

    // --- We don't need these anymore ---
    // private Vector3 lastPosition;
    // public float speedThreshold = 0.1f;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>(); // <-- ADD THIS
        // lastPosition = transform.position; // <-- REMOVE THIS
    }

    void Update()
    {
        // --- This script ONLY listens for these keys ---
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerWatering();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            TriggerInteract();
        }

        // --- REMOVE ALL THIS OLD LOGIC ---
        /*
        Vector3 deltaPosition = transform.position - lastPosition;
        float speed = new Vector3(deltaPosition.x, 0, deltaPosition.z).magnitude / Time.deltaTime;

        bool isWalking = speed > speedThreshold;
        animator.SetBool("isWalking", isWalking);

        lastPosition = transform.position;
        */

        // --- ADD THIS NEW LOGIC ---
        UpdateWalkingAnimation();
    }

    private void UpdateWalkingAnimation()
    {
        if (playerMove == null) return; // Safety check

        // Read the 'y' value (W/S keys) from the PlayerMove script.
        // We use Mathf.Abs() because moving backward (input of -1) is also "walking".
        float moveInput = playerMove.MoveValue.y;
        bool isWalking = Mathf.Abs(moveInput) > 0.1f; // 0.1f acts as a small "deadzone"

        // This bool will now stay 'true' as long as W or S is held down,
        // allowing the animation to loop smoothly.
        animator.SetBool("isWalking", isWalking);
    }


    // --- PUBLIC FUNCTIONS for other scripts (like PlayerPlanting) to call ---

    public void TriggerWatering()
    {
        if (animator) animator.SetTrigger("doWatering");
    }

    public void TriggerInteract()
    {
        if (animator) animator.SetTrigger("interact");
    }

    public void TriggerHarvesting()
    {
        if (animator) animator.SetTrigger("isHarvesting");
    }

    public void TriggerPickingFruit()
    {
        if (animator) animator.SetTrigger("pickingFruit");
    }
}