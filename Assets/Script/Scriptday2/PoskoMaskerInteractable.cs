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
        PlayerPrefs.DeleteKey(SaveKey);
        PlayerPrefs.Save();

        sudahDiambil = false;

        if (kotakMasker != null)
            kotakMasker.SetActive(true);

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
        Debug.Log("INTERACT POSKO DIPANGGIL!");

        if (sudahDiambil)
        {
            Debug.Log("Masker sudah pernah diambil!");
            return;
        }

        if (Day2MissionManager.instance == null)
        {
            Debug.LogError("Day2MissionManager tidak ditemukan!");
            return;
        }

        Debug.Log("Boleh interaksi masker: " +
            Day2MissionManager.instance.BolehInteraksiMasker);

        if (!Day2MissionManager.instance.BolehInteraksiMasker)
        {
            Debug.LogWarning("Misi masker belum aktif!");
            return;
        }

        Day2MissionManager.instance.AmbilMasker(jumlahMasker);

        sudahDiambil = true;

        if (kotakMasker != null)
            kotakMasker.SetActive(false);

        PlayerPrefs.SetInt(SaveKey, 1);

        if (GameSaveManager.Instance != null)
            GameSaveManager.Instance.RegisterProgressKey(SaveKey);

        PlayerPrefs.Save();

        PerbaruiMarker();

        Debug.Log("MASKER BERHASIL DIAMBIL!");
    }

    public void ResetStatusMasker()
    {
        sudahDiambil = false;

        PlayerPrefs.DeleteKey(SaveKey);
        PlayerPrefs.Save();

        if (kotakMasker != null)
            kotakMasker.SetActive(true);

        PerbaruiMarker();
    }
}
