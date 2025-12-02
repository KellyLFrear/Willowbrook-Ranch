using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerMove playerMove;
    private AudioSource audioSource;

    [Header("Action SFX")]
    public AudioClip plantingClip;      // Plays when planting succeeds
    public AudioClip wateringClip;      // Plays when watering succeeds
    public AudioClip harvestingClip;    // Plays when harvesting succeeds
    public AudioClip pickingFruitClip;  // Optional: use if picking fruit is different from planting

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        audioSource = GetComponent<AudioSource>();   // Uses the same AudioSource as footsteps

        if (audioSource == null)
        {
            Debug.LogWarning("PlayerAnimation: No AudioSource found on this GameObject. Action sounds won't play.");
        }
    }

    void Update()
    {
        // --- This script ONLY listens for these keys directly ---
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
        if (playerMove == null) return; // Safety check

        // Read the 'y' value (W/S keys) from the PlayerMove script.
        // We use Mathf.Abs() because moving backward (input of -1) is also "walking".
        float moveInput = playerMove.MoveValue.y;
        bool isWalking = Mathf.Abs(moveInput) > 0.1f; // 0.1f acts as a small "deadzone"

        // This bool will now stay 'true' as long as W or S is held down,
        // allowing the animation to loop smoothly.
        animator.SetBool("isWalking", isWalking);
    }

    // --- Small helper to play sounds safely ---
    private void PlaySFX(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // --- PUBLIC FUNCTIONS for other scripts (like PlayerPlanting) to call ---

    public void TriggerWatering()
    {
        if (animator) animator.SetTrigger("doWatering");
        PlaySFX(wateringClip);          // <- plays watering sound
    }

    public void TriggerInteract()
    {
        if (animator) animator.SetTrigger("interact");
        // Add a sound here too if you ever want one for generic interact
    }

    public void TriggerHarvesting()
    {
        if (animator) animator.SetTrigger("isHarvesting");
        PlaySFX(harvestingClip);        // <- plays harvesting sound
    }

    public void TriggerPickingFruit()
    {
        if (animator) animator.SetTrigger("pickingFruit");

        // Right now you're using this trigger when PLANTING succeeds.
        // So we treat it as a planting sound by default:
        if (pickingFruitClip != null)
        {
            PlaySFX(pickingFruitClip);  // special sound for picking fruit (if assigned)
        }
        else
        {
            PlaySFX(plantingClip);      // otherwise fall back to planting sound
        }
    }
}
