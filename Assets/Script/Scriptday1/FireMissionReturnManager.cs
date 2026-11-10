using UnityEngine;

public class FireMissionReturnManager : MonoBehaviour
{
    [Header("NPC")]
    public GameObject gokilNPC;

    [Header("Pohon Hutan")]
    public Sprite normalTreeSprite;

    [Header("Objective")]
    public MissionObjective missionObjective;

    [Header("Ending Day 1")]
    public GameObject day1CompletePanel;

    private void Start()
    {
        bool pemadamanSelesai =
            PlayerPrefs.GetInt("MisiPemadamanSelesai", 0) == 1;

        bool evakuasiSelesai =
            PlayerPrefs.GetInt("MisiEvakuasiGokilSelesai", 0) == 1;

        bool pertolonganSelesai =
            PlayerPrefs.GetInt("MisiPertolonganSelesai", 0) == 1;

        // Jika pemadaman selesai, ubah pohon menjadi normal.
        if (pemadamanSelesai)
        {
            GameObject[] allTrees =
                GameObject.FindGameObjectsWithTag("PohonTerbakar");

            foreach (GameObject tree in allTrees)
            {
                SpriteRenderer renderer =
                    tree.GetComponent<SpriteRenderer>();

                if (renderer != null)
                    renderer.sprite = normalTreeSprite;
            }
        }

        // Tentukan objective berdasarkan progres terakhir.
        if (!pemadamanSelesai)
        {
            Debug.Log("Misi pemadaman api belum selesai.");
        }
        else if (!evakuasiSelesai)
        {
            if (gokilNPC != null)
                gokilNPC.SetActive(true);

            missionObjective.UpdateObjective(
                "Temui Gokil di hutan dan antarkan ke posko pengungsian."
            );
        }
        else if (!pertolonganSelesai)
        {
            missionObjective.UpdateObjective(
                "Temui anak SMA yang terluka dan berikan pertolongan pertama."
            );
        }
        else
        {
            missionObjective.UpdateObjective(
                "Semua misi Day 1 berhasil diselesaikan!"
            );

            if (day1CompletePanel != null)
                day1CompletePanel.SetActive(true);

            Debug.Log("DAY 1 COMPLETED!");
        }
    }
}
