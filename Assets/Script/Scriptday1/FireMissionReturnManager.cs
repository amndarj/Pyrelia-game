using UnityEngine;

public class FireMissionReturnManager : MonoBehaviour
{
    [Header("NPC")]
    public GameObject gokilNPC;
    public GameObject firefighterNPC;

    [Header("Forest Map")]
    public GameObject forestBurning;
    public GameObject forestBurned;

    [Header("Objective")]
    public MissionObjective missionObjective;

    [Header("Ending Day 1")]
    public GameObject day1CompletePanel;

    private void Start()
    {
        bool teleponSelesai =
            PlayerPrefs.GetInt("Day1_TeleponDamkarSelesai", 0) == 1;

        bool keputusanDamkar =
            PlayerPrefs.GetInt("Day1_KeputusanDamkar", 0) == 1;

        bool pemadamanSelesai =
            PlayerPrefs.GetInt("MisiPemadamanSelesai", 0) == 1;

        bool evakuasiSelesai =
            PlayerPrefs.GetInt("MisiEvakuasiGokilSelesai", 0) == 1;

        bool pertolonganSelesai =
            PlayerPrefs.GetInt("MisiPertolonganSelesai", 0) == 1;

        // Kondisi hutan.
        if (forestBurning != null)
            forestBurning.SetActive(!pemadamanSelesai);

        if (forestBurned != null)
            forestBurned.SetActive(pemadamanSelesai);

        // Tampilkan NPC Damkar hanya saat sudah ditelepon
        // dan keputusan pemadaman belum dibuat.
        if (firefighterNPC != null)
        {
            firefighterNPC.SetActive(
                teleponSelesai &&
                !keputusanDamkar &&
                !pemadamanSelesai
            );
        }

        // NPC Gokil baru aktif setelah pemadaman selesai.
        if (gokilNPC != null)
        {
            gokilNPC.SetActive(
                pemadamanSelesai && !evakuasiSelesai
            );
        }

        // Objective mengikuti urutan progres.
        if (!teleponSelesai)
        {
            UpdateObjective(
                "Pergi ke rumah dan hubungi petugas pemadam kebakaran."
            );
        }
        else if (!pemadamanSelesai && !keputusanDamkar)
        {
            UpdateObjective(
                "Kembali ke hutan dan temui petugas Damkar."
            );
        }
        else if (!pemadamanSelesai)
        {
            UpdateObjective(
                "Selesaikan misi pemadaman api di hutan."
            );
        }
        else if (!evakuasiSelesai)
        {
            UpdateObjective(
                "Himbau warga yang merekam di hutan untuk pergi ke posko pengungsian."
            );
        }
        else if (!pertolonganSelesai)
        {
            UpdateObjective(
                "Temui anak SMA yang terluka dan berikan pertolongan pertama."
            );
        }
        else
        {
            UpdateObjective(
                "Semua misi Day 1 berhasil diselesaikan!"
            );

            if (day1CompletePanel != null)
                day1CompletePanel.SetActive(true);

            Debug.Log("DAY 1 COMPLETED!");
        }

        Debug.Log(
            "STATUS DAY 1: Telepon = " + teleponSelesai +
            ", Keputusan = " + keputusanDamkar +
            ", Pemadaman = " + pemadamanSelesai +
            ", Evakuasi = " + evakuasiSelesai +
            ", Pertolongan = " + pertolonganSelesai
        );
    }

    private void UpdateObjective(string objective)
    {
        if (missionObjective != null)
        {
            missionObjective.UpdateObjective(objective);
        }
        else
        {
            Debug.LogWarning(
                "MissionObjective belum dihubungkan!"
            );
        }
    }
}
