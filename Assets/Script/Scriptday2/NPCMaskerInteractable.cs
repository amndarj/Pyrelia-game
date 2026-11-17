using UnityEngine;

public class NPCMaskerInteractable : MonoBehaviour, IInteractable
{
    [Header("Pengaturan NPC")]
    public string npcName = "Warga";
    public DialogueUIDay2 dialogueUI;

    [TextArea(2, 4)]
    public string[] dialogueLines;

    [Header("Pengaturan Dialog Player")]
    public string playerName = "Aku";
    public Sprite playerPortrait;

    [Tooltip("Centang jika baris tersebut diucapkan Player.")]
    public bool[] playerSpeakingLines;

    [Header("Sprite NPC")]
    public SpriteRenderer npcRenderer;
    public Sprite npcMaskerSprite;

    [Header("Portrait NPC")]
    public Sprite npcPortrait;

    [Header("Objective Marker")]
    public GameObject objectiveMarker;

    private int currentLine = 0;
    private bool dialogueStarted = false;
    private bool sudahMenerimaMasker = false;

    private Sprite spriteAwal;

    private string SaveKey =>
        "Day2_NPC_Masker_" + gameObject.name;

    private void Awake()
    {
        if (npcRenderer != null)
            spriteAwal = npcRenderer.sprite;
    }

    private void Start()
    {
        MuatStatusNPC();
    }

    public void Interact()
    {
        if (sudahMenerimaMasker || dialogueStarted)
            return;

        if (Day2MissionManager.instance == null)
            return;

        if (!Day2MissionManager.instance.BolehInteraksiMasker)
            return;

        if (Day2MissionManager.instance.maskerDimiliki <= 0)
            return;

        if (dialogueUI == null ||
            dialogueLines == null ||
            dialogueLines.Length == 0)
        {
            Debug.LogWarning("Dialog NPC belum lengkap!");
            return;
        }

        dialogueStarted = true;
        currentLine = 0;

        dialogueUI.StartDialogue(this);

        Debug.Log("Dialog dimulai oleh NPC: " + npcName);
    }

    public void TampilkanBarisDialog(int index)
    {
        if (index < 0 || index >= dialogueLines.Length)
            return;

        bool playerSpeaking =
            playerSpeakingLines != null &&
            index < playerSpeakingLines.Length &&
            playerSpeakingLines[index];

        string speakerName = playerSpeaking
            ? playerName
            : npcName;

        Sprite portrait = playerSpeaking
            ? playerPortrait
            : npcPortrait;

        dialogueUI.TampilkanBaris(
            speakerName,
            dialogueLines[index],
            portrait
        );
    }

    public void NextDialogueLine()
    {
        if (!dialogueStarted)
            return;

        currentLine++;

        if (currentLine < dialogueLines.Length)
        {
            TampilkanBarisDialog(currentLine);
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
        Day2MissionManager manager =
            Day2MissionManager.instance;

        if (manager == null ||
            !manager.BolehInteraksiMasker ||
            manager.maskerDimiliki <= 0 ||
            manager.maskerDibagikan >= manager.totalMasker)
        {
            Debug.LogWarning("Masker tidak tersedia!");
            return;
        }

        if (npcRenderer == null || npcMaskerSprite == null)
        {
            Debug.LogError("Sprite NPC belum dihubungkan!");
            return;
        }

        manager.maskerDimiliki--;

        sudahMenerimaMasker = true;
        npcRenderer.sprite = npcMaskerSprite;

        SimpanStatusNPC();

        manager.CatatPembagianMasker();
        manager.PerbaruiSemuaMarkerNPC();

        Debug.Log(npcName + " sudah menerima masker!");
    }

    public void PerbaruiMarker()
    {
        if (objectiveMarker == null)
            return;

        Day2MissionManager manager =
            Day2MissionManager.instance;

        bool tampilkan =
            manager != null &&
            manager.BolehTampilkanMarkerNPC &&
            !sudahMenerimaMasker;

        if (objectiveMarker.activeSelf != tampilkan)
            objectiveMarker.SetActive(tampilkan);
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
        dialogueStarted = false;
        currentLine = 0;

        PlayerPrefs.DeleteKey(SaveKey);

        if (npcRenderer != null && spriteAwal != null)
            npcRenderer.sprite = spriteAwal;

        PerbaruiMarker();
    }
}
