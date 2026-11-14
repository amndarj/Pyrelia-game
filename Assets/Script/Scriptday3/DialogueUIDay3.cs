
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueUIDay3 : MonoBehaviour
{
    [Header("Dialogue UI")]
    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Image characterPortrait;

    [Header("NPC Portrait Settings")]
    public Vector2 npcPortraitPosition = new Vector2(-801f, 420f);
    public Vector2 npcPortraitSize = new Vector2(850f, 1027f);

    [Header("MC Portrait Settings")]
    public Vector2 mcPortraitPosition = new Vector2(801f, 420f);
    public Vector2 mcPortraitSize = new Vector2(850f, 1027f);

    [Header("MC Dialogue Text Settings")]
    public float mcRight = 0f;
    public float mcBottom = 0f;
    public float mcLeft = 0f;
    public float mcTop = 0f;

    public TextAlignmentOptions mcDialogueAlignment =
        TextAlignmentOptions.BottomRight;

    private DialogueLineDay3[] dialogueLines;
    private int currentLine;

    private Action onDialogueFinished;

    private RectTransform portraitRect;
    private RectTransform dialogueTextRect;

    // Menyimpan pengaturan awal NPC
    private Vector2 originalAnchorMin;
    private Vector2 originalAnchorMax;
    private Vector2 originalPivot;
    private Vector2 originalOffsetMin;
    private Vector2 originalOffsetMax;
    private TextAlignmentOptions originalAlignment;

    void Awake()
    {
        if (characterPortrait != null)
        {
            portraitRect = characterPortrait.GetComponent<RectTransform>();
        }

        if (dialogueText != null)
        {
            dialogueTextRect = dialogueText.GetComponent<RectTransform>();

            originalAnchorMin = dialogueTextRect.anchorMin;
            originalAnchorMax = dialogueTextRect.anchorMax;
            originalPivot = dialogueTextRect.pivot;
            originalOffsetMin = dialogueTextRect.offsetMin;
            originalOffsetMax = dialogueTextRect.offsetMax;

            originalAlignment = dialogueText.alignment;
        }
    }

    void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    public bool IsDialogueActive()
    {
        return dialoguePanel != null && dialoguePanel.activeSelf;
    }

    public void StartDialogue(
        DialogueLineDay3[] lines,
        Action onFinished = null)
    {
        if (lines == null || lines.Length == 0)
            return;

        dialogueLines = lines;
        currentLine = 0;
        onDialogueFinished = onFinished;

        dialoguePanel.SetActive(true);
        ShowCurrentLine();
    }

    void ShowCurrentLine()
    {
        DialogueLineDay3 line = dialogueLines[currentLine];

        if (nameText != null)
            nameText.text = line.characterName;

        if (dialogueText != null)
            dialogueText.text = line.dialogueText;

        // Pengaturan posisi dan ukuran kotak teks
        if (dialogueTextRect != null && dialogueText != null)
        {
            if (line.side == DialogueSide.Right)
            {
                // Anchor Stretch - Stretch
                dialogueTextRect.anchorMin = new Vector2(0f, 0f);
                dialogueTextRect.anchorMax = new Vector2(1f, 1f);

                // Pivot pojok kanan bawah
                dialogueTextRect.pivot = new Vector2(1f, 0f);

                // Pengaturan jarak setiap sisi
                dialogueTextRect.offsetMin =
                    new Vector2(mcLeft, mcBottom);

                dialogueTextRect.offsetMax =
                    new Vector2(-mcRight, -mcTop);

                // Alignment teks MC
                dialogueText.alignment = mcDialogueAlignment;
            }
            else
            {
                // Kembalikan pengaturan awal NPC
                dialogueTextRect.anchorMin = originalAnchorMin;
                dialogueTextRect.anchorMax = originalAnchorMax;
                dialogueTextRect.pivot = originalPivot;

                dialogueTextRect.offsetMin = originalOffsetMin;
                dialogueTextRect.offsetMax = originalOffsetMax;

                dialogueText.alignment = originalAlignment;
            }
        }

        // Pengaturan portrait
        if (characterPortrait == null || portraitRect == null)
            return;

        characterPortrait.sprite = line.portrait;
        characterPortrait.gameObject.SetActive(line.portrait != null);

        if (line.portrait == null)
            return;

        // Portrait NPC
        if (line.side == DialogueSide.Left)
        {
            portraitRect.sizeDelta = npcPortraitSize;
            portraitRect.anchoredPosition = npcPortraitPosition;
        }
        // Portrait MC
        else
        {
            portraitRect.sizeDelta = mcPortraitSize;
            portraitRect.anchoredPosition = mcPortraitPosition;
        }
    }

    public void NextLine()
    {
        if (!IsDialogueActive())
            return;

        currentLine++;

        if (currentLine < dialogueLines.Length)
        {
            ShowCurrentLine();
        }
        else
        {
            FinishDialogue();
        }
    }

    void FinishDialogue()
    {
        dialoguePanel.SetActive(false);

        Action callback = onDialogueFinished;
        onDialogueFinished = null;

        callback?.Invoke();
    }
}

public enum DialogueSide
{
    Left,
    Right
}

[System.Serializable]
public class DialogueLineDay3
{
    public string characterName;

    [TextArea(2, 5)]
    public string dialogueText;

    public Sprite portrait;

    public DialogueSide side;
}
