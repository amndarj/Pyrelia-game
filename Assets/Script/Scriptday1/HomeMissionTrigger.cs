
using UnityEngine;

public class HomeMissionTrigger : MonoBehaviour
{
    public MissionObjective missionObjective;
    public PhoneCallDialogue phoneCallDialogue;
    public DialogueUI dialogueUI;

    public GameObject firefighterNPC;

    private bool phoneCallStarted = false;
    private bool phoneCallFinished = false;


    private void OnEnable()
    {
        if (dialogueUI != null)
            dialogueUI.OnDialogueFinished += OnPhoneCallFinished;
    }

    private void OnDisable()
    {
        if (dialogueUI != null)
            dialogueUI.OnDialogueFinished -= OnPhoneCallFinished;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;


        if (phoneCallStarted || phoneCallFinished)
            return;

        phoneCallStarted = true;
        phoneCallDialogue.StartPhoneCall();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;


        if (!phoneCallFinished)
        {
            if (dialogueUI != null)
                dialogueUI.CloseDialogue();

            phoneCallStarted = false;
        }
    }

    private void OnPhoneCallFinished()
    {
        if (phoneCallFinished)
            return;

        phoneCallFinished = true;

        if (firefighterNPC != null)
            firefighterNPC.SetActive(true);

        if (missionObjective != null)
        {
            missionObjective.UpdateObjective(
                "Kembali ke hutan dan temui petugas damkar."
            );
        }

        Debug.Log("Telepon Damkar selesai!");
    }
}
