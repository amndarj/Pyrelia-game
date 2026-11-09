using UnityEngine;

public class AnimalTreatmentInteractable : MonoBehaviour, IInteractable
{
    [Header("Informasi Hewan")]
    public string namaHewan;

    [TextArea]
    public string deskripsiLuka;

    [Header("Sprite Perawatan")]
    public SpriteRenderer animalRenderer;
    public Sprite spriteAwal;
    public Sprite spriteSetelahDirawat;

    [Header("Sistem Perawatan")]
    public AnimalTreatmentManager treatmentManager;

    private bool sudahDirawat = false;

    private void Start()
    {
        if (animalRenderer == null)
            animalRenderer = GetComponent<SpriteRenderer>();

        if (animalRenderer != null && spriteAwal != null)
            animalRenderer.sprite = spriteAwal;
    }

    public void Interact()
    {
        Debug.Log("INTERACT HEWAN TERPANGGIL!");

        if (sudahDirawat) return;

        if (treatmentManager == null)
        {
            Debug.LogWarning("AnimalTreatmentManager belum dihubungkan!");
            return;
        }

        treatmentManager.BukaPanelPerawatan(this);
    }

    public void SelesaikanPerawatan()
    {
        if (sudahDirawat) return;

        sudahDirawat = true;

        if (animalRenderer != null && spriteSetelahDirawat != null)
            animalRenderer.sprite = spriteSetelahDirawat;
    }
}
