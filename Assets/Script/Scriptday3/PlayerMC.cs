using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerMC : MonoBehaviour
{
    [Header("Dialogue UI")]
    public DialogueUIDay3 dialogueUI;

    [Header("MC Identity")]
    public string mcName = "Aku";
    public Sprite mcPortrait;

    [Header("Opening Conversation")]
    public DialogueLineDay3[] openingDialogueLines;

    [Header("Education Dialogue")]
    public DialogueLineDay3[] educationDialogueLines;

    [Header("Closing Conversation")]
    public DialogueLineDay3[] closingDialogueLines;

    private Action onDialogueFinished;

    // Tahapan dialog Day 3
    public const int STAGE_NONE = 0;
    public const int STAGE_OPENING = 1;
    public const int STAGE_EDUCATION = 2;
    public const int STAGE_QUIZ = 3;
    public const int STAGE_QUIZ_RESPONSE = 4;
    public const int STAGE_CLOSING = 5;
    public const int STAGE_COMPLETED = 6;

    private const string STAGE_KEY = "Day3_Stage";
    private const string LINE_KEY = "Day3_DialogueLine";

    void Update()
    {
        if (dialogueUI == null || !dialogueUI.IsDialogueActive())
            return;

        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            dialogueUI.NextLine();
        }
    }

    public void StartOpeningDialogue(Action onFinished = null)
    {
        StartCustomDialogue(
            openingDialogueLines,
            STAGE_OPENING,
            onFinished
        );
    }

    public void StartEducationDialogue(Action onFinished = null)
    {
        StartCustomDialogue(
            educationDialogueLines,
            STAGE_EDUCATION,
            onFinished
        );
    }

    public void StartClosingDialogue(Action onFinished = null)
    {
        StartCustomDialogue(
            closingDialogueLines,
            STAGE_CLOSING,
            onFinished
        );
    }

    void StartCustomDialogue(
        DialogueLineDay3[] lines,
        int stage,
        Action onFinished,
        bool resume = false)
    {
        if (dialogueUI == null ||
            lines == null ||
            lines.Length == 0)
        {
            Debug.LogWarning(
                "Dialog belum diatur atau Dialogue UI belum dihubungkan!"
            );
            return;
        }

        if (dialogueUI.IsDialogueActive())
            return;

        int startLine = 0;

        if (resume &&
            PlayerPrefs.GetInt(STAGE_KEY, STAGE_NONE) == stage)
        {
            startLine = PlayerPrefs.GetInt(LINE_KEY, 0);
        }
        else
        {
            PlayerPrefs.SetInt(STAGE_KEY, stage);
            PlayerPrefs.SetInt(LINE_KEY, 0);
            PlayerPrefs.Save();
        }

        onDialogueFinished = onFinished;

        dialogueUI.StartDialogue(
            lines,
            startLine,
            FinishCurrentDialogue,
            SaveDialogueLine
        );
    }

    void SaveDialogueLine(int lineIndex)
    {
        PlayerPrefs.SetInt(LINE_KEY, lineIndex);
        PlayerPrefs.Save();
    }

    void FinishCurrentDialogue()
    {
        PlayerPrefs.SetInt(LINE_KEY, 0);
        PlayerPrefs.Save();

        Action callback = onDialogueFinished;
        onDialogueFinished = null;

        callback?.Invoke();
    }

    public void ShowQuizResponse(
        string response,
        Action onFinished = null)
    {
        if (dialogueUI == null ||
            dialogueUI.IsDialogueActive())
            return;

        DialogueLineDay3[] responseLines =
        {
            new DialogueLineDay3
            {
                characterName = mcName,
                dialogueText = response,
                portrait = mcPortrait,
                side = DialogueSide.Right
            }
        };

        PlayerPrefs.SetInt(STAGE_KEY, STAGE_QUIZ_RESPONSE);
        PlayerPrefs.SetInt(LINE_KEY, 0);
        PlayerPrefs.Save();

        onDialogueFinished = onFinished;

        dialogueUI.StartDialogue(
            responseLines,
            0,
            FinishCurrentDialogue,
            SaveDialogueLine
        );
    }

    // Dipakai nanti untuk melanjutkan dialog dari baris tersimpan.
    public void ResumeOpeningDialogue(Action onFinished = null)
    {
        StartCustomDialogue(
            openingDialogueLines,
            STAGE_OPENING,
            onFinished,
            true
        );
    }

    public void ResumeEducationDialogue(Action onFinished = null)
    {
        StartCustomDialogue(
            educationDialogueLines,
            STAGE_EDUCATION,
            onFinished,
            true
        );
    }

    public void ResumeClosingDialogue(Action onFinished = null)
    {
        StartCustomDialogue(
            closingDialogueLines,
            STAGE_CLOSING,
            onFinished,
            true
        );
    }
}
