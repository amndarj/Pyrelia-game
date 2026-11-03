
using UnityEngine;

public class NPCbocil : MonoBehaviour, IInteractableDay3
{
    [Header("Player MC")]
    public PlayerMC playerMC;

    [Header("Education")]
    public GameObject educationPanel;

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

        // Mulai dialog pembuka NPC dan MC
        playerMC.StartOpeningDialogue(OnOpeningFinished);

        Debug.Log("Dialog pembuka dimulai!");
    }

    // Dipanggil setelah seluruh dialog pembuka selesai
    void OnOpeningFinished()
    {
        introFinished = true;

        // Mulai dialog edukasi MC
        playerMC.StartEducationDialogue(OnEducationDialogueFinished);

        Debug.Log("Dialog edukasi dimulai!");
    }

    // Dipanggil setelah dialog edukasi selesai
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

    // Dipanggil QuizDay3 setelah semua pertanyaan selesai
    public void FinishEducation()
    {
        if (!introFinished || educationFinished || completed)
            return;

        educationFinished = true;

        if (educationPanel != null)
            educationPanel.SetActive(false);


        // Mulai dialog penutup NPC dan MC
        playerMC.StartClosingDialogue(OnClosingFinished);

        Debug.Log("Dialog penutup dimulai!");
    }

    // Dipanggil setelah dialog penutup selesai
    void OnClosingFinished()
    {
        completed = true;
        Debug.Log("Misi Day 3 selesai!");
    }
}
