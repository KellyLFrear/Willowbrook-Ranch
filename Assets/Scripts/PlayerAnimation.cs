using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerMove playerMove;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerWatering();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            TriggerInteract();
        }

        UpdateWalkingAnimation();
    }

    private void UpdateWalkingAnimation()
    {
        if (playerMove == null) return;

        float moveInput = playerMove.MoveValue.y;
        bool isWalking = Mathf.Abs(moveInput) > 0.1f;

        animator.SetBool("isWalking", isWalking);
    }

    public void TriggerWatering()
    {
        animator.SetTrigger("doWatering");
    }

    public void TriggerInteract()
    {
        animator.SetTrigger("interact");
    }

    public void TriggerHarvesting()
    {
        animator.SetTrigger("isHarvesting");
    }

    public void TriggerPickingFruit()
    {
        animator.SetTrigger("pickingFruit");
    }
}
