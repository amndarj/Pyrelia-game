using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class TrashSortingMinigame : MonoBehaviour
{
    public enum KategoriSampah
    {
        Organik,
        Anorganik,
        B3
    }

    [Header("Panel Minigame")]
    public GameObject sortingPanel;
    public GameObject closeButton;

    [Header("Komponen UI")]
    public RectTransform sortingBar;
    public RectTransform indicator;
    public Image trashImage;
    public TMP_Text currentProgressText;
    public TMP_Text resultText;

    [Header("Data 6 Sampah")]
    public Sprite[] trashSprites = new Sprite[6];
    public KategoriSampah[] trashCategories =
        new KategoriSampah[6];

    [Header("Kecepatan Indikator")]
    public float kecepatanAwal = 220f;
    public float tambahanKecepatan = 35f;
    public float kecepatanMaksimal = 450f;

    private int indexSampah = 0;
    private float posisiX;
    private float arah = 1f;
    private float kecepatanSekarang;

    private bool sedangBerjalan = false;
    private bool minigameSelesai = false;

    private float batasKiri;
    private float batasKanan;

    public bool PanelSedangTerbuka =>
        sortingPanel != null && sortingPanel.activeSelf;

    private void Start()
    {
        kecepatanSekarang = kecepatanAwal;

        if (sortingPanel != null)
            sortingPanel.SetActive(false);

        if (closeButton != null)
            closeButton.SetActive(false);
    }

    private void Update()
    {
        if (!sedangBerjalan || minigameSelesai)
            return;

        GerakkanIndikator();

        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            HentikanIndikator();
        }
    }

    public void MulaiMinigame()
    {
        if (Day2MissionManager.instance != null &&
            Day2MissionManager.instance.pemilahanSelesai)
        {
            Debug.Log("Misi pemilahan sudah selesai!");
            return;
        }

        if (trashSprites == null || trashCategories == null ||
            trashSprites.Length < 6 ||
            trashCategories.Length < 6)
        {
            Debug.LogError("Data 6 sampah belum lengkap!");
            return;
        }

        if (sortingPanel == null || sortingBar == null ||
            indicator == null || trashImage == null ||
            currentProgressText == null || resultText == null)
        {
            Debug.LogError("Referensi UI belum lengkap!");
            return;
        }

        for (int i = 0; i < 6; i++)
        {
            if (trashSprites[i] == null)
            {
                Debug.LogError("Gambar sampah Element " + i + " kosong!");
                return;
            }
        }

        sortingPanel.SetActive(true);

        if (closeButton != null)
            closeButton.SetActive(false);

        indexSampah = 0;
        kecepatanSekarang = kecepatanAwal;
        arah = 1f;
        minigameSelesai = false;

        batasKiri = -sortingBar.rect.width / 2f
                    + indicator.rect.width / 2f;

        batasKanan = sortingBar.rect.width / 2f
                     - indicator.rect.width / 2f;

        posisiX = batasKiri;

        TampilkanSampah();

        AturPosisiIndikator();

        sedangBerjalan = true;
    }

    private void GerakkanIndikator()
    {
        posisiX += arah * kecepatanSekarang *
                   Time.deltaTime;

        if (posisiX >= batasKanan)
        {
            posisiX = batasKanan;
            arah = -1f;
        }
        else if (posisiX <= batasKiri)
        {
            posisiX = batasKiri;
            arah = 1f;
        }

        AturPosisiIndikator();
    }

    private void AturPosisiIndikator()
    {
        Vector2 posisi = indicator.anchoredPosition;
        posisi.x = posisiX;
        indicator.anchoredPosition = posisi;
    }

    private void HentikanIndikator()
    {
        float posisiRelatif = posisiX - batasKiri;
        float lebarBar = batasKanan - batasKiri;

        int zonaTerpilih = Mathf.Clamp(
            Mathf.FloorToInt(
                posisiRelatif / (lebarBar / 3f)
            ),
            0,
            2
        );

        KategoriSampah kategoriTerpilih =
            (KategoriSampah)zonaTerpilih;

        if (kategoriTerpilih == trashCategories[indexSampah])
        {
            BerhasilMemilah();
        }
        else
        {
            GagalMemilah();
        }
    }

    private void BerhasilMemilah()
    {
        sedangBerjalan = false;

        resultText.text = "BENAR! Sampah berhasil dipilah!";

        indexSampah++;

        Debug.Log("Index berikutnya: " + indexSampah);

        // Semua sampah sudah selesai
        if (indexSampah >= 6)
        {
            minigameSelesai = true;

            resultText.text =
                "SELAMAT! Semua sampah berhasil dipilah!";

            currentProgressText.text = "6/6 - SELESAI!";

            if (Day2MissionManager.instance != null)
            {
                Day2MissionManager.instance.CatatPemilahanSelesai();
            }

            if (closeButton != null)
                closeButton.SetActive(true);

            Debug.Log("MISI PEMILAHAN SAMPAH SELESAI!");
            return;
        }

        // Tambah kecepatan setiap jawaban benar
        kecepatanSekarang = Mathf.Min(
            kecepatanSekarang + tambahanKecepatan,
            kecepatanMaksimal
        );

        // LANJUT KE SAMPAH BERIKUTNYA
        CancelInvoke(nameof(LanjutSampah));
        Invoke(nameof(LanjutSampah), 0.8f);
    }

    private void GagalMemilah()
    {
        resultText.text = "SALAH! Coba lagi!";

        // Indikator tetap bergerak.
        // Sampah tidak berganti.
    }

    private void LanjutSampah()
    {
        if (minigameSelesai || indexSampah >= 6)
            return;

        TampilkanSampah();

        posisiX = batasKiri;
        arah = 1f;

        AturPosisiIndikator();

        resultText.text = "";

        sedangBerjalan = true;
    }

    private void TampilkanSampah()
    {
        Debug.Log(
            "Sampah ke-" + (indexSampah + 1) +
            " | Nama: " + trashSprites[indexSampah].name +
            " | Kategori: " + trashCategories[indexSampah]
        );

        trashImage.sprite = trashSprites[indexSampah];

        currentProgressText.text =
            "Sampah " + (indexSampah + 1) + " dari 6";

        resultText.text = "";
    }

    public void TutupMinigame()
    {
        sedangBerjalan = false;

        CancelInvoke(nameof(LanjutSampah));

        if (sortingPanel != null)
            sortingPanel.SetActive(false);
    }
}
