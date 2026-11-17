
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
    public MissionObjective missionObjective;

    private bool dialogueStarted = false;
    private bool dialogueFinished = false;

    private DialogueLine[] dialogueLines;

    private bool EvakuasiSelesai()
    {
        return PlayerPrefs.GetInt(
            "MisiEvakuasiGokilSelesai", 0
        ) == 1;
    }

    private void Start()
    {
        dialogueFinished =
            PlayerPrefs.GetInt("MisiPertolonganSelesai", 0) == 1;

        if (dialogueFinished)
        {
            SetTreated();
            return;
        }

        dialogueLines = new DialogueLine[]
        {
            new DialogueLine
            {
                speakerName = "Fathan",
                dialogueText = "Tolong... sakitt...",
                speakerImage = npcImage
            },
            new DialogueLine
            {
                speakerName = "Aku",
                dialogueText = "Kamu kenapa? Apa yang terjadi?",
                speakerImage = playerImage
            },
            new DialogueLine
            {
                speakerName = "Fathan",
                dialogueText = "Aku tadi terdorong oleh warga lain dan tersungkur.",
                speakerImage = npcImage
            },
            new DialogueLine
            {
                speakerName = "Aku",
                dialogueText = "Kakimu terluka? Coba jangan banyak bergerak dulu.",
                speakerImage = playerImage
            },
            new DialogueLine
            {
                speakerName = "Fathan",
                dialogueText = "Sakit sekali... aku tidak bisa berdiri, bahkan aku tidak bisa menggerakkan kakiku.",
                speakerImage = npcImage
            },
            new DialogueLine
            {
                speakerName = "Aku",
                dialogueText = "Tenang, aku akan membantumu. Aku cari perlengkapan pertolongan pertama dulu.",
                speakerImage = playerImage
            },
            new DialogueLine
            {
                speakerName = "Fathan",
                dialogueText = "Terima kasih ya kak...",
                speakerImage = npcImage
            }
        };
    }

    private void OnEnable()
    {
        if (dialogueUI != null)
            dialogueUI.OnDialogueFinished += OnDialogueFinished;
    }

    private void OnDisable()
    {
        if (dialogueUI != null)
            dialogueUI.OnDialogueFinished -= OnDialogueFinished;
    }

    public void Interact()
    {
        if (!EvakuasiSelesai())
            return;

        if (dialogueFinished)
        {
            BukaFirstAidPanel();
            return;
        }

        if (!dialogueStarted)
        {
            dialogueStarted = true;
            dialogueUI.StartDialogue(dialogueLines);
            return;
        }

        if (dialogueUI.IsDialogueActive())
            dialogueUI.NextLine();
    }

    private void OnDialogueFinished()
    {
        if (!dialogueStarted || dialogueFinished)
            return;

        dialogueFinished = true;

        if (itemManager != null)
        {
            itemManager.questStarted = true;
            Debug.Log("Misi pertolongan pertama dimulai!");
        }

        if (missionObjective != null)
        {
            missionObjective.UpdateObjective(
                "Cari kayu dan perban untuk menolong Fathan."
            );
        }
    }

    public void BukaFirstAidPanel()
    {
        if (!EvakuasiSelesai() || !dialogueFinished)
            return;

        if (itemManager != null && itemManager.HasFirstAidItems())
        {
            if (firstAidPanel != null)
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
            if (dialogueUI != null)
                dialogueUI.CloseDialogue();
        }
    }

    public void SetTreated()
    {
        if (npcRenderer != null && npcBandagedSprite != null)
            npcRenderer.sprite = npcBandagedSprite;
    }
}
