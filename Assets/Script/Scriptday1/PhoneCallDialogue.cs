
using UnityEngine;

public class PhoneCallDialogue : MonoBehaviour
{
    public DialogueUI dialogueUI;

    public Sprite playerPhoneSprite;
    public Sprite firefighterSprite;

    private DialogueLine[] dialogueLines =
    {
        new DialogueLine
        {
            speakerName = "Player",
            dialogueText = "Halo, Pak. Saya ingin melaporkan kebakaran hutan.",
            speakerImage = null
        },
        new DialogueLine
        {
            speakerName = "Player",
            dialogueText = "Lokasinya berada tidak jauh dari desa.",
            speakerImage = null
        },
        new DialogueLine
        {
            speakerName = "Petugas Damkar",
            dialogueText = "Baik, bisa jelaskan kondisi kebakarannya?",
            speakerImage = null
        },
        new DialogueLine
        {
            speakerName = "Player",
            dialogueText = "Apinya mulai menyebar ke area pepohonan lainnya.",
            speakerImage = null
        },
        new DialogueLine
        {
            speakerName = "Petugas Damkar",
            dialogueText = "Baik, tim kami segera menuju lokasi.",
            speakerImage = null
        },
        new DialogueLine
        {
            speakerName = "Petugas Damkar",
            dialogueText = "Mohon tetap berada di tempat yang aman.",
            speakerImage = null
        },
        new DialogueLine
        {
            speakerName = "Player",
            dialogueText = "Baik, Pak. Terima kasih.",
            speakerImage = null
        }
    };

    public void StartPhoneCall()
    {
        foreach (DialogueLine line in dialogueLines)
        {
            if (line.speakerName == "Player")
                line.speakerImage = playerPhoneSprite;
            else
                line.speakerImage = firefighterSprite;
        }

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
