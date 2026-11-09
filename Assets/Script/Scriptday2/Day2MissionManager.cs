using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Day2MissionManager : MonoBehaviour
{
    public static Day2MissionManager instance;

    [Header("Data Masker")]
    public int totalMasker = 3;
    public int maskerDimiliki = 0;
    public int maskerDibagikan = 0;

    [Header("Data Sampah")]
    public int totalSampah = 6;
    public int sampahDiambil = 0;

    [Header("Status Misi Sampah")]
    public bool pemilahanSelesai = false;

    [Header("UI Misi Sampah")]
    public TMP_Text trashProgressText;
    public Image trashProgressFill;

    [Header("Data Penyelamatan Hewan")]
    public int totalHewan = 3;
    public int hewanDirawat = 0;

    [Header("UI Misi Hewan")]
    public TMP_Text animalProgressText;
    public Image animalProgressFill;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("Day2MissionManager duplikat!");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        UpdateTrashProgress();
    }

    // ===== MISI MASKER =====

    public void AmbilMasker(int jumlah)
    {
        maskerDimiliki += jumlah;

        Debug.Log("Berhasil mengambil masker!");
        Debug.Log("Masker dimiliki: " + maskerDimiliki);
    }

    public void CatatPembagianMasker()
    {
        maskerDibagikan++;

        Debug.Log(
            "Progres pembagian masker: " +
            maskerDibagikan + "/" + totalMasker
        );

        if (maskerDibagikan >= totalMasker)
        {
            Debug.Log("MISI PEMBAGIAN MASKER SELESAI!");
        }
    }

    // ===== MISI SAMPAH =====

    public void CatatSampahDiambil()
    {
        if (sampahDiambil >= totalSampah)
            return;

        sampahDiambil++;

        UpdateTrashProgress();

        Debug.Log(
            "Progres pengumpulan sampah: " +
            sampahDiambil + "/" + totalSampah
        );

        if (sampahDiambil >= totalSampah)
        {
            Debug.Log("SEMUA SAMPAH BERHASIL DIKUMPULKAN!");
        }
    }

    private void UpdateTrashProgress()
    {
        if (trashProgressText != null)
        {
            trashProgressText.text =
                "Sampah Terkumpul: " +
                sampahDiambil + "/" + totalSampah;
        }

        if (trashProgressFill != null && totalSampah > 0)
        {
            trashProgressFill.fillAmount =
                Mathf.Clamp01((float)sampahDiambil / totalSampah);
        }
    }


    public void CatatPemilahanSelesai()
    {
        if (pemilahanSelesai)
            return;

        pemilahanSelesai = true;

        Debug.Log("MISI PEMILAHAN SAMPAH SELESAI!");
    }

    // ===== MISI PERAWATAN HEWAN =====

    public void CatatHewanDirawat(string namaHewan)
    {
        if (hewanDirawat >= totalHewan)
            return;

        hewanDirawat++;

        Debug.Log(namaHewan + " berhasil dirawat!");

        UpdateAnimalProgress();

        if (hewanDirawat >= totalHewan)
        {
            Debug.Log("SEMUA HEWAN BERHASIL DIRAWAT!");
        }
    }

    private void UpdateAnimalProgress()
    {
        if (animalProgressText != null)
        {
            animalProgressText.text =
                "Hewan Dirawat: " +
                hewanDirawat + "/" + totalHewan;
        }

        if (animalProgressFill != null && totalHewan > 0)
        {
            animalProgressFill.fillAmount =
                Mathf.Clamp01((float)hewanDirawat / totalHewan);
        }
    }
}
