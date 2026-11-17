using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class Day2MissionManager : MonoBehaviour
{
    public static Day2MissionManager instance;

    [Header("Mission Guide")]
    public MissionGuideManager  missionGuide;

    private int misiAktif = 0;
    private int langkahTersimpan = 0;

    private const string KEY_MISI = "Day2_MisiAktif";
    private const string KEY_LANGKAH = "Day2_Langkah";
    private const string KEY_MASKER_DIMILIKI = "Day2_MaskerDimiliki";
    private const string KEY_MASKER_DIBAGIKAN = "Day2_MaskerDibagikan";
    private const string KEY_HEWAN = "Day2_HewanDirawat";
    private const string KEY_SAMPAH = "Day2_SampahDiambil";
    private const string KEY_PEMILAHAN = "Day2_PemilahanSelesai";
    private const string KEY_POHON = "Day2_PohonDitanam";
    private const string KEY_SELESAI = "Day2_Selesai";

    public bool BolehInteraksiMasker => misiAktif == 0;
    public bool BolehRawatHewan => misiAktif == 1;
    public bool BolehAmbilSampah => misiAktif == 2;
    public bool BolehTanamPohon => misiAktif == 3;

    public bool BolehTampilkanMarkerNPC =>
    BolehInteraksiMasker && maskerDimiliki > 0;

    [Header("Day 2 Ending")]
    public GameObject day2CompletePanel;

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

    [Header("Objective Marker Sampah")]
    public GameObject trashObjectiveMarker;
    public TrashPickupInteractable[] daftarSampah;
    public TrashSortingStationInteractable trashSortingStation;
    public Vector3 trashMarkerOffset = new Vector3(0, 1f, 0);

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
        if (GameSaveManager.Instance != null)
            GameSaveManager.Instance.SaveCurrentScene();

        MuatProgres();

        UpdateTrashProgress();
        UpdateAnimalProgress();
        UpdateMaskProgress();

        TampilkanMisiAktif();

        if (missionGuide != null)
            missionGuide.SetIndexLangkah(langkahTersimpan);

        StartCoroutine(PerbaruiMarkerSetelahInisialisasi());
    }

    private IEnumerator PerbaruiMarkerSetelahInisialisasi()
    {
        yield return null;

        PerbaruiSemuaMarkerNPC();
        PerbaruiSemuaMarkerHewan();
        PerbaruiMarkerSampah();
        PerbaruiSemuaMarkerPohon();
    }

    // ===== PENYIMPANAN =====

    public void SimpanProgres()
    {
        PlayerPrefs.SetInt(KEY_MISI, misiAktif);
        PlayerPrefs.SetInt(KEY_LANGKAH,
            missionGuide != null
                ? missionGuide.GetIndexLangkah()
                : langkahTersimpan);

        PlayerPrefs.SetInt(KEY_MASKER_DIMILIKI, maskerDimiliki);
        PlayerPrefs.SetInt(KEY_MASKER_DIBAGIKAN, maskerDibagikan);
        PlayerPrefs.SetInt(KEY_HEWAN, hewanDirawat);
        PlayerPrefs.SetInt(KEY_SAMPAH, sampahDiambil);
        PlayerPrefs.SetInt(KEY_PEMILAHAN, pemilahanSelesai ? 1 : 0);
        PlayerPrefs.SetInt(KEY_POHON, pohonDitanam);
        PlayerPrefs.SetInt(KEY_SELESAI, misiAktif == 4 ? 1 : 0);

        PlayerPrefs.Save();
    }

    private void MuatProgres()
    {
        misiAktif = PlayerPrefs.GetInt(KEY_MISI, 0);
        langkahTersimpan = PlayerPrefs.GetInt(KEY_LANGKAH, 0);

        maskerDimiliki = PlayerPrefs.GetInt(KEY_MASKER_DIMILIKI, 0);
        maskerDibagikan = PlayerPrefs.GetInt(KEY_MASKER_DIBAGIKAN, 0);
        hewanDirawat = PlayerPrefs.GetInt(KEY_HEWAN, 0);
        sampahDiambil = PlayerPrefs.GetInt(KEY_SAMPAH, 0);
        pemilahanSelesai = PlayerPrefs.GetInt(KEY_PEMILAHAN, 0) == 1;
        pohonDitanam = PlayerPrefs.GetInt(KEY_POHON, 0);

        misiAktif = Mathf.Clamp(misiAktif, 0, 4);
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
                        "Cari posko kemudian ambil masker disana.",
                        "Berikan masker kepada warga yang tidak memakai masker.",
                        "Berikan masker kepada warga yang tidak memakai masker.",
                        "Berikan masker kepada warga yang tidak memakai masker."
                    });
                break;

            case 1:
                missionGuide.TampilkanMisi(
                    "MISI 2: RAWAT HEWAN",
                    new string[]
                    {
                        "Pergi ke hutan dan temukan hewan yang butuh pertolongan.",
                        "Telusuri area lain dan temukan hewan yang butuh pertolongan.",
                        "Telusuri area lain dan temukan hewan yang butuh pertolongan."
                    });
                break;

            case 2:
                missionGuide.TampilkanMisi(
                    "MISI 3: BERSIHKAN SAMPAH",
                    new string[]
                    {
                        "Kumpulkan 6 sampah di sekitar sungai.",
                        "Cari tempat sampah dan pilah sesuai kategorinya."
                    });
                break;

            case 3:
                missionGuide.TampilkanMisi(
                    "MISI 4: TANAM POHON",
                    new string[]
                    {
                        "Kembali ke hutan kemudian cari area tanam.",
                        "Telusuri area lain untuk menemukan titik tanam.",
                        "Telusuri area lain untuk menemukan titik tanam."
                    });
                break;

            case 4:
                missionGuide.TampilkanMisi(
                    "SEMUA MISI SELESAI!",
                    new string[]
                    {
                        "Terima kasih telah membantu lingkungan!"
                    });

                TampilkanPanelSelesai();

                Debug.Log("DAY 2 COMPLETED!");
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
            langkahTersimpan = 0;
            TampilkanMisiAktif();
        }

        SimpanProgres();

        Debug.Log("Misi aktif: " + misiAktif);
    }

    // ===== MISI MASKER =====

    public void AmbilMasker(int jumlah)
    {
        if (!BolehInteraksiMasker || maskerDimiliki > 0)
            return;

        maskerDimiliki += jumlah;

        Debug.Log("Berhasil mengambil masker!");

        LangkahBerikutnya();
        SimpanProgres();
        PerbaruiSemuaMarkerNPC();
    }

    public void PerbaruiSemuaMarkerNPC()
    {
        NPCMaskerInteractable[] semuaNPC =
            FindObjectsByType<NPCMaskerInteractable>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

        foreach (NPCMaskerInteractable npc in semuaNPC)
        {
            npc.PerbaruiMarker();
        }
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
        SimpanProgres();
        PerbaruiSemuaMarkerNPC();
        PerbaruiSemuaMarkerHewan();
    }

    private void UpdateMaskProgress()
    {
        if (maskProgressText != null)
            maskProgressText.text =
                "Warga Menerima Masker: " +
                maskerDibagikan + "/" + totalMasker;
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
        SimpanProgres();

        PerbaruiSemuaMarkerNPC();
        PerbaruiSemuaMarkerHewan();
        PerbaruiMarkerSampah();
    }

    private void UpdateAnimalProgress()
    {
        if (animalMissionUI != null)
            animalMissionUI.UpdateProgress(
                hewanDirawat,
                totalHewan
            );
    }

    public void PerbaruiSemuaMarkerHewan()
    {
        AnimalTreatmentInteractable[] semuaHewan =
            FindObjectsByType<AnimalTreatmentInteractable>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

        foreach (AnimalTreatmentInteractable hewan in semuaHewan)
        {
            hewan.PerbaruiMarker();
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
            LangkahBerikutnya();

        SimpanProgres();
    }

    private void UpdateTrashProgress()
    {
        if (trashProgressText != null)
            trashProgressText.text =
                "Sampah Terkumpul: " +
                sampahDiambil + "/" + totalSampah;
    }

    public void PerbaruiMarkerSampah()
    {
        if (trashObjectiveMarker == null)
            return;

        if (!BolehAmbilSampah || pemilahanSelesai)
        {
            trashObjectiveMarker.SetActive(false);
            return;
        }

        // Jika semua sampah sudah terkumpul,
        // arahkan marker ke tempat pemilahan.
        if (sampahDiambil >= totalSampah)
        {
            if (trashSortingStation != null)
            {
                trashObjectiveMarker.transform.position =
                    trashSortingStation.transform.position + trashMarkerOffset;

                trashObjectiveMarker.SetActive(true);
            }
            else
            {
                trashObjectiveMarker.SetActive(false);
                Debug.LogWarning("Trash Sorting Station belum dihubungkan!");
            }

            return;
        }

        // Jika sampah belum lengkap, cari sampah berikutnya.
        foreach (TrashPickupInteractable sampah in daftarSampah)
        {
            if (sampah != null && sampah.gameObject.activeInHierarchy)
            {
                trashObjectiveMarker.transform.position =
                    sampah.transform.position + trashMarkerOffset;

                trashObjectiveMarker.SetActive(true);

                Debug.Log("Marker menunjuk sampah: " + sampah.gameObject.name);
                return;
            }
        }

        trashObjectiveMarker.SetActive(false);
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
        SimpanProgres();
        PerbaruiMarkerSampah();
        PerbaruiSemuaMarkerPohon();
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
        SimpanProgres();
        PerbaruiSemuaMarkerPohon();
    }

        public void TampilkanPanelSelesai()
    {
        if (misiAktif == 4 && day2CompletePanel != null)
        {
            day2CompletePanel.SetActive(true);
        }
    }

    public void PerbaruiSemuaMarkerPohon()
    {
        PlantingPointInteractable[] semuaTitik =
            FindObjectsByType<PlantingPointInteractable>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

        foreach (PlantingPointInteractable titik in semuaTitik)
        {
            titik.PerbaruiMarker();
        }
    }

    // ===== RESET PROGRES =====

    
    public void ResetProgres()
    {
        // Reset data misi
        misiAktif = 0;
        langkahTersimpan = 0;

        // Reset jumlah item dan progres
        maskerDimiliki = 0;
        maskerDibagikan = 0;
        hewanDirawat = 0;
        sampahDiambil = 0;
        pemilahanSelesai = false;
        pohonDitanam = 0;

        // Hapus seluruh data tersimpan
        PlayerPrefs.DeleteKey(KEY_MISI);
        PlayerPrefs.DeleteKey(KEY_LANGKAH);
        PlayerPrefs.DeleteKey(KEY_MASKER_DIMILIKI);
        PlayerPrefs.DeleteKey(KEY_MASKER_DIBAGIKAN);
        PlayerPrefs.DeleteKey(KEY_HEWAN);
        PlayerPrefs.DeleteKey(KEY_SAMPAH);
        PlayerPrefs.DeleteKey(KEY_PEMILAHAN);
        PlayerPrefs.DeleteKey(KEY_POHON);
        PlayerPrefs.DeleteKey(KEY_SELESAI);

        // Reset tampilan mission guide
        if (missionGuide != null)
        {
            missionGuide.SetIndexLangkah(0);
            TampilkanMisiAktif();
        }

        // Reset status seluruh NPC masker
        NPCMaskerInteractable[] semuaNPC =
            FindObjectsByType<NPCMaskerInteractable>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

        foreach (NPCMaskerInteractable npc in semuaNPC)
        {
            npc.ResetStatusNPC();
        }

        // Reset status seluruh hewan
        AnimalTreatmentInteractable[] semuaHewan =
            FindObjectsByType<AnimalTreatmentInteractable>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

        foreach (AnimalTreatmentInteractable hewan in semuaHewan)
        {
            hewan.ResetStatusHewan();
        }

        // Reset seluruh sampah
        TrashPickupInteractable[] semuaSampah =
            FindObjectsByType<TrashPickupInteractable>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

        foreach (TrashPickupInteractable sampah in semuaSampah)
        {
            sampah.ResetStatusSampah();
        }

        // Reset seluruh titik penanaman
        PlantingPointInteractable[] semuaTitik =
            FindObjectsByType<PlantingPointInteractable>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

        foreach (PlantingPointInteractable titik in semuaTitik)
        {
            titik.ResetStatusPenanaman();
        }


        // Reset kotak masker di posko
        PoskoMaskerInteractable[] semuaPosko =
            FindObjectsByType<PoskoMaskerInteractable>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );

        foreach (PoskoMaskerInteractable posko in semuaPosko)
        {
            posko.ResetStatusMasker();
        }

        // Perbarui seluruh objective marker
        PerbaruiSemuaMarkerNPC();
        PerbaruiSemuaMarkerHewan();
        PerbaruiMarkerSampah();
        PerbaruiSemuaMarkerPohon();

        // Reset UI progres
        UpdateTrashProgress();
        UpdateAnimalProgress();
        UpdateMaskProgress();

        // Sembunyikan panel selesai
        if (day2CompletePanel != null)
            day2CompletePanel.SetActive(false);

        PlayerPrefs.Save();

        Debug.Log("SELURUH PROGRES DAY 2 BERHASIL DIRESET!");
    }

}
