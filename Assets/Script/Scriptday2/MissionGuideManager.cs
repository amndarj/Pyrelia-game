using UnityEngine;
using TMPro;

public class MissionGuideManager : MonoBehaviour
{
    [Header("UI Mission Guide")]
    public TMP_Text titleText;
    public TMP_Text step1;
    public TMP_Text step2;
    public TMP_Text step3;
    public TMP_Text step4;
    public TMP_Text step5;

    private TMP_Text[] stepTexts;
    private string[] langkahAktif;
    private int indexLangkah;

    private void Awake()
    {
        stepTexts = new TMP_Text[]
        {
            step1, step2, step3, step4, step5
        };
    }

    public void TampilkanMisi(string judul, string[] langkah)
    {
        titleText.text = judul;
        langkahAktif = langkah;
        indexLangkah = 0;

        TampilkanLangkahAktif();
    }

    private void TampilkanLangkahAktif()
    {
        for (int i = 0; i < stepTexts.Length; i++)
        {
            bool aktif = i == indexLangkah &&
                         indexLangkah < langkahAktif.Length;

            stepTexts[i].gameObject.SetActive(aktif);

            if (aktif)
                stepTexts[i].text = langkahAktif[indexLangkah];
        }
    }

    public bool LangkahBerikutnya()
    {
        if (langkahAktif == null ||
            indexLangkah >= langkahAktif.Length)
            return false;

        indexLangkah++;
        TampilkanLangkahAktif();

        return indexLangkah >= langkahAktif.Length;
    }
}
