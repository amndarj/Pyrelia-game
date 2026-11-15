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

        // Pemain harus memiliki masker sebelum berbicara.
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

        dialogueUI.StartDialogue(
            this,
            npcName,
            dialogueLines,
            npcPortrait
        );

        Debug.Log("Dialog dimulai oleh NPC: " + npcName);
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

        // Kurangi masker yang dimiliki pemain.
        manager.maskerDimiliki--;

        // Tandai NPC sudah menerima masker.
        sudahMenerimaMasker = true;

        // Ganti sprite NPC.
        npcRenderer.sprite = npcMaskerSprite;

        // Simpan status NPC.
        SimpanStatusNPC();

        // Catat pembagian masker pada Mission Manager.
        manager.CatatPembagianMasker();

        // Perbarui marker seluruh NPC.
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
