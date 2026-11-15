
using UnityEngine;
using TMPro;
using System.Collections;

public class Day3Narration : MonoBehaviour
{
    public TextMeshProUGUI narrationText;

    private void Start()
    {
        StartCoroutine(PlayNarration());
    }

    IEnumerator PlayNarration()
    {
        narrationText.text = "";

        narrationText.text = "Akhirnya, kehidupan di desa mulai kembali seperti semula...";
        yield return new WaitForSeconds(4f);

        narrationText.text = "Warga pun kembali menjalani hari-hari mereka.";
        yield return new WaitForSeconds(4f);

        narrationText.text = "Namun, ada sesuatu yang menarik perhatianku...";
        yield return new WaitForSeconds(4f);

        narrationText.text = "Anak itu... apa yang dibawanya?";
        yield return new WaitForSeconds(4f);

        narrationText.text = "Jangan-jangan...!";
        yield return new WaitForSeconds(4f);

        narrationText.text = "";
    }
}
