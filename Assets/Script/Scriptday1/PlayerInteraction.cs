
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable currentInteractable;

    private void Update()
    {
        if (currentInteractable != null &&
            Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            currentInteractable.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable =
            other.GetComponentInParent<IInteractable>();

        if (interactable != null)
        {
            currentInteractable = interactable;
            Debug.Log("Tekan E untuk berinteraksi.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactable =
            other.GetComponentInParent<IInteractable>();

        if (interactable == null || currentInteractable != interactable)
            return;

        NPC_Korban npcKorban =
            other.GetComponentInParent<NPC_Korban>();

        if (npcKorban != null && npcKorban.dialogueUI != null)
        {
            npcKorban.dialogueUI.CloseDialogue();
        }

        NPC_DamkarInteractable npcDamkar =
            other.GetComponentInParent<NPC_DamkarInteractable>();

        if (npcDamkar != null)
        {
            npcDamkar.CloseInteractionUI();
        }

        currentInteractable = null;
    }
}
