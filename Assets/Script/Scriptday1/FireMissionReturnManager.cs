
using UnityEngine;

public class FireMissionReturnManager : MonoBehaviour
{
    [Header("NPC")]
    public GameObject gokilNPC;

    [Header("Forest Map")]
    public GameObject forestBurning;
    public GameObject forestBurned;

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

        // Pergantian kondisi map hutan.
        if (forestBurning != null)
            forestBurning.SetActive(!pemadamanSelesai);

        if (forestBurned != null)
            forestBurned.SetActive(pemadamanSelesai);

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
                "Himbau warga yang merekam di hutan untuk pergi ke posko pengungsian."
            );
        }
        else if (!pertolonganSelesai)
        {
            missionObjective.UpdateObjective(
                "Perika warga yang terluka dan berikan pertolongan pertama."
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
        Debug.Log("STATUS PEMADAMAN: " + pemadamanSelesai);
        Debug.Log("FOREST BURNING: " + forestBurning.activeSelf);
        Debug.Log("FOREST BURNED: " + forestBurned.activeSelf);
    }
}
