using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractionDay2 : MonoBehaviour
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
            currentInteractable = null;
        }
    }
}
