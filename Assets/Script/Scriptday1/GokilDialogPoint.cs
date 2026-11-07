
using UnityEngine;

public class GokilDialogPoint : MonoBehaviour
{
    public GokilEvacuationDialogue dialogueManager;
    public int pointIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        NPCFollowPlayer npc =
            other.GetComponentInParent<NPCFollowPlayer>();

        if (npc != null && dialogueManager != null)
        {
            dialogueManager.TryStartPointDialogue(
                pointIndex,
                npc
            );
        }
    }
}
