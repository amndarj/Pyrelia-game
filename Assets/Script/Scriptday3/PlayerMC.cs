
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerMC : MonoBehaviour
{
    [Header("Dialogue UI")]
    public DialogueUIDay3 dialogueUI;

    [Header("MC Identity")]
    public string mcName = "MC";
    public Sprite mcPortrait;

    [Header("Opening Conversation")]
    public DialogueLineDay3[] openingDialogueLines;

    [Header("Education Dialogue")]
    public DialogueLineDay3[] educationDialogueLines;

    [Header("Closing Conversation")]
    public DialogueLineDay3[] closingDialogueLines;

    private bool isDialogueActive;
    private Action onDialogueFinished;

    void Update()
    {
        if (!isDialogueActive)
            return;

        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            dialogueUI.NextLine();

            if (!dialogueUI.IsDialogueActive())
            {
                isDialogueActive = false;

                Action callback = onDialogueFinished;
                onDialogueFinished = null;
                callback?.Invoke();
            }
        }
    }

    // Memulai dialog pembuka
    public void StartOpeningDialogue(Action onFinished = null)
    {
        StartCustomDialogue(openingDialogueLines, onFinished);
    }

    // Memulai dialog edukasi sebelum quiz
    public void StartEducationDialogue(Action onFinished = null)
    {
        StartCustomDialogue(educationDialogueLines, onFinished);
    }

    // Memulai dialog penutup
    public void StartClosingDialogue(Action onFinished = null)
    {
        StartCustomDialogue(closingDialogueLines, onFinished);
    }

    // Menampilkan dialog berdasarkan array
    void StartCustomDialogue(
        DialogueLineDay3[] lines,
        Action onFinished)
    {
        if (dialogueUI == null ||
            lines == null ||
            lines.Length == 0)
        {
            Debug.LogWarning("Dialog belum diatur atau Dialogue UI belum dihubungkan!");
            return;
        }

        if (dialogueUI.IsDialogueActive())
            return;

        onDialogueFinished = onFinished;
        isDialogueActive = true;

        dialogueUI.StartDialogue(lines);
    }

    // Menampilkan respons otomatis setelah menjawab quiz
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

        onDialogueFinished = onFinished;
        isDialogueActive = true;

        dialogueUI.StartDialogue(responseLines);
    }
}
