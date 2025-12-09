using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class CanvasOnTouch : MonoBehaviour
{
    [Header("UI to enable (shared canvas)")]
    [SerializeField] private GameObject uiCanvas;

    [Header("Who can activate it? (optional)")]
    [Tooltip("Leave empty = anyone can trigger. Otherwise, drag Player or other GameObjects here.")]
    [SerializeField] private List<GameObject> activators = new List<GameObject>();

    [Header("Behavior")]
    [SerializeField] private bool hideOnExit = true;  // turn off when they leave

    [Header("Dialogue for THIS NPC")]
    [Tooltip("These lines will be shown when the player talks to this NPC.")]
    [TextArea]
    public string[] npcLines;

    private Dialogue dialogue;        // shared Dialogue component on canvas
    private TalkTrigger talkTrigger;  // TalkTrigger on this NPC

    void Reset()
    {
        // Ensure the CapsuleCollider is a trigger
        var col = GetComponent<CapsuleCollider>();
        col.isTrigger = true;

        var rb = GetComponent<Rigidbody>();
        if (!rb) rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;             // keeps it from falling/moving
        rb.useGravity = false;             // not needed for a trigger anchor
        rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
    }

    void Awake()
    {
        if (uiCanvas == null)
        {
            Debug.LogError("[CanvasOnTouch] uiCanvas not assigned.");
            enabled = false;
            return;
        }

        // Find the Dialogue component on the canvas (including inactive children)
        dialogue = uiCanvas.GetComponentInChildren<Dialogue>(true);
        if (dialogue == null)
        {
            Debug.LogError("[CanvasOnTouch] No Dialogue component found on uiCanvas.");
        }

        // Get TalkTrigger on this NPC (optional but recommended)
        talkTrigger = GetComponent<TalkTrigger>();

        uiCanvas.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!IsActivator(other.transform)) return;

        // Turn on the shared canvas
        uiCanvas.SetActive(true);

        if (dialogue != null)
        {
            // Clear any old subscribers
            dialogue.onDialogueEnd = null;

            // Start this NPC's dialogue
            dialogue.PlayLines(npcLines);

            // When dialogue ends, stop talking animation
            if (talkTrigger != null)
            {
                dialogue.onDialogueEnd += () =>
                {
                    talkTrigger.StopTalkingForThisVisit();
                };
            }
        }

        // Start talking animation for this visit
        if (talkTrigger != null)
        {
            talkTrigger.StartTalkingForThisVisit();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!hideOnExit) return;
        if (!IsActivator(other.transform)) return;

        uiCanvas.SetActive(false);
    }

    // Accept the exact object OR any of its children. Empty list = accept anyone.
    bool IsActivator(Transform t)
    {
        if (activators == null || activators.Count == 0) return true;
        foreach (var go in activators)
        {
            if (!go) continue;
            var root = go.transform;
            if (t == root || t.IsChildOf(root)) return true;
        }
        return false;
    }
}
