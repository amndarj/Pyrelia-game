using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class DialogueLine
{
    public string speakerName;

    [TextArea(2, 4)]
    public string dialogueText;

    public Sprite speakerImage;
}

public class DialogueUI : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public GameObject questObjectiveText;
    public Image speakerImage;

    private string[] dialogueLines;
    private DialogueLine[] customDialogueLines;

    private int currentLine = 0;
    private bool usingCustomDialogue = false;

    public event Action OnDialogueFinished;

    public bool IsDialogueActive()
    {
        return dialoguePanel.activeSelf;
    }

    // Dialog biasa untuk NPC
    public void StartDialogue(string npcName, string[] lines)
    {
        dialogueLines = lines;
        currentLine = 0;
        usingCustomDialogue = false;

        dialoguePanel.SetActive(true);
        nameText.text = npcName;
        dialogueText.text = dialogueLines[currentLine];

        if (speakerImage != null)
            speakerImage.gameObject.SetActive(false);
    }

    // Dialog khusus dengan gambar pembicara
    public void StartDialogue(DialogueLine[] lines)
    {
        customDialogueLines = lines;
        currentLine = 0;
        usingCustomDialogue = true;

        dialoguePanel.SetActive(true);
        ShowCurrentLine();
    }

    private void ShowCurrentLine()
    {
        if (!usingCustomDialogue)
            return;

        DialogueLine line = customDialogueLines[currentLine];

        nameText.text = line.speakerName;
        dialogueText.text = line.dialogueText;

        if (speakerImage != null)
        {
            speakerImage.gameObject.SetActive(line.speakerImage != null);
            speakerImage.sprite = line.speakerImage;
        }
    }

    public void NextLine()
    {
        currentLine++;

        if (usingCustomDialogue)
        {
            if (currentLine < customDialogueLines.Length)
            {
                ShowCurrentLine();
            }
            else
            {
                HideDialogue();
                OnDialogueFinished?.Invoke();
            }
        }
        else
        {
            if (currentLine < dialogueLines.Length)
            {
                dialogueText.text = dialogueLines[currentLine];
            }
            else
            {
                HideDialogue();
                OnDialogueFinished?.Invoke();
            }
        }
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}