using UnityEngine;

public class NPC_Korban : MonoBehaviour, IInteractable
{
    public DialogueUI dialogueUI;
    public ItemManager itemManager;
    public GameObject firstAidPanel;

    public SpriteRenderer npcRenderer;
    public Sprite npcBandagedSprite;

    private bool dialogueFinished = false;

    private string[] dialogueLines =
    {
        "Tolong... sakitt...",
        "Aku tadi terdorong oleh warga lain dan terjatuh.",
        "Sakit sekali... aku tidak bisa berdiri..",
        "Tolong bantu aku.."
    };

    public void Interact()
    {
        // Kalau dialog sudah selesai
        if (dialogueFinished)
        {
            if (itemManager.HasFirstAidItems())
            {
                firstAidPanel.SetActive(true);
            }
            else
            {
                Debug.Log("Cari kayu dan perban terlebih dahulu.");
            }

            return;
        }

        // Kalau dialog sedang berjalan, lanjut ke kalimat berikutnya
        if (dialogueUI.IsDialogueActive())
        {
            dialogueUI.NextLine();

            // Kalau setelah NextLine dialog sudah tidak aktif,
            // berarti semua dialog sudah selesai
            if (!dialogueUI.IsDialogueActive())
            {
                dialogueFinished = true;
                itemManager.questStarted = true;
            }
        }
        else
        {
            // Mulai dialog dari awal
            dialogueUI.StartDialogue("Anak SMA", dialogueLines);
        }
    }

    public void SetTreated()
    {
        if (npcRenderer != null && npcBandagedSprite != null)
        {
            npcRenderer.sprite = npcBandagedSprite;
        }
    }
}