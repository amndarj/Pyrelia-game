using UnityEngine;
using UnityEngine.SceneManagement;

public class Day2CompleteUI : MonoBehaviour
{
    public void MainLagi()
    {
        // Reset progres utama
        if (Day2MissionManager.instance != null)
            Day2MissionManager.instance.ResetProgres();

        // Reset semua NPC masker
        foreach (NPCMaskerInteractable npc in
            FindObjectsByType<NPCMaskerInteractable>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None))
        {
            npc.ResetStatusNPC();
        }

        // Reset semua hewan
        foreach (AnimalTreatmentInteractable hewan in
            FindObjectsByType<AnimalTreatmentInteractable>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None))
        {
            hewan.ResetStatusHewan();
        }

        // Reset semua sampah
        foreach (TrashPickupInteractable sampah in
            FindObjectsByType<TrashPickupInteractable>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None))
        {
            sampah.ResetStatusSampah();
        }

        // Reset semua titik tanam
        foreach (PlantingPointInteractable titik in
            FindObjectsByType<PlantingPointInteractable>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None))
        {
            titik.ResetStatusPenanaman();
        }

        // Reset kotak masker
        foreach (PoskoMaskerInteractable posko in
            FindObjectsByType<PoskoMaskerInteractable>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None))
        {
            posko.ResetStatusMasker();
        }

        // Reset progres pemilahan
        foreach (TrashSortingMinigame sorting in
            FindObjectsByType<TrashSortingMinigame>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None))
        {
            sorting.ResetSortingProgress();
        }

        PlayerPrefs.Save();

        // Muat ulang Day 2
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void LanjutDay3()
    {
        if (Day2MissionManager.instance != null)
            Day2MissionManager.instance.SimpanProgres();

        SceneManager.LoadScene("Day3");
    }
}
