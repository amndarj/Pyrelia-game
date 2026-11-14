
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
                speakerName = "Gokil",
                dialogueText = "Eh, tunggu! Aku lagi merekam kejadian kebakaran ini.",
                speakerImage = gokilImage
            },
            new DialogueLine
            {
                speakerName = "Player",
                dialogueText = "Hei! Jangan merekam di sini. Kamu mengganggu petugas Damkar!",
                speakerImage = playerImage
            },
            new DialogueLine
            {
                speakerName = "Gokil",
                dialogueText = "Lah, aku cuma merekam kejadian. Ini bisa jadi dokumentasi penting, tahu!",
                speakerImage = gokilImage
            },
            new DialogueLine
            {
                speakerName = "Player",
                dialogueText = "Dokumentasi bisa dilakukan nanti. Sekarang kamu harus menjauh dari area kebakaran.",
                speakerImage = playerImage
            },
            new DialogueLine
            {
                speakerName = "Gokil",
                dialogueText = "Tapi momen seperti ini jarang terjadi. Sayang kalau dilewatkan!",
                speakerImage = gokilImage
            },
            new DialogueLine
            {
                speakerName = "Player",
                dialogueText = "Keselamatan lebih penting daripada rekamanmu. Ayo, ikut aku ke tempat aman!",
                speakerImage = playerImage
            },
            new DialogueLine
            {
                speakerName = "Gokil",
                dialogueText = "Kenapa harus sekarang? Aku masih mau mengambil beberapa video lagi.",
                speakerImage = gokilImage
            },
            new DialogueLine
            {
                speakerName = "Player",
                dialogueText = "Situasinya berbahaya. Api bisa menyebar dan kamu justru menghambat proses evakuasi.",
                speakerImage = playerImage
            },
            new DialogueLine
            {
                speakerName = "Gokil",
                dialogueText = "Duh, kamu ini terlalu serius. Aku bisa menjaga diriku sendiri!",
                speakerImage = gokilImage
            },
            new DialogueLine
            {
                speakerName = "Player",
                dialogueText = "Aku sudah memperingatkanmu. Sekarang pilih: ikut mengungsi atau tetap di sini?",
                speakerImage = playerImage
            },
            new DialogueLine
            {
                speakerName = "Gokil",
                dialogueText = "Hadeh... kalian memang tidak bisa diajak kompromi.",
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
