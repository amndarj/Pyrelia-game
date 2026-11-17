
using UnityEngine;

public class ForestMissionTrigger : MonoBehaviour
{
    public MissionObjective missionObjective;

    private bool missionStarted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        bool teleponSelesai =
            PlayerPrefs.GetInt("Day1_TeleponDamkarSelesai", 0) == 1;

        bool pemadamanSelesai =
            PlayerPrefs.GetInt("MisiPemadamanSelesai", 0) == 1;

        // Jangan mengubah objective jika telepon sudah selesai
        // atau pemadaman sudah selesai.
        if (teleponSelesai || pemadamanSelesai)
            return;

        if (missionStarted)
            return;

        missionStarted = true;

        if (missionObjective != null)
        {
            missionObjective.UpdateObjective(
                "Pergi ke rumah dan hubungi petugas pemadam kebakaran."
            );
        }
    }
}
