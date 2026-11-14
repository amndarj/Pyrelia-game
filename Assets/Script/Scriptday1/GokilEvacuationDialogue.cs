using UnityEngine;

public class GokilEvacuationDialogue : MonoBehaviour
{
    [Header("References")]
    public NPCFollowPlayer gokilFollow;
    public DialogueUI dialogueUI;

    [Header("Portrait")]
    public Sprite playerImage;
    public Sprite gokilImage;

    private int currentPoint = 0;
    private bool isDialogueActive = false;

    private DialogueLine[][] dialogueLines =
    {
        new DialogueLine[]
        {
            new DialogueLine
            {
                speakerName = "Gokil",
                dialogueText = "Eh, sebentar! Aku belum sempat merekam dari sudut yang bagus!",
                speakerImage = null
            },
            new DialogueLine
            {
                speakerName = "Player",
                dialogueText = "Gokil, sekarang bukan waktunya merekam. Ayo lanjut!",
                speakerImage = null
            }
        },

        new DialogueLine[]
        {
            new DialogueLine
            {
                speakerName = "Gokil",
                dialogueText = "Duh, jauh banget! Kita nggak bisa istirahat sebentar?",
                speakerImage = null
            },
            new DialogueLine
            {
                speakerName = "Player",
                dialogueText = "Kita sudah hampir sampai. Sedikit lagi, Gokil!",
                speakerImage = null
            }
        },

        new DialogueLine[]
        {
            new DialogueLine
            {
                speakerName = "Gokil",
                dialogueText = "Ternyata situasinya memang seserius itu, ya...",
                speakerImage = null
            },
            new DialogueLine
            {
                speakerName = "Player",
                dialogueText = "Iya. Makanya kita harus memastikan semua orang sampai di tempat aman.",
                speakerImage = null
            }
        }
    };

    private void Start()
    {
        // Isi portrait sesuai pembicara.
        foreach (DialogueLine[] point in dialogueLines)
        {
            foreach (DialogueLine line in point)
            {
                if (line.speakerName == "Gokil")
                    line.speakerImage = gokilImage;
                else
                    line.speakerImage = playerImage;
            }
        }
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
