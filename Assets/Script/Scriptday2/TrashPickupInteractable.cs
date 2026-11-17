using UnityEngine;

public class TrashPickupInteractable : MonoBehaviour, IInteractable
{
    private string SaveKey =>
        "Day2_Trash_" + gameObject.name;

    private void Start()
    {
        if (PlayerPrefs.GetInt(SaveKey, 0) == 1)
        {
            gameObject.SetActive(false);
        }
    }

    public void Interact()
    {
        if (Day2MissionManager.instance == null)
            return;

        if (!Day2MissionManager.instance.BolehAmbilSampah)
            return;

        PlayerPrefs.SetInt(SaveKey, 1);
        
        if (GameSaveManager.Instance != null)
            GameSaveManager.Instance.RegisterProgressKey(SaveKey);

        PlayerPrefs.Save();

        gameObject.SetActive(false);

        Day2MissionManager.instance.CatatSampahDiambil();
        Day2MissionManager.instance.PerbaruiMarkerSampah();
    }

    public void ResetStatusSampah()
    {
        PlayerPrefs.DeleteKey(SaveKey);
        PlayerPrefs.Save();

        gameObject.SetActive(true);
    }
}
