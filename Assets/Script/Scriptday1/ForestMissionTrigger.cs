using UnityEngine;

public class ForestMissionTrigger : MonoBehaviour
{
    public MissionObjective missionObjective;

    private bool missionStarted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (missionStarted || !other.CompareTag("Player"))
            return;

        // Jangan ulangi objektif lama setelah pemadaman selesai.
        if (PlayerPrefs.GetInt("MisiPemadamanSelesai", 0) == 1)
            return;

        missionStarted = true;

        missionObjective.UpdateObjective(
            "Kembali ke rumah dan ambil handphone."
        );
    }
}
