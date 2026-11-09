using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimalTreatmentManager : MonoBehaviour
{
    [Header("Panel Perawatan")]
    public GameObject treatmentPanel;

    [Header("Informasi Hewan")]
    public TMP_Text animalNameText;
    public TMP_Text injuryDescriptionText;
    public Image animalImage;

    [Header("Status Tahapan")]
    public TMP_Text treatmentStatusText;

    [Header("Tombol Selesai")]
    public GameObject closeButton;

    private AnimalTreatmentInteractable hewanAktif;
    private int tahapSekarang = 0;
    private bool panelTerbuka = false;

    private readonly JenisAlatPerawatan[] urutanAlat =
    {
        JenisAlatPerawatan.Sikat,
        JenisAlatPerawatan.ObatLuka,
        JenisAlatPerawatan.Perban
    };

    public void BukaPanelPerawatan(AnimalTreatmentInteractable hewan)
    {

        if (panelTerbuka || hewan == null)
            return;

        hewanAktif = hewan;
        tahapSekarang = 0;

        if (treatmentPanel != null)
        {
            treatmentPanel.SetActive(true);

            Canvas.ForceUpdateCanvases();

            Debug.Log("Panel aktif: " + treatmentPanel.activeInHierarchy);
            Debug.Log("Panel posisi: " +
                treatmentPanel.GetComponent<RectTransform>().position);
                
            Debug.Log("Panel yang diaktifkan: " + treatmentPanel.name);
            Debug.Log("Status panel: " + treatmentPanel.activeSelf);
            Debug.Log("Status hierarchy: " + treatmentPanel.activeInHierarchy);
        }

        if (animalNameText != null)
            animalNameText.text = hewan.namaHewan;

        if (injuryDescriptionText != null)
            injuryDescriptionText.text = hewan.deskripsiLuka;

        if (animalImage != null)
        {
            animalImage.sprite = hewan.spriteAwal;
            animalImage.preserveAspect = true;
        }
        
        PerbaruiInstruksi();
        panelTerbuka = true;

        if (closeButton != null)
        {
            closeButton.SetActive(false);
        }
    }


    public void ProsesAlat(JenisAlatPerawatan alat)
    {
        if (!panelTerbuka || hewanAktif == null)
        {
            Debug.Log("PROSES BERHENTI!");
            return;
        }

        Debug.Log("ALAT DITERIMA: " + alat);
        Debug.Log("TAHAP SEKARANG: " + tahapSekarang);

        if (alat != urutanAlat[tahapSekarang])
        {
            if (treatmentStatusText != null)
                treatmentStatusText.text = "Urutan alat belum tepat!";

            return;
        }

        tahapSekarang++;

        if (tahapSekarang >= urutanAlat.Length)
        {
            SelesaikanPerawatan();
            return;
        }

        PerbaruiInstruksi();
    }


    private void PerbaruiInstruksi()
    {
        if (treatmentStatusText == null)
            return;

        switch (tahapSekarang)
        {
            case 0:
                treatmentStatusText.text = "Tahap 1: Gunakan sikat!";
                break;
            case 1:
                treatmentStatusText.text = "Tahap 2: Gunakan obat luka!";
                break;
            case 2:
                treatmentStatusText.text = "Tahap 3: Gunakan perban!";
                break;
        }
    }

    public void SelesaikanPerawatan()
    {
        if (hewanAktif == null)
            return;

        hewanAktif.SelesaikanPerawatan();

        if (animalImage != null)
        {
            animalImage.sprite = hewanAktif.spriteSetelahDirawat;
            animalImage.preserveAspect = true;
        }

        if (Day2MissionManager.instance != null)
        {
            Day2MissionManager.instance.CatatHewanDirawat(
                hewanAktif.namaHewan
            );
        }

        if (treatmentStatusText != null)
        {
            treatmentStatusText.text = "Yeay! Hewan berhasil dirawat!";
        }

        if (closeButton != null)
        {
            closeButton.SetActive(true);
        }
        else
        {
            Debug.LogWarning("CloseButton belum dihubungkan!");
        }
    }

    public void TutupPanelPerawatan()
    {
        if (treatmentPanel != null)
            treatmentPanel.SetActive(false);

        hewanAktif = null;
        panelTerbuka = false;
        tahapSekarang = 0;
    }
}
