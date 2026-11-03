
using UnityEngine;

public class HomeMissionTrigger : MonoBehaviour
{
    public MissionObjective missionObjective;
    public PhoneCallDialogue phoneCallDialogue;
    public DialogueUI dialogueUI;

    public GameObject firefighterNPC;

    private bool phoneCallStarted = false;
    private bool phoneCallFinished = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (phoneCallStarted)
            return;

        phoneCallStarted = true;
        phoneCallDialogue.StartPhoneCall();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!phoneCallFinished && dialogueUI != null)
        {
            dialogueUI.CloseDialogue();
        }
    }

    private void Update()
    {
        if (!phoneCallStarted || phoneCallFinished)
            return;

        if (!dialogueUI.IsDialogueActive())
        {
            phoneCallFinished = true;

            if (firefighterNPC != null)
                firefighterNPC.SetActive(true);

            missionObjective.UpdateObjective(
                "Kembali ke hutan dan temui petugas damkar."
            );
        }
    }
}
