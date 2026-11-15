using UnityEngine;

public class NPCMaskerInteractable : MonoBehaviour, IInteractable
{
    [Header("Pengaturan NPC")]
    public string npcName = "Warga";
    public DialogueUIDay2 dialogueUI;

    [TextArea(2, 4)]
    public string[] dialogueLines;

    [Header("Sprite NPC")]
    public SpriteRenderer npcRenderer;
    public Sprite npcMaskerSprite;

    [Header("Portrait NPC")]
    public Sprite npcPortrait;

    private int currentLine = 0;
    private bool dialogueStarted = false;
    private bool sudahMenerimaMasker = false;

    private string SaveKey =>
        "Day2_NPC_Masker_" + gameObject.name;

    private void Start()
    {
        MuatStatusNPC();
    }

    public void Interact()
    {
        if (sudahMenerimaMasker || dialogueStarted)
            return;

        if (Day2MissionManager.instance == null ||
            !Day2MissionManager.instance.BolehInteraksiMasker)
            return;

        if (dialogueUI == null ||
            dialogueLines == null ||
            dialogueLines.Length == 0)
            return;

        dialogueStarted = true;
        currentLine = 0;

        dialogueUI.StartDialogue(
            this, npcName, dialogueLines, npcPortrait
        );
    }

    public void NextDialogueLine()
    {
        if (!dialogueStarted)
            return;

        currentLine++;

        if (currentLine < dialogueLines.Length)
        {
            dialogueUI.dialogueText.text =
                dialogueLines[currentLine];
        }
        else
        {
            dialogueStarted = false;
            dialogueUI.HideDialogue();
            BerikanMasker();
        }
    }

    private void BerikanMasker()
    {
        if (Day2MissionManager.instance == null ||
            Day2MissionManager.instance.maskerDimiliki <= 0)
        {
            Debug.LogWarning("Masker belum dimiliki!");
            return;
        }

        if (npcRenderer == null || npcMaskerSprite == null)
        {
            Debug.LogError("Sprite NPC belum dihubungkan!");
            return;
        }

        Day2MissionManager.instance.maskerDimiliki--;
        Day2MissionManager.instance.CatatPembagianMasker();

        npcRenderer.sprite = npcMaskerSprite;
        sudahMenerimaMasker = true;

        SimpanStatusNPC();

        Debug.Log(npcName + " sudah menerima masker!");
    }

    private void SimpanStatusNPC()
    {
        PlayerPrefs.SetInt(SaveKey, 1);
        PlayerPrefs.Save();
    }

    private void MuatStatusNPC()
    {
        sudahMenerimaMasker =
            PlayerPrefs.GetInt(SaveKey, 0) == 1;

        if (sudahMenerimaMasker &&
            npcRenderer != null &&
            npcMaskerSprite != null)
        {
            npcRenderer.sprite = npcMaskerSprite;
        }
    }

    public void ResetStatusNPC()
    {
        sudahMenerimaMasker = false;
        PlayerPrefs.DeleteKey(SaveKey);
    }
}
