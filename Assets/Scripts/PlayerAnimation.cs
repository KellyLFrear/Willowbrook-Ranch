using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    // We will use these to calculate speed
    private Vector3 lastPosition;
    public float speedThreshold = 0.1f; // You can tweak this in the Inspector

    void Start()
    {
        animator = GetComponent<Animator>();

        // Store our starting position
        lastPosition = transform.position;
    }

    void Update()
    {
        // Calculate how far we've moved since the last frame
        Vector3 deltaPosition = transform.position - lastPosition;
        float speed = new Vector3(deltaPosition.x, 0, deltaPosition.z).magnitude / Time.deltaTime;

        // Set isWalking true if the player is moving, false if not
        bool isWalking = speed > speedThreshold;
        animator.SetBool("isWalking", isWalking);

        // IMPORTANT: Update lastPosition for the next frame
        lastPosition = transform.position;
    }
}