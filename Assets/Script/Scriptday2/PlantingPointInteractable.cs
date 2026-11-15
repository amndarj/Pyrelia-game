using UnityEngine;

public class PlantingPointInteractable : MonoBehaviour, IInteractable
{
    [Header("Pengaturan Menanam")]
    public PlantingMiniGame plantingMiniGame;
    public GameObject pohonHasilTanam;

    [Header("Objective Marker")]
    public GameObject objectiveMarker;

    private bool sudahDitanam = false;

    private string SaveKey =>
        "Day2_Planting_" + gameObject.name;

    private void Start()
    {
        MuatStatusPenanaman();
        PerbaruiMarker();
    }

    public void Interact()
    {
        if (Day2MissionManager.instance == null ||
            !Day2MissionManager.instance.BolehTanamPohon)
            return;

        if (sudahDitanam)
        {
            Debug.Log("Pohon di titik ini sudah ditanam!");
            return;
        }

        if (plantingMiniGame != null)
        {
            plantingMiniGame.MulaiMenanam(pohonHasilTanam, this);
        }
        else
        {
            Debug.LogError("Planting Mini Game belum dihubungkan!");
        }
    }

    public void SelesaikanPenanaman()
    {
        sudahDitanam = true;

        if (pohonHasilTanam != null)
            pohonHasilTanam.SetActive(true);

        PlayerPrefs.SetInt(SaveKey, 1);
        PlayerPrefs.Save();

        PerbaruiMarker();

        Debug.Log("Penanaman di " + gameObject.name + " selesai!");
    }


    public void PerbaruiMarker()
    {
        if (objectiveMarker == null)
            return;

        objectiveMarker.SetActive(
            Day2MissionManager.instance != null &&
            Day2MissionManager.instance.BolehTanamPohon &&
            !sudahDitanam
        );
    }

    private void MuatStatusPenanaman()
    {
        sudahDitanam =
            PlayerPrefs.GetInt(SaveKey, 0) == 1;

        if (pohonHasilTanam != null)
            pohonHasilTanam.SetActive(sudahDitanam);
    }

    public void ResetStatusPenanaman()
    {
        sudahDitanam = false;
        PlayerPrefs.DeleteKey(SaveKey);

        if (pohonHasilTanam != null)
            pohonHasilTanam.SetActive(false);

        PerbaruiMarker();
    }
}
