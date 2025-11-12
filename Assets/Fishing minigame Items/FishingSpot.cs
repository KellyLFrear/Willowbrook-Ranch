using UnityEngine;
using TMPro;

public class FishingSpot : MonoBehaviour
{
    public TextMeshProUGUI promptText;
    public FishingCastController castController;
    public KeyCode interactKey = KeyCode.F;
    public float interactRadius = 3f;

    bool isNear;

    void Start()
    {
        if (promptText != null) promptText.gameObject.SetActive(false);
    }

    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactRadius);
        bool near = false;
        foreach (var h in hits)
            if (h.CompareTag("Player")) { near = true; break; }

        if (near && !isNear)
        {
            isNear = true;
            if (promptText != null)
            {
                promptText.gameObject.SetActive(true);
                promptText.text = "Press " + interactKey + " to fish";
            }
        }
        else if (!near && isNear)
        {
            isNear = false;
            if (promptText != null) promptText.gameObject.SetActive(false);
        }

        if (isNear && Input.GetKeyDown(interactKey) && castController != null)
            castController.StartCasting();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
