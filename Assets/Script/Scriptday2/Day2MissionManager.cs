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

    [Header("UI Misi Sampah")]
    public TMP_Text trashProgressText;
    public Image trashProgressFill;

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
}
