
using UnityEngine;

public class NPCGokilInteractable : MonoBehaviour, IInteractable
{
    public DialogueUI dialogueUI;
    public NPCFollowPlayer npcFollowPlayer;
    public GameObject choicePanel;

    public Sprite playerImage;
    public Sprite gokilImage;

    private bool dialogueStarted = false;
    private bool dialogueFinished = false;
    private bool decisionMade = false;

    private DialogueLine[] dialogueLines;

    private void Start()
    {
        dialogueLines = new DialogueLine[]
        {
            new DialogueLine
            {
                speakerName = "Ratna",
                dialogueText = "Apasih, ini bagus nih buat konten pasti rame pecah.",
                speakerImage = gokilImage
            },
            new DialogueLine
            {
                speakerName = "Aku",
                dialogueText = "Jangan merekam di sini. Kamu itu mengganggu petugas Damkar tau!",
                speakerImage = playerImage
            },
            new DialogueLine
            {
                speakerName = "Ratna",
                dialogueText = "Lah, aku cuma merekam doang kok. Lagian petugasnya ga protes, kenapa kamu sewot!",
                speakerImage = gokilImage
            },
            new DialogueLine
            {
                speakerName = "Aku",
                dialogueText = "Udahlah, kamu ayo ikut aku ke pos pengungsian sekarang.",
                speakerImage = playerImage
            },
            new DialogueLine
            {
                speakerName = "Ratna",
                dialogueText = "Tapi momen seperti ini jarang terjadi. Sayang kalau dilewatkan!",
                speakerImage = gokilImage
            },
            new DialogueLine
            {
                speakerName = "Aku",
                dialogueText = "Keselamatan lebih penting daripada kontenmu. Ayo, cepat ikut aku ke tempat aman!",
                speakerImage = playerImage
            },
            new DialogueLine
            {
                speakerName = "Ratna",
                dialogueText = "Alah kamu gausah sok peduli gitu deh.",
                speakerImage = gokilImage
            },
            new DialogueLine
            {
                speakerName = "Aku",
                dialogueText = "Aku beneran peduli ya. Situasinya berbahaya. Api bisa menyebar dan kamu justru menghambat proses evakuasi.",
                speakerImage = playerImage
            },
            new DialogueLine
            {
                speakerName = "Ratna",
                dialogueText = "Yaudah kamu kenapa masih disini, sana pergi ke pos pengungsian sendiri. Diriku urusanku",
                speakerImage = gokilImage
            },
            new DialogueLine
            {
                speakerName = "Aku",
                dialogueText = "Aku sudah memperingatkanmu ya! kamu kalo udah gosong ga bakal bisa merekam konten.",
                speakerImage = playerImage
            },
            new DialogueLine
            {
                speakerName = "Ratna",
                dialogueText = "Hadeh... cerewet banget. Iya-iya aku ikut.",
                speakerImage = gokilImage
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
        if (dialogueStarted || decisionMade)
            return;

        dialogueStarted = true;
        dialogueUI.StartDialogue(dialogueLines);
    }

    private void OnDialogueFinished()
    {
        if (!dialogueStarted || dialogueFinished)
            return;

        dialogueFinished = true;

        if (choicePanel != null)
            choicePanel.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (decisionMade)
            return;

        if (dialogueUI != null)
            dialogueUI.CloseDialogue();

        if (choicePanel != null)
            choicePanel.SetActive(false);

        if (!dialogueFinished)
        {
            dialogueStarted = false;
        }
    }


    public void AcceptEvacuation()
    {
        if (decisionMade)
            return;

        decisionMade = true;

        if (choicePanel != null)
            choicePanel.SetActive(false);

        if (npcFollowPlayer != null)
            npcFollowPlayer.StartFollowing();
    }

    public void DeclineEvacuation()
    {
        if (decisionMade)
            return;

        decisionMade = true;

        if (choicePanel != null)
            choicePanel.SetActive(false);
    }
}
