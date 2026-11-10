using UnityEngine;

public class PlantingPointInteractable : MonoBehaviour, IInteractable
{
    [Header("Pengaturan Menanam")]
    public PlantingMiniGame plantingMiniGame;
    public GameObject pohonHasilTanam;

    private bool sudahDitanam = false;

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
        Debug.Log("Penanaman di " + gameObject.name + " selesai!");
    }
}