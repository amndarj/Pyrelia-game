using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public MissionObjective missionObjective;

    private bool missionCompleted;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (missionCompleted)
            return;

        NPCFollowPlayer npc = other.GetComponent<NPCFollowPlayer>();

        if (npc == null)
            return;

        npc.StopFollowing();

        Animator npcAnimator = other.GetComponent<Animator>();

        if (npcAnimator != null)
            npcAnimator.SetBool("IsMoving", false);

        missionCompleted = true;

        // Simpan progres evakuasi.
        PlayerPrefs.SetInt("MisiEvakuasiGokilSelesai", 1);
        PlayerPrefs.Save();

        // Ganti objektif berikutnya.
        if (missionObjective != null)
        {
            missionObjective.UpdateObjective(
                "Temui anak SMA yang terluka dan berikan pertolongan pertama."
            );
        }

        Debug.Log("Misi evakuasi Gokil selesai!");
    }
}
