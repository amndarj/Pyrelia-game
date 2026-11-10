using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Day2MissionManager : MonoBehaviour
{
    public static Day2MissionManager instance;

    [Header("Mission Guide")]
    public MissionGuideManager missionGuide;

    // 0 = Masker, 1 = Hewan, 2 = Sampah,
    // 3 = Pohon, 4 = Selesai
    private int misiAktif = 0;

    public bool BolehInteraksiMasker => misiAktif == 0;
    public bool BolehRawatHewan => misiAktif == 1;
    public bool BolehAmbilSampah => misiAktif == 2;
    public bool BolehTanamPohon => misiAktif == 3;

    [Header("Data Masker")]
    public int totalMasker = 3;
    public int maskerDimiliki = 0;
    public int maskerDibagikan = 0;

    [Header("Data Sampah")]
    public int totalSampah = 6;
    public int sampahDiambil = 0;
    public bool pemilahanSelesai = false;

    [Header("UI Misi Sampah")]
    public TMP_Text trashProgressText;
    public Image trashProgressFill;

    [Header("Data Penyelamatan Hewan")]
    public int totalHewan = 3;
    public int hewanDirawat = 0;

    [Header("UI Misi Hewan")]
    public AnimalMissionUI animalMissionUI;

    [Header("UI Misi Masker")]
    public TMP_Text maskProgressText;
    public Image maskProgressFill;
    public GameObject maskMissionPanel;

    [Header("Data Penanaman Pohon")]
    public int totalPohon = 3;
    public int pohonDitanam = 0;

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
        UpdateAnimalProgress();
        UpdateMaskProgress();

        TampilkanMisiAktif();
    }

    // ===== PENGATUR MISI =====

    private void TampilkanMisiAktif()
    {
        if (missionGuide == null)
        {
            Debug.LogError("MissionGuide belum dihubungkan!");
            return;
        }

        switch (misiAktif)
        {
            case 0:
                missionGuide.TampilkanMisi(
                    "MISI 1: BAGIKAN MASKER",
                    new string[]
                    {
                        "Ambil 3 masker di posko.",
                        "Berikan masker kepada warga pertama.",
                        "Berikan masker kepada warga kedua.",
                        "Berikan masker kepada warga ketiga."
                    });
                break;

            case 1:
                missionGuide.TampilkanMisi(
                    "MISI 2: RAWAT HEWAN",
                    new string[]
                    {
                        "Rawat hewan pertama: sikat, obat, dan perban.",
                        "Rawat hewan kedua: sikat, obat, dan perban.",
                        "Rawat hewan ketiga: sikat, obat, dan perban."
                    });
                break;

            case 2:
                missionGuide.TampilkanMisi(
                    "MISI 3: BERSIHKAN SAMPAH",
                    new string[]
                    {
                        "Kumpulkan 6 sampah di sekitar lingkungan.",
                        "Pilah semua sampah sesuai kategorinya."
                    });
                break;

            case 3:
                missionGuide.TampilkanMisi(
                    "MISI 4: TANAM POHON",
                    new string[]
                    {
                        "Tanam pohon pertama.",
                        "Tanam pohon kedua.",
                        "Tanam pohon ketiga."
                    });
                break;

            case 4:
                missionGuide.TampilkanMisi(
                    "SEMUA MISI SELESAI!",
                    new string[]
                    {
                        "Terima kasih telah membantu lingkungan!"
                    });
                break;
        }
    }

    private void LangkahBerikutnya()
    {
        if (missionGuide == null) return;

        bool selesai = missionGuide.LangkahBerikutnya();

        if (selesai)
        {
            misiAktif++;
            TampilkanMisiAktif();

            Debug.Log("Misi berikutnya terbuka: " + misiAktif);
        }
    }

    // ===== MISI MASKER =====

    public void AmbilMasker(int jumlah)
    {
        if (!BolehInteraksiMasker || maskerDimiliki > 0)
            return;

        maskerDimiliki += jumlah;

        Debug.Log("Berhasil mengambil masker!");

        LangkahBerikutnya();
    }

    public void CatatPembagianMasker()
    {
        if (!BolehInteraksiMasker ||
            maskerDibagikan >= totalMasker)
            return;

        maskerDibagikan++;
        UpdateMaskProgress();

        Debug.Log("Masker dibagikan: " +
            maskerDibagikan + "/" + totalMasker);

        LangkahBerikutnya();
    }

    private void UpdateMaskProgress()
    {
        if (maskProgressText != null)
            maskProgressText.text =
                "Warga Menerima Masker: " +
                maskerDibagikan + "/" + totalMasker;

        if (maskProgressFill != null && totalMasker > 0)
            maskProgressFill.fillAmount =
                Mathf.Clamp01((float)maskerDibagikan / totalMasker);
    }

    // ===== MISI HEWAN =====

    public void CatatHewanDirawat(string namaHewan)
    {
        if (!BolehRawatHewan ||
            hewanDirawat >= totalHewan)
            return;

        hewanDirawat++;

        Debug.Log(namaHewan + " berhasil dirawat!");

        UpdateAnimalProgress();
        LangkahBerikutnya();
    }

    private void UpdateAnimalProgress()
    {
        if (animalMissionUI != null)
        {
            animalMissionUI.UpdateProgress(
                hewanDirawat,
                totalHewan
            );
        }
    }

    // ===== MISI SAMPAH =====

    public void CatatSampahDiambil()
    {
        if (!BolehAmbilSampah ||
            sampahDiambil >= totalSampah)
            return;

        sampahDiambil++;
        UpdateTrashProgress();

        Debug.Log("Sampah terkumpul: " +
            sampahDiambil + "/" + totalSampah);

        if (sampahDiambil >= totalSampah)
        {
            LangkahBerikutnya();
            Debug.Log("Semua sampah berhasil dikumpulkan!");
        }
    }

    private void UpdateTrashProgress()
    {
        if (trashProgressText != null)
            trashProgressText.text =
                "Sampah Terkumpul: " +
                sampahDiambil + "/" + totalSampah;

        if (trashProgressFill != null && totalSampah > 0)
            trashProgressFill.fillAmount =
                Mathf.Clamp01((float)sampahDiambil / totalSampah);
    }

    public void CatatPemilahanSelesai()
    {
        if (!BolehAmbilSampah || pemilahanSelesai)
            return;

        if (sampahDiambil < totalSampah)
        {
            Debug.Log("Sampah belum terkumpul semua!");
            return;
        }

        pemilahanSelesai = true;

        Debug.Log("MISI PEMILAHAN SAMPAH SELESAI!");

        LangkahBerikutnya();
    }

    // ===== MISI POHON =====

    public void CatatPohonDitanam()
    {
        if (!BolehTanamPohon ||
            pohonDitanam >= totalPohon)
            return;

        pohonDitanam++;

        Debug.Log("Pohon ditanam: " +
            pohonDitanam + "/" + totalPohon);

        LangkahBerikutnya();
    }
}
