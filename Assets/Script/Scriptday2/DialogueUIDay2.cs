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

    public void StartDialogue(
        NPCMaskerInteractable npc,
        string npcName,
        string[] lines,
        Sprite portrait)
    {
        if (npc == null)
        {
            Debug.LogError("NPC yang memulai dialog bernilai NULL!");
            return;
        }

        if (lines == null || lines.Length == 0)
        {
            Debug.LogWarning("Dialog belum diisi!");
            return;
        }

        activeNPC = npc;

        dialoguePanel.SetActive(true);
        nameText.text = npcName;
        dialogueText.text = lines[0];

        SetPortrait(portrait);
    }

    public void SetPortrait(Sprite portrait)
    {
        if (characterPortrait == null)
        {
            Debug.LogError("Character Portrait belum dihubungkan!");
            return;
        }

        if (portrait == null)
        {
            Debug.LogWarning("Portrait NPC kosong!");
            return;
        }

        characterPortrait.sprite = portrait;
        characterPortrait.gameObject.SetActive(true);
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
        dialoguePanel.SetActive(false);
        activeNPC = null;
    }

    public void CloseDialogue()
    {
        HideDialogue();
    }
}
