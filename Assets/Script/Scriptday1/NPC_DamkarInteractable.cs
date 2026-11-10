
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC_DamkarInteractable : MonoBehaviour, IInteractable
{
    public DialogueUI dialogueUI;
    public GameObject choicePanel;

    [Header("Mini Game")]
    public string miniGameSceneName = "MiniGame_PemadamanApi";

    private bool dialogueStarted = false;
    private bool dialogueFinished = false;
    private bool decisionMade = false;
    private bool missionCompleted = false;

    private string[] dialogueLines =
    {
        "Halo, saya petugas pemadam kebakaran.",
        "Kami sudah menerima laporan mengenai kebakaran hutan.",
        "Kondisi api masih perlu segera ditangani.",
        "Bisakah kamu membantu kami dalam proses pemadaman?"
    };

    private void Start()
    {
        missionCompleted =
            PlayerPrefs.GetInt("MisiPemadamanSelesai", 0) == 1;

        if (missionCompleted)
        {
            dialogueStarted = true;
            decisionMade = true;

            if (choicePanel != null)
                choicePanel.SetActive(false);
        }
    }

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
        if (missionCompleted || dialogueStarted || decisionMade)
            return;

        dialogueStarted = true;
        dialogueUI.StartDialogue("Petugas Damkar", dialogueLines);
    }

    private void OnDialogueFinished()
    {
        if (!dialogueStarted || dialogueFinished || missionCompleted)
            return;

        dialogueFinished = true;

        if (choicePanel != null)
            choicePanel.SetActive(true);
    }

    public void CloseInteractionUI()
    {
        if (dialogueUI != null)
            dialogueUI.CloseDialogue();

        if (choicePanel != null)
            choicePanel.SetActive(false);
    }

    public void StartFirefighting()
    {
        if (decisionMade || missionCompleted)
            return;

        decisionMade = true;

        if (choicePanel != null)
            choicePanel.SetActive(false);

        Debug.Log("Memulai mini-game pemadaman api.");

        SceneManager.LoadScene(miniGameSceneName);
    }
}
