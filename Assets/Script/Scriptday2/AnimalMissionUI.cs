using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimalMissionUI : MonoBehaviour
{
    [Header("Progress Hewan")]
    public TMP_Text progressText;
    public Image progressFill;

    public void UpdateProgress(int jumlah, int total)
    {
        if (progressText != null)
        {
            progressText.text =
                "Hewan Dirawat: " + jumlah + "/" + total;
        }

        if (progressFill != null && total > 0)
        {
            progressFill.fillAmount =
                Mathf.Clamp01((float)jumlah / total);
        }
    }
}

