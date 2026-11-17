using UnityEngine;
using UnityEngine.SceneManagement;

public class NPC_DamkarInteractable : MonoBehaviour, IInteractable
{
    private const string PHONE_CALL_KEY =
        "Day1_TeleponDamkarSelesai";

    private const string DECISION_KEY =
        "Day1_KeputusanDamkar";

    [Header("Dialogue")]
    public DialogueUI dialogueUI;
    public GameObject choicePanel;

    [Header("Portrait")]
    public Sprite firefighterImage;

    [Header("Mini Game")]
    public string miniGameSceneName = "MiniGame_PemadamanApi";

    private bool dialogueStarted;
    private bool dialogueFinished;
    private bool decisionMade;
    private bool missionCompleted;
    private bool phoneCallFinished;

    private DialogueLine[] dialogueLines;

    private void Start()
    {
        // Ambil seluruh status tersimpan.
        phoneCallFinished =
            PlayerPrefs.GetInt(PHONE_CALL_KEY, 0) == 1;

        decisionMade =
            PlayerPrefs.GetInt(DECISION_KEY, 0) == 1;

        missionCompleted =
            PlayerPrefs.GetInt("MisiPemadamanSelesai", 0) == 1;

        // Susun dialog.
        dialogueLines = new DialogueLine[]
        {
            new DialogueLine
            {
                speakerName = "Petugas",
                dialogueText = "Hai, saya petugas pemadam kebakaran.",
                speakerImage = firefighterImage
            },
            new DialogueLine
            {
                speakerName = "Petugas",
                dialogueText = "Kami sudah menerima laporan mengenai kebakaran hutan.",
                speakerImage = firefighterImage
            },
            new DialogueLine
            {
                speakerName = "Petugas",
                dialogueText = "Kondisi api masih perlu segera ditangani.",
                speakerImage = firefighterImage
            },
            new DialogueLine
            {
                speakerName = "Petugas",
                dialogueText = "Kami akan mempersiapkan alat pemadam dan segera memadamkan api.",
                speakerImage = firefighterImage
            }
        };

        if (missionCompleted || decisionMade)
        {
            dialogueStarted = true;
            dialogueFinished = true;

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
        // Pastikan telepon sudah selesai.
        if (!phoneCallFinished)
            return;

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

        if (dialogueUI == null)
        {
            Debug.LogError("Dialogue UI belum dihubungkan!");
            return;
        }

        dialogueStarted = true;
        dialogueUI.StartDialogue(dialogueLines);
    }

    private void OnDialogueFinished()
    {
        if (!dialogueStarted ||
            dialogueFinished ||
            missionCompleted)
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
            dialogueStarted = false;
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

        if (!phoneCallFinished)
            return;

        decisionMade = true;

        // Simpan keputusan sebelum berpindah scene.
        PlayerPrefs.SetInt(DECISION_KEY, 1);
        PlayerPrefs.Save();

        if (choicePanel != null)
            choicePanel.SetActive(false);

        // Simpan checkpoint minigame.
        if (GameSaveManager.Instance != null)
        {
            GameSaveManager.Instance.SaveScene(miniGameSceneName);
        }

        Debug.Log("Memulai mini-game pemadaman api.");

        SceneManager.LoadScene(miniGameSceneName);
    }
}
