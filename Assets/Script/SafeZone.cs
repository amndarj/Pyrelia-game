using UnityEngine;

public class SafeZone : MonoBehaviour
{
    private bool missionCompleted;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (missionCompleted)
            return;

        NPCFollowPlayer npc = other.GetComponent<NPCFollowPlayer>();

        if (npc != null)
        {
            npc.StopFollowing();

            Animator npcAnimator = other.GetComponent<Animator>();

            if (npcAnimator != null)
            {
                npcAnimator.SetBool("IsMoving", false);
            }

            missionCompleted = true;

            Debug.Log("Misi mengantar NPC ke tempat aman selesai!");
        }
    }
}
