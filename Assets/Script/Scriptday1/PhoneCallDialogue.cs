
using UnityEngine;

public class PhoneCallDialogue : MonoBehaviour
{
    public DialogueUI dialogueUI;

    [Header("Speaker Images")]
    public Sprite playerPhoneSprite;
    public Sprite firefighterSprite;

    private DialogueLine[] dialogueLines;

    private void Awake()
    {
        dialogueLines = new DialogueLine[]
        {
            new DialogueLine
            {
                speakerName = "Aku",
                dialogueText = "Halo, Pak. Saya ingin melaporkan kebakaran hutan.",
                speakerImage = playerPhoneSprite
            },
            new DialogueLine
            {
                speakerName = "Aku",
                dialogueText = "Lokasinya berada tidak jauh dari desa xxx.",
                speakerImage = playerPhoneSprite
            },
            new DialogueLine
            {
                speakerName = "Petugas",
                dialogueText = "Baik, bisa jelaskan kondisi kebakarannya?",
                speakerImage = firefighterSprite
            },
            new DialogueLine
            {
                speakerName = "Aku",
                dialogueText = "Apinya mulai menyebar ke area pepohonan lainnya.",
                speakerImage = playerPhoneSprite
            },
            new DialogueLine
            {
                speakerName = "Petugas",
                dialogueText = "Baik, tim kami segera menuju lokasi.",
                speakerImage = firefighterSprite
            },
            new DialogueLine
            {
                speakerName = "Petugas",
                dialogueText = "Mohon tetap berada di tempat yang aman.",
                speakerImage = firefighterSprite
            },
            new DialogueLine
            {
                speakerName = "Aku",
                dialogueText = "Baik, Pak. Terima kasih.",
                speakerImage = playerPhoneSprite
            }
        };
    }

    public void StartPhoneCall()
    {
        if (dialogueUI != null)
            dialogueUI.StartDialogue(dialogueLines);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (dialogueUI != null)
            dialogueUI.CloseDialogue();
    }
}
