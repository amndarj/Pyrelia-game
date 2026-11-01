
using UnityEngine;

public class NPCMaskerInteractable : MonoBehaviour, IInteractable
{
    [Header("Pengaturan NPC")]
    public string npcName = "Warga";
    public DialogueUIDay2 dialogueUI;

    [TextArea(2, 4)]
    public string[] dialogueLines;

    [Header("Sprite NPC")]
    public SpriteRenderer npcRenderer;
    public Sprite npcMaskerSprite;

    [Header("Portrait NPC")]
    public Sprite npcPortrait;

    private int currentLine = 0;
    private bool dialogueStarted = false;
    private bool sudahMenerimaMasker = false;

    public void Interact()
    {
        if (sudahMenerimaMasker)
            return;

        if (dialogueUI == null)
        {
            Debug.LogError("Dialogue UI belum dihubungkan!");
            return;
        }

        // Mulai percakapan
        if (!dialogueStarted)
        {
            if (dialogueLines == null || dialogueLines.Length == 0)
            {
                Debug.LogWarning("Dialog NPC belum diisi!");
                return;
            }

            dialogueStarted = true;
            currentLine = 0;

            dialogueUI.SetPortrait(npcPortrait);
            dialogueUI.StartDialogue(npcName, dialogueLines);
            return;
        }

        // Lanjutkan percakapan
        currentLine++;

        if (currentLine < dialogueLines.Length)
        {
            dialogueUI.dialogueText.text =
                dialogueLines[currentLine];
        }
        else
        {
            dialogueUI.HideDialogue();
            dialogueStarted = false;

            BerikanMasker();
        }
    }

    private void BerikanMasker()
    {
        if (Day2MissionManager.instance == null)
        {
            Debug.LogError("Day2MissionManager tidak ditemukan!");
            return;
        }

        if (Day2MissionManager.instance.maskerDimiliki <= 0)
        {
            Debug.Log("Masker belum dimiliki. Ambil di posko dahulu.");
            return;
        }

        Day2MissionManager.instance.maskerDimiliki--;
        Day2MissionManager.instance.CatatPembagianMasker();

        if (npcRenderer != null && npcMaskerSprite != null)
        {
            npcRenderer.sprite = npcMaskerSprite;
            Debug.Log("Sprite " + npcName + " berhasil berubah!");
        }
        else
        {
            Debug.LogError(
            "Sprite gagal berubah! Periksa Npc Renderer dan Npc Masker Sprite."
            );
        }

        sudahMenerimaMasker = true;

        Debug.Log("Masker diberikan kepada " + npcName);
        Debug.Log("Masker tersisa: " +
            Day2MissionManager.instance.maskerDimiliki);
    }
}
