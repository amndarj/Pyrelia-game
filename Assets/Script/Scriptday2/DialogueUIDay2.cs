
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUIDay2 : MonoBehaviour
{
    [Header("Pengaturan Dialog")]
    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    [Header("Portrait Karakter")]
    public Image characterPortrait;

    private string[] dialogueLines;
    private int currentLine = 0;

    public bool IsDialogueActive()
    {
        return dialoguePanel.activeSelf;
    }

    public void StartDialogue(string npcName, string[] lines)
    {
        if (lines == null || lines.Length == 0)
        {
            Debug.LogWarning("Dialog belum diisi!");
            return;
        }

        dialogueLines = lines;
        currentLine = 0;

        dialoguePanel.SetActive(true);
        nameText.text = npcName;
        dialogueText.text = dialogueLines[currentLine];
    }

    public void SetPortrait(Sprite portrait)
    {
        if (characterPortrait != null && portrait != null)
        {
            characterPortrait.sprite = portrait;
            characterPortrait.gameObject.SetActive(true);
        }
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
