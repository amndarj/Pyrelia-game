using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC_DamkarInteractable : MonoBehaviour, IInteractable
{
    public DialogueUI dialogueUI;
    public GameObject choicePanel;

    [Header("Portrait")]
    public Sprite firefighterImage;

    [Header("Mini Game")]
    public string miniGameSceneName = "MiniGame_PemadamanApi";

    private bool dialogueStarted = false;
    private bool dialogueFinished = false;
    private bool decisionMade = false;
    private bool missionCompleted = false;

    private DialogueLine[] dialogueLines;

    private void Start()
    {
        dialogueLines = new DialogueLine[]
        {
            new DialogueLine
            {
                speakerName = "Petugas Damkar",
                dialogueText = "Halo, saya petugas pemadam kebakaran.",
                speakerImage = firefighterImage
            },
            new DialogueLine
            {
                speakerName = "Petugas Damkar",
                dialogueText = "Kami sudah menerima laporan mengenai kebakaran hutan.",
                speakerImage = firefighterImage
            },
            new DialogueLine
            {
                speakerName = "Petugas Damkar",
                dialogueText = "Kondisi api masih perlu segera ditangani.",
                speakerImage = firefighterImage
            },
            new DialogueLine
            {
                speakerName = "Petugas Damkar",
                dialogueText = "Bisakah kamu membantu kami dalam proses pemadaman?",
                speakerImage = firefighterImage
            }
        };

        missionCompleted =
            PlayerPrefs.GetInt("MisiPemadamanSelesai", 0) == 1;

        if (missionCompleted)
        {
            dialogueStarted = true;
            dialogueFinished = true;
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
        if (missionCompleted || decisionMade)
            return;

        if (dialogueFinished)
        {
            if (choicePanel != null)
                choicePanel.SetActive(true);

            return;
        }

        if (dialogueStarted)
            return;

        dialogueStarted = true;
        dialogueUI.StartDialogue(dialogueLines);
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

        if (!dialogueFinished && !decisionMade)
        {
            dialogueStarted = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        CloseInteractionUI();
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
