using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public GameObject questObjectiveText;

    private string[] dialogueLines;
    private int currentLine = 0;

    public bool IsDialogueActive()
    {
        return dialoguePanel.activeSelf;
    }

    public void StartDialogue(string npcName, string[] lines)
    {
        dialogueLines = lines;
        currentLine = 0;

        dialoguePanel.SetActive(true);
        nameText.text = npcName;
        dialogueText.text = dialogueLines[currentLine];
    }

    public void NextLine()
    {
        currentLine++;

        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
        }
        else
        {
            HideDialogue();
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