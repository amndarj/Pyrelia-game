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

    private string SaveKey =>
        "Day2_Animal_" + gameObject.name;

    private void Start()
    {
        if (animalRenderer == null)
            animalRenderer = GetComponent<SpriteRenderer>();

        MuatStatusHewan();
    }

    public void Interact()
    {
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

        PlayerPrefs.SetInt(SaveKey, 1);
        PlayerPrefs.Save();
    }

    private void MuatStatusHewan()
    {
        sudahDirawat =
            PlayerPrefs.GetInt(SaveKey, 0) == 1;

        if (animalRenderer != null)
        {
            animalRenderer.sprite = sudahDirawat
                ? spriteSetelahDirawat
                : spriteAwal;
        }
    }

    public void ResetStatusHewan()
    {
        sudahDirawat = false;
        PlayerPrefs.DeleteKey(SaveKey);

        if (animalRenderer != null && spriteAwal != null)
            animalRenderer.sprite = spriteAwal;
    }
}
