
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

    void Update()
    {
        if (dialogueUI == null || !dialogueUI.IsDialogueActive())
            return;

        // Keyboard tetap bisa digunakan di editor
        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            dialogueUI.NextLine();
        }
    }

    public void StartOpeningDialogue(Action onFinished = null)
    {
        StartCustomDialogue(openingDialogueLines, onFinished);
    }

    public void StartEducationDialogue(Action onFinished = null)
    {
        StartCustomDialogue(educationDialogueLines, onFinished);
    }

    public void StartClosingDialogue(Action onFinished = null)
    {
        StartCustomDialogue(closingDialogueLines, onFinished);
    }

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

        dialogueUI.StartDialogue(lines, FinishCurrentDialogue);
    }

    void FinishCurrentDialogue()
    {
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

        onDialogueFinished = onFinished;

        dialogueUI.StartDialogue(
            responseLines,
            FinishCurrentDialogue
        );
    }
}
