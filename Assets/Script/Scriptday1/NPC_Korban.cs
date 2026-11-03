using UnityEngine;

public class NPC_Korban : MonoBehaviour, IInteractable
{
    public DialogueUI dialogueUI;
    public ItemManager itemManager;
    public GameObject firstAidPanel;

    public SpriteRenderer npcRenderer;
    public Sprite npcBandagedSprite;

    public Sprite playerImage;
    public Sprite npcImage;

    private bool dialogueStarted = false;
    private bool dialogueFinished = false;

    private DialogueLine[] dialogueLines;

    private void Start()
    {
        dialogueLines = new DialogueLine[]
        {
            new DialogueLine
            {
                speakerName = "Anak SMA",
                dialogueText = "Tolong... sakitt...",
                speakerImage = npcImage
            },
            new DialogueLine
            {
                speakerName = "Player",
                dialogueText = "Kamu kenapa? Apa yang terjadi?",
                speakerImage = playerImage
            },
            new DialogueLine
            {
                speakerName = "Anak SMA",
                dialogueText = "Aku tadi terdorong oleh warga lain dan terjatuh.",
                speakerImage = npcImage
            },
            new DialogueLine
            {
                speakerName = "Player",
                dialogueText = "Kakimu terluka? Coba jangan banyak bergerak dulu.",
                speakerImage = playerImage
            },
            new DialogueLine
            {
                speakerName = "Anak SMA",
                dialogueText = "Sakit sekali... aku tidak bisa berdiri, bahkan aku tidak bisa menggerakkan kakiku.",
                speakerImage = npcImage
            },
            new DialogueLine
            {
                speakerName = "Player",
                dialogueText = "Tenang, aku akan membantumu. Aku cari perlengkapan pertolongan pertama dulu.",
                speakerImage = playerImage
            },
            new DialogueLine
            {
                speakerName = "Anak SMA",
                dialogueText = "Terima kasih ya kak...",
                speakerImage = npcImage
            }
        };
    }

    public void Interact()
    {
        if (dialogueFinished)
            return;

        if (!dialogueStarted)
        {
            dialogueStarted = true;
            dialogueUI.StartDialogue(dialogueLines);
            return;
        }

        if (dialogueUI.IsDialogueActive())
        {
            dialogueUI.NextLine();

            if (!dialogueUI.IsDialogueActive())
            {
                dialogueFinished = true;
                itemManager.questStarted = true;
            }
        }
    }

    public void BukaFirstAidPanel()
    {
        if (!dialogueFinished)
            return;

        if (itemManager.HasFirstAidItems())
        {
            firstAidPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Cari kayu dan perban terlebih dahulu.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueUI.CloseDialogue();
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