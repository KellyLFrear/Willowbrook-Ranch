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
        //way to check for the spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //trigger for watering animation
            animator.SetTrigger("doWatering");
        }
          //trigger for interacting 
        if (Input.GetKeyDown(KeyCode.I)) {
            animator.SetTrigger("interact");
        }
        //trigger for haversting
        if (Input.GetKeyDown(KeyCode.H)) {
            animator.SetTrigger("isHarvesting");
        }
        //trigger for picking fruit
        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetTrigger("pickingFruit");
        }

        Vector3 deltaPosition = transform.position - lastPosition;
        float speed = new Vector3(deltaPosition.x, 0, deltaPosition.z).magnitude / Time.deltaTime;

        bool isWalking = speed > speedThreshold;
        animator.SetBool("isWalking", isWalking);

        lastPosition = transform.position;
    }
}