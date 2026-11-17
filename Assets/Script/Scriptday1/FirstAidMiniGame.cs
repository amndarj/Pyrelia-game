
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

    private bool missionCompleted;

    private bool EvakuasiSelesai()
    {
        return PlayerPrefs.GetInt("MisiEvakuasiGokilSelesai", 0) == 1;
    }

    private void Start()
    {
        missionCompleted =
            PlayerPrefs.GetInt("MisiPertolonganSelesai", 0) == 1;

        if (missionCompleted)
        {
            if (firstAidPanelRoot != null)
                firstAidPanelRoot.SetActive(false);

            return;
        }

        if (firstAidPanelRoot != null)
            firstAidPanelRoot.SetActive(false);

        ResetMiniGame();
    }

    public void BukaMiniGame()
    {
        if (!EvakuasiSelesai() || missionCompleted)
            return;

        if (firstAidPanelRoot != null)
            firstAidPanelRoot.SetActive(true);
    }

    public void PasangKayuBerhasil()
    {
        if (missionCompleted || !EvakuasiSelesai())
            return;

        if (kakiSakitImage != null)
            kakiSakitImage.SetActive(false);

        if (kakiKayuImage != null)
            kakiKayuImage.SetActive(true);

        if (kayuDrag != null)
            kayuDrag.SetActive(false);

        if (perbanDrag != null)
            perbanDrag.SetActive(true);
    }

    public void PasangPerbanBerhasil()
    {
        if (missionCompleted || !EvakuasiSelesai())
            return;

        missionCompleted = true;

        if (kakiKayuImage != null)
            kakiKayuImage.SetActive(false);

        if (kakiSelesaiImage != null)
            kakiSelesaiImage.SetActive(true);

        if (perbanDrag != null)
            perbanDrag.SetActive(false);

        if (npcKorban != null)
            npcKorban.SetTreated();

        PlayerPrefs.SetInt("MisiPertolonganSelesai", 1);
        PlayerPrefs.Save();

        if (missionObjective != null)
        {
            missionObjective.UpdateObjective(
                "Semua misi Day 1 berhasil diselesaikan!"
            );
        }

        if (successPanel != null)
            successPanel.SetActive(true);

        if (GameSaveManager.Instance != null)
            GameSaveManager.Instance.SaveScene("Day 1");
    }

    public void CloseMiniGame()
    {
        if (!missionCompleted)
            return;

        if (firstAidPanelRoot != null)
            firstAidPanelRoot.SetActive(false);

        if (day1CompletePanel != null)
            day1CompletePanel.SetActive(true);
    }

    private void ResetMiniGame()
    {
        if (kakiSakitImage != null)
            kakiSakitImage.SetActive(true);

        if (kakiKayuImage != null)
            kakiKayuImage.SetActive(false);

        if (kakiSelesaiImage != null)
            kakiSelesaiImage.SetActive(false);

        if (kayuDrag != null)
            kayuDrag.SetActive(true);

        if (perbanDrag != null)
            perbanDrag.SetActive(false);

        if (successPanel != null)
            successPanel.SetActive(false);
    }
}
