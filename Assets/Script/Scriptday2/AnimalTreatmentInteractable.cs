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

    [Header("Objective Marker")]
    public GameObject objectiveMarker;

    private bool sudahDirawat = false;

    private string SaveKey =>
        "Day2_Animal_" + gameObject.name;

    private void Start()
    {
        if (animalRenderer == null)
            animalRenderer = GetComponent<SpriteRenderer>();

        MuatStatusHewan();
        PerbaruiMarker();
    }

    public void Interact()
    {
        if (sudahDirawat) return;

        if (Day2MissionManager.instance == null ||
            !Day2MissionManager.instance.BolehRawatHewan)
            return;

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

        if (GameSaveManager.Instance != null)
            GameSaveManager.Instance.RegisterProgressKey(SaveKey);

        PlayerPrefs.Save();
    }

    public void PerbaruiMarker()
    {
        if (objectiveMarker == null)
            return;

        Day2MissionManager manager =
            Day2MissionManager.instance;

        bool tampilkan =
            manager != null &&
            manager.BolehRawatHewan &&
            !sudahDirawat;

        if (objectiveMarker.activeSelf != tampilkan)
            objectiveMarker.SetActive(tampilkan);
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
        if (GameSaveManager.Instance != null)
            GameSaveManager.Instance.RegisterProgressKey(SaveKey);
            
        PlayerPrefs.DeleteKey(SaveKey);
        PlayerPrefs.Save();

        if (animalRenderer != null && spriteAwal != null)
            animalRenderer.sprite = spriteAwal;

        PerbaruiMarker();
    }
}
