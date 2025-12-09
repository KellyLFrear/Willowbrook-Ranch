using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TalkTrigger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string talkingBoolName = "isTalking";
    [SerializeField] private string playerTag = "Player";

    private bool playerInside = false;
    private bool canTalkThisVisit = false;

    void Reset()
    {
        // Ensure the collider is marked as trigger
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    void Awake()
    {
        if (!animator) animator = GetComponent<Animator>();
        if (!animator)
        {
            Debug.LogError("TalkTrigger: No Animator assigned or found!");
            enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        playerInside = true;
        canTalkThisVisit = true;  // allow talking for this new visit
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        playerInside = false;
        canTalkThisVisit = false;

        // Force stop talking when player leaves
        SetTalking(false);
    }


    public void StartTalkingForThisVisit()
    {
        if (!playerInside || !canTalkThisVisit) return;
        SetTalking(true);
    }
    public void StopTalkingForThisVisit()
    {
        SetTalking(false);
        canTalkThisVisit = false;
    }

    private void SetTalking(bool value)
    {
        if (animator)
        {
            animator.SetBool(talkingBoolName, value);
        }
    }
}
