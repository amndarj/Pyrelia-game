
using UnityEngine;

public class ForestTrigger : MonoBehaviour
{
    public MissionObjectiveDay3 missionObjective;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Ada objek masuk trigger: " + other.name);

        if (triggered) return;

        // Mendeteksi PlayerMC meskipun collider berada di child object
        PlayerMC player = other.GetComponentInParent<PlayerMC>();

        if (player != null)
        {
            triggered = true;

            Debug.Log("MC terdeteksi masuk hutan!");

            if (missionObjective != null)
            {
                missionObjective.ShowObjective(1);
            }
            else
            {
                Debug.LogError("Mission Objective belum dihubungkan!");
            }
        }
    }
}
