using UnityEngine;

public class ForestMissionTrigger : MonoBehaviour
{
    public MissionObjective missionObjective;

    private bool missionStarted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (missionStarted)
            return;

        if (other.CompareTag("Player"))
        {
            missionStarted = true;

            missionObjective.UpdateObjective(
                "Kembali ke rumah dan ambil handphone."
            );
        }
    }
}