
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

        // Atur nama NPC pada dialog pembuka dan penutup
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
        playerMC.StartEducationDialogue(OnEducationDialogueFinished);
    }

    void OnEducationDialogueFinished()
    {
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
            Debug.LogError("QuizDay3 tidak ditemukan pada EducationPanel!");
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
        completed = true;

        Debug.Log("Misi Day 3 selesai!");

        if (endingPanel != null)
        {
            endingPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Ending Panel belum dihubungkan!");
        }
    }
}
