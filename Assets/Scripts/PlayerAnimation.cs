using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    // For walking
    private Vector3 lastPosition;
    public float speedThreshold = 0.1f;

    void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
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
        // --- 'P' and 'H' have been REMOVED ---

        // Walking animation logic
        Vector3 deltaPosition = transform.position - lastPosition;
        float speed = new Vector3(deltaPosition.x, 0, deltaPosition.z).magnitude / Time.deltaTime;

        bool isWalking = speed > speedThreshold;
        animator.SetBool("isWalking", isWalking);

        lastPosition = transform.position;
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