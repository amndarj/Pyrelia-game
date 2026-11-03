
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

    [Header("Portrait Settings")]
    [Min(0)]
    public float portraitPadding = 40f;

    public Vector2 portraitSize = new Vector2(650f, 907f);

    private DialogueLineDay3[] dialogueLines;
    private int currentLine;

    private Action onDialogueFinished;

    private RectTransform panelRect;
    private RectTransform portraitRect;

    private float originalPortraitY;

    void Awake()
    {
        if (dialoguePanel != null)
            panelRect = dialoguePanel.GetComponent<RectTransform>();

        if (characterPortrait != null)
        {
            portraitRect = characterPortrait.GetComponent<RectTransform>();
            originalPortraitY = portraitRect.anchoredPosition.y;
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

        nameText.text = line.characterName;
        dialogueText.text = line.dialogueText;

        if (characterPortrait == null || portraitRect == null)
            return;

        characterPortrait.sprite = line.portrait;
        characterPortrait.gameObject.SetActive(line.portrait != null);

        if (line.portrait == null || panelRect == null)
            return;

        // Atur ukuran portrait
        portraitRect.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal,
            portraitSize.x
        );

        portraitRect.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Vertical,
            portraitSize.y
        );

        // Hitung posisi horizontal berdasarkan ukuran aktual
        float halfPanelWidth = panelRect.rect.width / 2f;
        float halfPortraitWidth = portraitRect.rect.width / 2f;

        float xPosition = line.side == DialogueSide.Left
            ? -halfPanelWidth + halfPortraitWidth + portraitPadding + 150f
            : halfPanelWidth - halfPortraitWidth - portraitPadding - 150f;

        // Y tetap mengikuti Inspector
        portraitRect.anchoredPosition = new Vector2(
            xPosition,
            originalPortraitY
        );
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
