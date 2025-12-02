using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(Rigidbody))]
public class FootstepAudio : MonoBehaviour
{
    [Header("Footstep Clips")]
    public AudioClip[] walkClips;
    public AudioClip[] sprintClips;

    [Header("Step Timing")]
    public float walkStepInterval = 0.5f;    // time between steps when walking
    public float sprintStepInterval = 0.3f;  // time between steps when sprinting

    [Header("Movement Settings")]
    public float minMoveSpeed = 0.1f;        // ignore tiny jitter

    [Header("Ground Check")]
    public LayerMask groundMask = ~0;        // which layers count as ground
    public float groundCheckDistance = 0.3f; // raycast distance down

    private AudioSource audioSource;
    private PlayerMove playerMove;
    private Rigidbody rb;

    private float stepTimer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerMove = GetComponent<PlayerMove>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!playerMove || !audioSource || !rb) return;

        // 1) Only care about forward/back input (W/S or stick up/down)
        float forwardInput = playerMove.MoveValue.y;
        if (Mathf.Abs(forwardInput) < 0.1f)
        {
            stepTimer = 0f;
            return;
        }

        // 2) Check actual horizontal movement speed
        Vector3 horizVel = rb.linearVelocity;      // if you get an error, use rb.linearVelocity instead
        horizVel.y = 0f;

        if (horizVel.magnitude < minMoveSpeed)
        {
            stepTimer = 0f;
            return;
        }

        // 3) Check if grounded
        bool grounded = Physics.Raycast(
            transform.position + Vector3.up * 0.1f,
            Vector3.down,
            groundCheckDistance,
            groundMask
        );

        if (!grounded)
        {
            stepTimer = 0f;
            return;
        }

        // 4) Decide if sprinting based on PlayerMove
        bool isSprinting = playerMove.IsSprinting;
        float interval = isSprinting ? sprintStepInterval : walkStepInterval;

        // 5) Timer and play step
        stepTimer += Time.deltaTime;
        if (stepTimer >= interval)
        {
            PlayStep(isSprinting);
            stepTimer = 0f;
        }
    }

    private void PlayStep(bool sprint)
    {
        AudioClip clip = sprint
            ? GetRandomClip(sprintClips)
            : GetRandomClip(walkClips);

        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0)
            return null;

        int index = Random.Range(0, clips.Length);
        return clips[index];
    }
}
