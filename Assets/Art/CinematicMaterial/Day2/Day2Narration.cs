
using UnityEngine;
using TMPro;
using System.Collections;

public class Day2Narration : MonoBehaviour
{
    public TextMeshProUGUI narrationText;

    private void Start()
    {
        StartCoroutine(PlayNarration());
    }

    IEnumerator PlayNarration()
    {
        narrationText.text = "";

        narrationText.text = "Dulu, tempat ini begitu hidup...";
        yield return new WaitForSeconds(4f);

        narrationText.text = "";
        yield return new WaitForSeconds(1f);

        narrationText.text = "Kini, bahkan angin pun membawa jejak kehancuran.";
        yield return new WaitForSeconds(4f);

        narrationText.text = "";
        yield return new WaitForSeconds(1f);

        narrationText.text = "Perlahan, kami mulai merasakan akibatnya.";
        yield return new WaitForSeconds(3f);

        narrationText.text = "";
        yield return new WaitForSeconds(0.5f);

        narrationText.text = "Tak seorang pun tahu seberapa lama keadaan ini akan berlangsung...";
        yield return new WaitForSeconds(3f);

        narrationText.text = "";
        yield return new WaitForSeconds(0.5f);

        narrationText.text = "Aku tak bisa membiarkan semuanya terus begini.";
        yield return new WaitForSeconds(5f);

        narrationText.text = "";
    }
}
