using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable currentInteractable;

    private void Update()
    {
        // Interaksi PC menggunakan tombol E
        if (currentInteractable != null &&
            Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            currentInteractable.Interact();
        }

        // Interaksi menggunakan klik mouse
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (EventSystem.current != null &&
                EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 posisi = Mouse.current.position.ReadValue();
            InteraksiDariLayar(posisi);
        }

        // Interaksi menggunakan tap layar HP
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            var touch = Touchscreen.current.primaryTouch;
            int touchId = touch.touchId.ReadValue();

            if (EventSystem.current != null &&
                EventSystem.current.IsPointerOverGameObject(touchId))
                return;

            Vector2 posisi = touch.position.ReadValue();
            InteraksiDariLayar(posisi);
        }
    }

    private void InteraksiDariLayar(Vector2 posisiLayar)
    {
        if (Camera.main == null) return;

        Vector3 posisiWorld = Camera.main.ScreenToWorldPoint(
            new Vector3(
                posisiLayar.x,
                posisiLayar.y,
                -Camera.main.transform.position.z
            )
        );

        Collider2D hit = Physics2D.OverlapPoint(posisiWorld);

        if (hit == null) return;

        IInteractable interactable =
            hit.GetComponentInParent<IInteractable>();

        if (interactable != null)
        {
            interactable.Interact();
            Debug.Log("Objek berhasil ditap: " + hit.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable =
            other.GetComponentInParent<IInteractable>();

        if (interactable != null)
        {
            currentInteractable = interactable;
            Debug.Log("Tekan E atau tap objek untuk berinteraksi.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactable =
            other.GetComponentInParent<IInteractable>();

        if (interactable == null ||
            currentInteractable != interactable)
            return;

        NPC_Korban npcKorban =
            other.GetComponentInParent<NPC_Korban>();

        if (npcKorban != null &&
            npcKorban.dialogueUI != null)
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
