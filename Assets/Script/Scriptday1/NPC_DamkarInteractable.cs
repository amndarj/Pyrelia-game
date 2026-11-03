using UnityEngine;

public class NPC_DamkarInteractable : MonoBehaviour, IInteractable
{
    public DialogueUI dialogueUI;
    public GameObject choicePanel;

    private bool dialogueStarted = false;
    private bool dialogueFinished = false;
    private bool decisionMade = false;

    private string[] dialogueLines =
    {
        "Halo, saya petugas pemadam kebakaran.",
        "Kami sudah menerima laporan mengenai kebakaran hutan.",
        "Kondisi api masih perlu segera ditangani.",
        "Kami akan atasi kebakarannya!"
    };

    private void OnEnable()
    {
        if (dialogueUI != null)
            dialogueUI.OnDialogueFinished += OnDialogueFinished;
    }

    private void OnDisable()
    {
        if (dialogueUI != null)
            dialogueUI.OnDialogueFinished -= OnDialogueFinished;
    }

    public void Interact()
    {
        if (dialogueStarted || decisionMade)
            return;

        dialogueStarted = true;
        dialogueUI.StartDialogue("Petugas Damkar", dialogueLines);
    }

    private void OnDialogueFinished()
    {
        if (!dialogueStarted || dialogueFinished)
            return;

        dialogueFinished = true;
        choicePanel.SetActive(true);
    }

    public void StartFirefighting()
    {
        if (decisionMade)
            return;

        decisionMade = true;
        choicePanel.SetActive(false);

        Debug.Log("Minigame pemadaman api dimulai.");
    }
}