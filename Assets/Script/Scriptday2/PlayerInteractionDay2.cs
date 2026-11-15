using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerInteractionDay2 : MonoBehaviour
{
    private IInteractable currentInteractable;

    private IInteractable CariInteractable(Collider2D other)
    {
        Component hasil = other.GetComponent(typeof(IInteractable));

        if (hasil == null)
            hasil = other.GetComponentInParent(typeof(IInteractable));

        return hasil as IInteractable;
    }

    private void Update()
    {
        // Interaksi PC menggunakan tombol E
        if (currentInteractable != null &&
            Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            currentInteractable.Interact();
        }

        // Klik mouse atau touchpad
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (EventSystem.current != null &&
                EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 posisi =
                Mouse.current.position.ReadValue();

            InteraksiDariLayar(posisi);
        }

        // Sentuhan layar HP
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

        Vector3 posisiWorld =
            Camera.main.ScreenToWorldPoint(posisiLayar);

        Collider2D hit =
            Physics2D.OverlapPoint(posisiWorld);

        if (hit != null)
        {
            IInteractable interactable = CariInteractable(hit);

            if (interactable != null)
            {
                interactable.Interact();
                Debug.Log("Objek berhasil ditap: " + hit.name);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable = CariInteractable(other);

        if (interactable != null)
            currentInteractable = interactable;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactable = CariInteractable(other);

        if (interactable != null &&
            currentInteractable == interactable)
        {
            currentInteractable = null;
        }
    }
}
