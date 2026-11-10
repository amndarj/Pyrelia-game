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

        // Mengubah sprite NPC menjadi sudah diperban
        npcKorban.SetTreated();

        // Menghilangkan objective lama
        questObjective.SetActive(false);

        // Mengubah objective berikutnya
        if (missionObjective != null)
        {
            missionObjective.UpdateObjective(
                "Pertolongan pertama kepada anak SMA selesai!"
            );
        }

        // Menampilkan panel keberhasilan
        successPanel.SetActive(true);

        Debug.Log("Perban berhasil dipasang!");
    }

    public void CloseMiniGame()
    {
        gameObject.SetActive(false);
    }
}
