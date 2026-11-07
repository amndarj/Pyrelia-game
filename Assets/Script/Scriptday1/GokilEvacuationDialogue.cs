
using UnityEngine;

public class GokilEvacuationDialogue : MonoBehaviour
{
    [Header("References")]
    public NPCFollowPlayer gokilFollow;
    public DialogueUI dialogueUI;

    private int currentPoint = 0;
    private bool isDialogueActive = false;

    private string[][] dialogueLines =
    {
        new string[]
        {
            "Eh, sebentar! Aku belum sempat merekam dari sudut yang bagus!",
            "Gokil, sekarang bukan waktunya merekam. Ayo lanjut!"
        },

        new string[]
        {
            "Duh, jauh banget! Kita nggak bisa istirahat sebentar?",
            "Kita sudah hampir sampai. Sedikit lagi, Gokil!"
        },

        new string[]
        {
            "Ternyata situasinya memang seserius itu, ya...",
            "Iya. Makanya kita harus memastikan semua orang sampai di tempat aman."
        }
    };

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

    public void TryStartPointDialogue(
        int pointIndex,
        NPCFollowPlayer npc
    )
    {
        if (npc != gokilFollow)
            return;

        if (isDialogueActive || pointIndex != currentPoint)
            return;

        if (currentPoint >= dialogueLines.Length)
            return;

        if (dialogueUI == null || gokilFollow == null)
            return;

        isDialogueActive = true;

        gokilFollow.PauseFollowing();

        dialogueUI.StartDialogue(
            "Gokil",
            dialogueLines[currentPoint]
        );

        Debug.Log("Dialog Gokil titik " + (currentPoint + 1) + " dimulai.");
    }

    private void OnDialogueFinished()
    {
        if (!isDialogueActive)
            return;

        currentPoint++;
        isDialogueActive = false;

        gokilFollow.ResumeFollowing();

        Debug.Log("Gokil melanjutkan perjalanan.");
    }
}
