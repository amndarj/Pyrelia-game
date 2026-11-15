using UnityEngine;

public class PoskoMaskerInteractable : MonoBehaviour, IInteractable
{
    public int jumlahMasker = 3;
    public GameObject objectiveMarker;
    public GameObject kotakMasker;

    private bool sudahDiambil = false;

    private string SaveKey =>
        "Day2_PoskoMasker_" + gameObject.name;

    private void Start()
    {
        sudahDiambil = PlayerPrefs.GetInt(SaveKey, 0) == 1;

        if (kotakMasker != null)
            kotakMasker.SetActive(!sudahDiambil);

        PerbaruiMarker();
    }

    private void Update()
    {
        PerbaruiMarker();
    }

    private void PerbaruiMarker()
    {
        if (objectiveMarker == null ||
            Day2MissionManager.instance == null)
            return;

        bool tampilkan =
            !sudahDiambil &&
            Day2MissionManager.instance.BolehInteraksiMasker;

        if (objectiveMarker.activeSelf != tampilkan)
            objectiveMarker.SetActive(tampilkan);
    }

    public void Interact()
    {
        if (sudahDiambil) return;

        if (Day2MissionManager.instance == null) return;

        if (!Day2MissionManager.instance.BolehInteraksiMasker)
            return;

        Day2MissionManager.instance.AmbilMasker(jumlahMasker);

        sudahDiambil = true;

        if (kotakMasker != null)
            kotakMasker.SetActive(false);

        PlayerPrefs.SetInt(SaveKey, 1);
        PlayerPrefs.Save();

        PerbaruiMarker();
    }

    public void ResetStatusMasker()
    {
        sudahDiambil = false;

        PlayerPrefs.DeleteKey(SaveKey);
        PlayerPrefs.Save();

        PerbaruiMarker();
    }
}
