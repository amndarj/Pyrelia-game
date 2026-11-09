using UnityEngine;

public class TrashSortingStationInteractable : MonoBehaviour, IInteractable
{
    public TrashSortingMinigame sortingMinigame;

    public void Interact()
    {
        if (Day2MissionManager.instance == null)
        {
            Debug.LogWarning("Day2MissionManager tidak ditemukan!");
            return;
        }

        if (Day2MissionManager.instance.sampahDiambil <
            Day2MissionManager.instance.totalSampah)
        {
            Debug.Log("Kumpulkan semua 6 sampah terlebih dahulu!");
            return;
        }

        if (sortingMinigame != null)
        {
            sortingMinigame.MulaiMinigame();
        }
        else
        {
            Debug.LogWarning("TrashSortingMinigame belum dihubungkan!");
        }

        if (Day2MissionManager.instance != null &&
            Day2MissionManager.instance.pemilahanSelesai)
        {
            Debug.Log("Misi pemilahan sampah sudah selesai!");
            return;
        }
    }
}
