
using UnityEngine;
using TMPro;
using System.Collections;

public class Day3Narration : MonoBehaviour
{
    [Header("Narration")]
    public TextMeshProUGUI narrationText;

    [Header("Music Transition")]
    public Day3MusicTransition musicTransition;

    private void Start()
    {
        StartCoroutine(PlayNarration());
    }

    IEnumerator PlayNarration()
    {
        narrationText.text = "";

        // NARASI 1 - DESA MULAI PULIH
        narrationText.text =
            "Akhirnya, kehidupan di desa mulai kembali seperti semula...";

        yield return new WaitForSeconds(4f);

        // NARASI 2 - WARGA KEMBALI BERAKTIVITAS
        narrationText.text =
            "Warga pun kembali menjalani hari-hari mereka.";

        yield return new WaitForSeconds(4f);

        // NARASI 3 - MULAI MUNCUL KEJANGGALAN
        narrationText.text =
            "Namun, ada sesuatu yang menarik perhatianku...";

        // Transisi musik damai ke musik misterius
        if (musicTransition != null)
        {
            musicTransition.TransitionToMystery();
        }

        yield return new WaitForSeconds(4f);

        // NARASI 4 - MELIHAT ANAK MEMBAWA SESUATU
        narrationText.text =
            "Anak itu... apa yang dibawanya?";

        yield return new WaitForSeconds(4f);

        // NARASI 5 - MULAI CURIGA
        narrationText.text =
            "Jangan-jangan...!";

        yield return new WaitForSeconds(4f);

        narrationText.text = "";
    }
}
