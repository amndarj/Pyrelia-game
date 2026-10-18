using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable currentInteractable;

    private void Update()
    {
        if (currentInteractable != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            currentInteractable.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
        {
            currentInteractable = interactable;
            Debug.Log("Tekan E untuk berinteraksi.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable != null && currentInteractable == interactable)
        {
            NPC_Korban npc = other.GetComponent<NPC_Korban>();

            if (npc != null && npc.dialogueUI != null)
            {
                npc.dialogueUI.CloseDialogue();
            }

            currentInteractable = null;
        }
    }
}