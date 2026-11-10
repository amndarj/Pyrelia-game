using UnityEngine;

public class FirstAidMiniGame : MonoBehaviour
{
    public GameObject kakiSakitImage;
    public GameObject kakiKayuImage;
    public GameObject kakiSelesaiImage;
    public GameObject kayuDrag;
    public GameObject perbanDrag;

    public NPC_Korban npcKorban;
    public GameObject successPanel;
    public GameObject questObjective;

    public MissionObjective missionObjective;
    public GameObject firstAidPanelRoot;

    [Header("Day 1 Ending")]
    public GameObject day1CompletePanel;

    public void PasangKayuBerhasil()
    {
        kakiSakitImage.SetActive(false);
        kakiKayuImage.SetActive(true);

        kayuDrag.SetActive(false);
        perbanDrag.SetActive(true);

        Debug.Log("Kayu berhasil dipasang!");
    }

    public void PasangPerbanBerhasil()
    {
        kakiKayuImage.SetActive(false);
        kakiSelesaiImage.SetActive(true);

        perbanDrag.SetActive(false);

        npcKorban.SetTreated();

        PlayerPrefs.SetInt("MisiPertolonganSelesai", 1);
        PlayerPrefs.Save();

        questObjective.SetActive(false);

        if (missionObjective != null)
        {
            missionObjective.UpdateObjective(
                "Semua misi Day 1 berhasil diselesaikan!"
            );
        }

        // Hanya tampilkan panel keberhasilan.
        successPanel.SetActive(true);

        Debug.Log("Pertolongan pertama berhasil!");
    }

    public void CloseMiniGame()
    {
        // Tutup FirstAidPanel.
        firstAidPanelRoot.SetActive(false);

        // Baru tampilkan ending.
        day1CompletePanel.SetActive(true);

        Debug.Log("DAY 1 COMPLETED!");
    }
}
