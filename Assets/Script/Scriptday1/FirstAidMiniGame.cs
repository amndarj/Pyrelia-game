
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

        if (missionObjective != null)
        {
            missionObjective.UpdateObjective(
                "Semua misi Day 1 berhasil diselesaikan!"
            );
        }

        successPanel.SetActive(true);

        Debug.Log("Pertolongan pertama berhasil!");
    }

    public void CloseMiniGame()
    {
        firstAidPanelRoot.SetActive(false);
        day1CompletePanel.SetActive(true);

        Debug.Log("DAY 1 COMPLETED!");
    }
}
