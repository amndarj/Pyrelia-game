
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

    private const string KEY_POINT = "Day1_GokilDialogPoint";
    private const string KEY_LINE = "Day1_GokilDialogLine";
    private const string KEY_ACTIVE = "Day1_GokilDialogActive";

    private const string FIRE_KEY = "MisiPemadamanSelesai";

    private DialogueLine[][] dialogueLines =
    {
        new DialogueLine[]
        {
            new DialogueLine
            {
                speakerName = "Ratna",
                dialogueText = "Eh, sebentar! Ini bagus nih buat konten!",
                speakerImage = null
            },
            new DialogueLine
            {
                speakerName = "Aku",
                dialogueText = "Mbak, sekarang bukan waktunya merekam. Ayo lanjut!",
                speakerImage = null
            }
        },

        new DialogueLine[]
        {
            new DialogueLine
            {
                speakerName = "Ratna",
                dialogueText = "Duh, jauh banget! Kita nggak bisa istirahat sebentar?",
                speakerImage = null
            },
            new DialogueLine
            {
                speakerName = "Ratna",
                dialogueText = "Kita sudah hampir sampai. Sedikit lagi, Mbak ya ampun!",
                speakerImage = null
            }
        },

        new DialogueLine[]
        {
            new DialogueLine
            {
                speakerName = "Ratna",
                dialogueText = "Ternyata situasinya memang seserius itu, ya...",
                speakerImage = null
            },
            new DialogueLine
            {
                speakerName = "Aku",
                dialogueText = "Iya. Makanya kita harus memastikan semua orang sampai di tempat aman.",
                speakerImage = null
            }
        }
    };

    private bool PemadamanSelesai()
    {
        return PlayerPrefs.GetInt(FIRE_KEY, 0) == 1;
    }

    private void OnEnable()
    {
        if (dialogueUI != null)
        {
            dialogueUI.OnDialogueFinished += OnDialogueFinished;
            dialogueUI.OnDialogueLineChanged += OnDialogueLineChanged;
        }
    }

    private void OnDisable()
    {
        if (dialogueUI != null)
        {
            dialogueUI.OnDialogueFinished -= OnDialogueFinished;
            dialogueUI.OnDialogueLineChanged -= OnDialogueLineChanged;
        }
    }

    private void Start()
    {
        foreach (DialogueLine[] point in dialogueLines)
        {
            foreach (DialogueLine line in point)
            {
                if (line.speakerName == "Ratna")
                    line.speakerImage = gokilImage;
                else
                    line.speakerImage = playerImage;
            }
        }

        currentPoint = PlayerPrefs.GetInt(KEY_POINT, 0);

        currentPoint = Mathf.Clamp(
            currentPoint, 0, dialogueLines.Length
        );

        // Jangan pulihkan dialog sebelum pemadaman selesai.
        if (!PemadamanSelesai())
            return;

        bool savedDialogueActive =
            PlayerPrefs.GetInt(KEY_ACTIVE, 0) == 1;

        if (savedDialogueActive &&
            currentPoint < dialogueLines.Length &&
            dialogueUI != null &&
            gokilFollow != null)
        {
            int savedLine = PlayerPrefs.GetInt(KEY_LINE, 0);

            isDialogueActive = true;
            gokilFollow.PauseFollowing();

            dialogueUI.StartDialogue(
                dialogueLines[currentPoint],
                savedLine
            );

            Debug.Log("Dialog Gokil berhasil dilanjutkan.");
        }
    }

    public void TryStartPointDialogue(
        int pointIndex,
        NPCFollowPlayer npc
    )
    {
        // Kunci dialog sebelum pemadaman selesai.
        if (!PemadamanSelesai())
            return;

        if (npc != gokilFollow)
            return;

        if (isDialogueActive || pointIndex != currentPoint)
            return;

        if (currentPoint >= dialogueLines.Length)
            return;

        if (dialogueUI == null || gokilFollow == null)
            return;

        isDialogueActive = true;

        PlayerPrefs.SetInt(KEY_ACTIVE, 1);
        PlayerPrefs.SetInt(KEY_POINT, currentPoint);
        PlayerPrefs.SetInt(KEY_LINE, 0);
        PlayerPrefs.Save();

        gokilFollow.PauseFollowing();

        dialogueUI.StartDialogue(
            dialogueLines[currentPoint]
        );
    }

    private void OnDialogueLineChanged(int lineIndex)
    {
        PlayerPrefs.SetInt(KEY_POINT, currentPoint);
        PlayerPrefs.SetInt(KEY_LINE, lineIndex);
        PlayerPrefs.SetInt(KEY_ACTIVE, 1);
        PlayerPrefs.Save();
    }

    private void OnDialogueFinished()
    {
        if (!isDialogueActive)
            return;

        currentPoint++;
        isDialogueActive = false;

        PlayerPrefs.SetInt(KEY_POINT, currentPoint);
        PlayerPrefs.SetInt(KEY_LINE, 0);
        PlayerPrefs.SetInt(KEY_ACTIVE, 0);
        PlayerPrefs.Save();

        if (gokilFollow != null)
            gokilFollow.ResumeFollowing();

        Debug.Log("Dialog Gokil selesai.");
    }
}
