using UnityEngine;

public class PoskoMaskerInteractable : MonoBehaviour, IInteractable
{
    public int jumlahMasker = 3;

    private bool sudahDiambil = false;

    private string SaveKey =>
        "Day2_PoskoMasker_" + gameObject.name;

    private void Start()
    {
        sudahDiambil = PlayerPrefs.GetInt(SaveKey, 0) == 1;

        gameObject.SetActive(!sudahDiambil);
    }

    public void Interact()
    {
        if (sudahDiambil)
            return;

        if (Day2MissionManager.instance == null)
            return;

        if (!Day2MissionManager.instance.BolehInteraksiMasker)
            return;

        Day2MissionManager.instance.AmbilMasker(jumlahMasker);

        sudahDiambil = true;

        PlayerPrefs.SetInt(SaveKey, 1);
        PlayerPrefs.Save();

        gameObject.SetActive(false);
    }

    public void ResetStatusMasker()
    {
        sudahDiambil = false;
        PlayerPrefs.DeleteKey(SaveKey);
        gameObject.SetActive(true);
    }
}
