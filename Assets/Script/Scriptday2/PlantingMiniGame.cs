using UnityEngine;
using TMPro;

public class PlantingMiniGame : MonoBehaviour
{
    [Header("Gambar Tahapan Tanah")]
    public GameObject tanahImage;
    public GameObject tanahBerlubangImage;
    public GameObject tanahBibitTanamanImage;
    public GameObject tanahBibitTanamanTumbuhImage;

    [Header("Alat Menanam")]
    public GameObject sekopImage;
    public GameObject bibitTanamanImage;
    public GameObject wateringCanImage;
    public GameObject wateringCanBasahImage;

    [Header("UI Minigame")]
    public TMP_Text instructionText;
    public TMP_Text progressText;
    public TMP_Text feedbackText;
    public GameObject successPanel;

    private int tahap = 0;

    private void Start()
    {
        tanahImage.SetActive(true);
        tanahBerlubangImage.SetActive(false);
        tanahBibitTanamanImage.SetActive(false);
        tanahBibitTanamanTumbuhImage.SetActive(false);

        sekopImage.SetActive(true);
        bibitTanamanImage.SetActive(true);
        wateringCanImage.SetActive(true);
        wateringCanBasahImage.SetActive(false);

        successPanel.SetActive(false);

        tahap = 0;
        UpdateProgress();

        instructionText.text = "Seret sekop ke area tanah!";
        feedbackText.text = "";
    }

    public bool TryUseTool(PlantingToolDrag.ToolType tool)
    {
        if (tool == PlantingToolDrag.ToolType.Sekop && tahap == 0)
        {
            tanahImage.SetActive(false);
            tanahBerlubangImage.SetActive(true);

            tahap = 1;
            instructionText.text = "Bagus! Sekarang tanam bibitnya!";
            feedbackText.text = "Tanah berhasil digali!";
        }
        else if (tool == PlantingToolDrag.ToolType.Bibit && tahap == 1)
        {
            tanahBerlubangImage.SetActive(false);
            tanahBibitTanamanImage.SetActive(true);

            tahap = 2;
            instructionText.text = "Sekarang siram bibit tanaman!";
            feedbackText.text = "Bibit berhasil ditanam!";
        }
        else if (tool == PlantingToolDrag.ToolType.Penyiram && tahap == 2)
        {
            tanahBibitTanamanImage.SetActive(false);
            tanahBibitTanamanTumbuhImage.SetActive(true);

            tahap = 3;
            instructionText.text = "Yeay! Pohon berhasil ditanam!";
            feedbackText.text = "Penanaman selesai!";

            successPanel.SetActive(true);
        }
        else
        {
            feedbackText.text = "Belum waktunya menggunakan alat itu!";
            return false;
        }

        UpdateProgress();
        return true;
    }

    private void UpdateProgress()
    {
        progressText.text = "Tahap " + Mathf.Min(tahap + 1, 3) + "/3";
        
        if (tahap == 3)
            progressText.text = "Selesai 3/3";
    }

    public void CloseMiniGame()
    {
        gameObject.SetActive(false);
    }
}
