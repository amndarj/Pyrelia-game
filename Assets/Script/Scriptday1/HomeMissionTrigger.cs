
using UnityEngine;

public class HomeMissionTrigger : MonoBehaviour
{
    private const string PHONE_CALL_KEY =
        "Day1_TeleponDamkarSelesai";

    public MissionObjective missionObjective;
    public PhoneCallDialogue phoneCallDialogue;
    public DialogueUI dialogueUI;
    public GameObject firefighterNPC;

    private bool phoneCallStarted = false;
    private bool phoneCallFinished = false;

    private void Awake()
    {
        phoneCallFinished =
            PlayerPrefs.GetInt(PHONE_CALL_KEY, 0) == 1;
    }

    private void OnEnable()
    {
        if (dialogueUI != null)
            dialogueUI.OnDialogueFinished += OnPhoneCallFinished;
    }

    private void OnDisable()
    {
        if (dialogueUI != null)
            dialogueUI.OnDialogueFinished -= OnPhoneCallFinished;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (phoneCallStarted || phoneCallFinished)
            return;

        if (phoneCallDialogue == null)
        {
            Debug.LogError("PhoneCallDialogue belum dihubungkan!");
            return;
        }

        phoneCallStarted = true;
        phoneCallDialogue.StartPhoneCall();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!phoneCallFinished)
        {
            if (dialogueUI != null)
                dialogueUI.CloseDialogue();

            phoneCallStarted = false;
        }
    }

    private void OnPhoneCallFinished()
    {
        if (phoneCallFinished)
            return;

        phoneCallFinished = true;

        PlayerPrefs.SetInt(PHONE_CALL_KEY, 1);
        PlayerPrefs.Save();

        if (firefighterNPC != null)
            firefighterNPC.SetActive(true);

        if (missionObjective != null)
        {
            missionObjective.UpdateObjective(
                "Kembali ke hutan dan temui petugas Damkar."
            );
        }

        if (GameSaveManager.Instance != null)
            GameSaveManager.Instance.SaveCurrentScene();

        Debug.Log("Telepon Damkar selesai dan berhasil disimpan!");
    }
}
