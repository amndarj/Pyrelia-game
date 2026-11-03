
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionDay3 : MonoBehaviour
{
    public float interactionRadius = 1.5f;
    public LayerMask interactableLayer;

    private IInteractableDay3 currentInteractable;
    private bool wasInRange;

    void Update()
    {
        DetectNPC();

        bool isInRange = currentInteractable != null;

        // Player baru memasuki jangkauan NPC
        if (isInRange && !wasInRange)
        {
            Debug.Log("Tekan E untuk melanjutkan dialog.");

            // Dialog pertama langsung dimulai
            currentInteractable.Interact();
        }

        if (!isInRange && wasInRange)
        {
            Debug.Log("Menjauh dari NPC.");
        }

        wasInRange = isInRange;

        // Tombol E untuk melanjutkan dialog
        if (isInRange &&
            Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            currentInteractable.Interact();
        }
    }

    void DetectNPC()
    {
        currentInteractable = null;

        Collider2D hit = Physics2D.OverlapCircle(
            transform.position,
            interactionRadius,
            interactableLayer
        );

        if (hit != null)
        {
            currentInteractable =
                hit.GetComponentInParent<IInteractableDay3>();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(
            transform.position,
            interactionRadius
        );
    }
}
