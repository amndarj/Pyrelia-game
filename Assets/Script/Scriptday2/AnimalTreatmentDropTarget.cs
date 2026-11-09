using UnityEngine;
using UnityEngine.EventSystems;

public class AnimalTreatmentDropTarget : MonoBehaviour, IDropHandler
{
    public AnimalTreatmentManager treatmentManager;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("ALAT DI-DROP KE ANIMAL IMAGE!");

        GameObject objekAlat = eventData.pointerDrag;

        if (objekAlat == null || treatmentManager == null)
        {
            Debug.LogWarning("Alat atau Treatment Manager kosong!");
            return;
        }

        AnimalTreatmentToolDrag alat =
            objekAlat.GetComponent<AnimalTreatmentToolDrag>();

        if (alat == null) return;

        Debug.Log("ALAT DITERIMA: " + alat.jenisAlat);

        treatmentManager.ProsesAlat(alat.jenisAlat);
    }
}
