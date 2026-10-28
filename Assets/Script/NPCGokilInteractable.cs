
using UnityEngine;

public class NPCNarsisInteractable : MonoBehaviour, IInteractable
{
    public DialogueUI dialogueUI;
    public NPCFollowPlayer npcFollowPlayer;
    public GameObject choicePanel;

    private bool dialogueStarted;
    private bool dialogueFinished;
    private bool decisionMade;

    private string[] dialogueLines =
    {
        "Eh, tunggu! Aku lagi merekam kejadian ini.",
        "Kalian mau mengungsi ke tempat yang aman?",
        "Hmm... sepertinya aku juga harus ikut."
    };

    public void Interact()
    {
        if (decisionMade)
            return;

        if (dialogueFinished)
            return;

        if (!dialogueStarted)
        {
            dialogueStarted = true;
            dialogueUI.StartDialogue("NPC Narsis", dialogueLines);
            return;
        }

        if (dialogueUI.IsDialogueActive())
        {
            dialogueUI.NextLine();

            if (!dialogueUI.IsDialogueActive())
            {
                dialogueFinished = true;
                choicePanel.SetActive(true);
            }
        }
    }

    public void AcceptEvacuation()
    {
        if (decisionMade)
            return;

        decisionMade = true;
        choicePanel.SetActive(false);

        if (npcFollowPlayer != null)
            npcFollowPlayer.StartFollowing();
    }

    public void DeclineEvacuation()
    {
        if (decisionMade)
            return;

        decisionMade = true;
        choicePanel.SetActive(false);
    }
}
