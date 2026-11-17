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

    private NPCMaskerInteractable activeNPC;

    public bool IsDialogueActive()
    {
        return dialoguePanel != null && dialoguePanel.activeSelf;
    }

    public void StartDialogue(NPCMaskerInteractable npc)
    {
        if (npc == null)
        {
            Debug.LogError("NPC yang memulai dialog bernilai NULL!");
            return;
        }

        if (npc.dialogueLines == null || npc.dialogueLines.Length == 0)
        {
            Debug.LogWarning("Dialog belum diisi!");
            return;
        }

        activeNPC = npc;
        dialoguePanel.SetActive(true);

        npc.TampilkanBarisDialog(0);

        Debug.Log("Dialog dimulai: " + npc.npcName);
    }

    public void TampilkanBaris(
        string speakerName,
        string line,
        Sprite portrait)
    {
        if (nameText != null)
            nameText.text = speakerName;

        if (dialogueText != null)
            dialogueText.text = line;

        if (characterPortrait != null)
        {
            if (portrait != null)
            {
                characterPortrait.sprite = portrait;
                characterPortrait.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Portrait pembicara belum dihubungkan!");
            }
        }
    }

    public void NextLine()
    {
        if (activeNPC == null)
        {
            Debug.LogWarning("Tidak ada NPC yang sedang berdialog!");
            return;
        }

        activeNPC.NextDialogueLine();
    }

    public void HideDialogue()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        activeNPC = null;
    }

    public void CloseDialogue()
    {
        HideDialogue();
    }
}
