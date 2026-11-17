
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public MissionObjective missionObjective;

    private bool missionCompleted;

    private void Start()
    {
        missionCompleted =
            PlayerPrefs.GetInt("MisiEvakuasiGokilSelesai", 0) == 1;

        if (missionCompleted && missionObjective != null)
        {
            missionObjective.UpdateObjective(
                "Temui anak SMA yang terluka dan berikan pertolongan pertama."
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (missionCompleted)
            return;

        // Evakuasi hanya boleh dilakukan setelah pemadaman selesai.
        bool pemadamanSelesai =
            PlayerPrefs.GetInt("MisiPemadamanSelesai", 0) == 1;

        if (!pemadamanSelesai)
            return;

        NPCFollowPlayer npc = other.GetComponent<NPCFollowPlayer>();

        if (npc == null)
            return;

        npc.StopFollowing();

        Animator npcAnimator = other.GetComponent<Animator>();

        if (npcAnimator != null)
            npcAnimator.SetBool("IsMoving", false);

        missionCompleted = true;

        PlayerPrefs.SetInt("MisiEvakuasiGokilSelesai", 1);
        PlayerPrefs.Save();

        if (missionObjective != null)
        {
            missionObjective.UpdateObjective(
                "Temui anak SMA yang terluka dan berikan pertolongan pertama."
            );
        }

        if (GameSaveManager.Instance != null)
        {
            GameSaveManager.Instance.SaveScene("Day 1");
        }

        Debug.Log("Misi evakuasi Gokil selesai!");
    }
}
