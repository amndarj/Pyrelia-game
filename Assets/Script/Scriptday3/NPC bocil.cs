using UnityEngine;

public class NPCbocil : MonoBehaviour, IInteractableDay3
{
    [Header("Identitas NPC")]
    public string namaNPC = "Bocil";

    [Header("Player MC")]
    public PlayerMC playerMC;

    [Header("Education")]
    public GameObject educationPanel;

    [Header("Ending Panel")]
    public GameObject endingPanel;

    private bool dialogueStarted;
    private bool introFinished;
    private bool educationFinished;
    private bool completed;

    private const string STAGE_KEY = "Day3_Stage";
    private const string QUIZ_COMPLETED_KEY = "Day3_QuizCompleted";

    private void Start()
    {
        if (GameSaveManager.Instance != null)
        {
            GameSaveManager.Instance.SaveCurrentScene();
        }

        if (playerMC == null)
        {
            Debug.LogError("PlayerMC belum dihubungkan!");
            return;
        }

        AturNamaNPC(playerMC.openingDialogueLines);
        AturNamaNPC(playerMC.educationDialogueLines);
        AturNamaNPC(playerMC.closingDialogueLines);

        int savedStage = PlayerPrefs.GetInt(
            STAGE_KEY,
            PlayerMC.STAGE_NONE
        );

        // Belum pernah memulai Day 3.
        if (savedStage == PlayerMC.STAGE_NONE)
        {
            if (endingPanel != null)
                endingPanel.SetActive(false);

            if (educationPanel != null)
                educationPanel.SetActive(false);

            return;
        }

        // Ending sudah selesai.
        if (savedStage == PlayerMC.STAGE_COMPLETED)
        {
            TampilkanEnding();
            return;
        }

        // Dialog pembuka sedang berlangsung.
        if (savedStage == PlayerMC.STAGE_OPENING)
        {
            dialogueStarted = true;

            playerMC.ResumeOpeningDialogue(OnOpeningFinished);
            return;
        }

        // Dialog edukasi sedang berlangsung.
        if (savedStage == PlayerMC.STAGE_EDUCATION)
        {
            dialogueStarted = true;
            introFinished = true;

            if (educationPanel != null)
                educationPanel.SetActive(false);

            playerMC.ResumeEducationDialogue(
                OnEducationDialogueFinished
            );
            return;
        }

        // Kuis atau respons jawaban sedang berlangsung.
        if (savedStage == PlayerMC.STAGE_QUIZ ||
            savedStage == PlayerMC.STAGE_QUIZ_RESPONSE)
        {
            dialogueStarted = true;
            introFinished = true;
            educationFinished = true;

            if (educationPanel != null)
                educationPanel.SetActive(true);

            QuizDay3 quiz = educationPanel != null
                ? educationPanel.GetComponent<QuizDay3>()
                : null;

            if (quiz != null)
            {
                quiz.ResumeQuiz();
            }
            else
            {
                Debug.LogError(
                    "QuizDay3 tidak ditemukan pada EducationPanel!"
                );
            }

            return;
        }

        // Dialog penutup sedang berlangsung.
        if (savedStage == PlayerMC.STAGE_CLOSING)
        {
            dialogueStarted = true;
            introFinished = true;
            educationFinished = true;

            if (educationPanel != null)
                educationPanel.SetActive(false);

            playerMC.ResumeClosingDialogue(OnClosingFinished);
        }
    }

    public void Interact()
    {
        if (completed || dialogueStarted)
            return;

        if (playerMC == null)
        {
            Debug.LogError("PlayerMC belum dihubungkan!");
            return;
        }

        dialogueStarted = true;

        AturNamaNPC(playerMC.openingDialogueLines);
        AturNamaNPC(playerMC.closingDialogueLines);

        playerMC.StartOpeningDialogue(OnOpeningFinished);

        Debug.Log("Dialog pembuka dimulai!");
    }

    void AturNamaNPC(DialogueLineDay3[] lines)
    {
        if (lines == null) return;

        foreach (DialogueLineDay3 line in lines)
        {
            if (line.side == DialogueSide.Left)
                line.characterName = namaNPC;
            else
                line.characterName = playerMC.mcName;
        }
    }

    void OnOpeningFinished()
    {
        introFinished = true;

        playerMC.StartEducationDialogue(
            OnEducationDialogueFinished
        );
    }

    void OnEducationDialogueFinished()
    {
        if (educationPanel != null)
            educationPanel.SetActive(true);

        QuizDay3 quiz = educationPanel != null
            ? educationPanel.GetComponent<QuizDay3>()
            : null;

        if (quiz != null)
        {
            quiz.StartQuiz();
            Debug.Log("Quiz Day 3 dimulai!");
        }
        else
        {
            Debug.LogError(
                "QuizDay3 tidak ditemukan pada EducationPanel!"
            );
        }
    }

    public void FinishEducation()
    {
        if (!introFinished || educationFinished || completed)
            return;

        educationFinished = true;

        if (educationPanel != null)
            educationPanel.SetActive(false);

        playerMC.StartClosingDialogue(OnClosingFinished);
    }

    void OnClosingFinished()
    {
        PlayerPrefs.SetInt(
            STAGE_KEY,
            PlayerMC.STAGE_COMPLETED
        );

        PlayerPrefs.SetInt(QUIZ_COMPLETED_KEY, 1);
        PlayerPrefs.Save();

        TampilkanEnding();
    }

    void TampilkanEnding()
    {
        completed = true;

        Debug.Log("Misi Day 3 selesai!");

        if (educationPanel != null)
            educationPanel.SetActive(false);

        if (endingPanel != null)
            endingPanel.SetActive(true);
        else
            Debug.LogError("Ending Panel belum dihubungkan!");
    }
}
