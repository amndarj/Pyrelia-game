
using System.Collections;
using UnityEngine;
using TMPro;

public class OpeningSequence : MonoBehaviour
{
    [Header("Slide Objects")]
    public GameObject slide1;
    public GameObject slide2;
    public GameObject slide3;

    [Header("Animators")]
    public Animator cameraAnimator;
    public Animator leafAnimator;

    [Header("Narration")]
    public TextMeshProUGUI narrationText;
    public CanvasGroup narrationGroup;
    public float narrationFadeDuration = 0.5f;
    public float slide1HoldDuration = 4.5f;
    public float slide2HoldDuration = 4.5f;
    public float slide3HoldDuration = 2.8f;
    public float narrationGapDuration = 0.2f;

    [Header("Durations")]
    public float cameraMoveDuration = 8.67f;
    public float leafInDuration = 12f;
    public float leafOutDuration = 2f;
    public float waitBeforeSlide3 = 18f;
    public float slide3FadeDuration = 3f;

    private SpriteRenderer[] slide3Renderers;

    private IEnumerator Start()
    {
        Debug.Log("OPENING START BERJALAN");

        if (slide1 == null || slide2 == null || slide3 == null ||
            cameraAnimator == null || leafAnimator == null ||
            narrationText == null || narrationGroup == null)
        {
            Debug.LogError("Ada referensi Opening Manager yang belum diisi!");
            yield break;
        }

        slide3Renderers =
            slide3.GetComponentsInChildren<SpriteRenderer>(true);

        // Kondisi awal
        slide1.SetActive(true);
        slide2.SetActive(false);
        slide3.SetActive(false);
        leafAnimator.gameObject.SetActive(false);

        narrationGroup.alpha = 0f;

        // NARASI SLIDE 1
        string[] slide1Lines =
        {
            "Liburan semester akhirnya tiba. Setelah sekian lama, aku bisa kembali ke tempat yang selalu kurindukan.",
            "Rasanya, waktu seolah berjalan lebih lambat di desa ini.",
            "Mungkin, ada alasan kenapa sejauh apa pun aku pergi, tempat ini selalu terasa seperti rumah."
        };

        Coroutine slide1Narration =
            StartCoroutine(PlayNarration(
                slide1Lines, slide1HoldDuration
            ));

        // Tunggu kamera dan narasi Slide 1 selesai
        yield return new WaitForSeconds(cameraMoveDuration);
        yield return slide1Narration;

        // TRANSISI SLIDE 1 KE SLIDE 2
        leafAnimator.gameObject.SetActive(true);
        leafAnimator.Play("Leaf_Transition_In", 0, 0f);

        yield return new WaitForSeconds(leafInDuration);

        slide1.SetActive(false);
        slide2.SetActive(true);

        cameraAnimator.SetTrigger("Start_Slide2");

        // NARASI SLIDE 2
        string[] slide2Lines =
        {
            "Pagi itu seharusnya berjalan seperti biasanya.",
            "Sampai sesuatu di kejauhan mengusik ketenangan kami."
        };

        StartCoroutine(PlayNarration(
            slide2Lines, slide2HoldDuration
        ));

        leafAnimator.Play("Leaf_Transition_Out", 0, 0f);

        yield return new WaitForSeconds(leafOutDuration);

        leafAnimator.gameObject.SetActive(false);

        // Tunggu hingga 18 detik sejak Slide 2 muncul
        yield return new WaitForSeconds(
            Mathf.Max(0f, waitBeforeSlide3 - leafOutDuration)
        );

        // TRANSISI SLIDE 2 KE SLIDE 3
        slide3.SetActive(true);

        foreach (SpriteRenderer sr in slide3Renderers)
        {
            Color c = sr.color;
            c.a = 0f;
            sr.color = c;
        }

        float elapsed = 0f;

        while (elapsed < slide3FadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(
                elapsed / slide3FadeDuration
            );

            foreach (SpriteRenderer sr in slide3Renderers)
            {
                Color c = sr.color;
                c.a = alpha;
                sr.color = c;
            }

            yield return null;
        }

        foreach (SpriteRenderer sr in slide3Renderers)
        {
            Color c = sr.color;
            c.a = 1f;
            sr.color = c;
        }

        slide2.SetActive(false);

        // NARASI SLIDE 3
        string[] slide3Lines =
        {
            "Asap itu ternyata berasal dari hutan.",
            "Dan pagi yang tenang itu... menjadi awal dari sesuatu yang tak pernah kubayangkan..."
        };

        yield return StartCoroutine(PlayNarration(
            slide3Lines, slide3HoldDuration
        ));

        Debug.Log("NARASI OPENING SELESAI");
    }

    private IEnumerator PlayNarration(
        string[] lines, float holdDuration)
    {
        foreach (string line in lines)
        {
            narrationText.text = line;

            yield return StartCoroutine(
                FadeNarration(0f, 1f)
            );

            yield return new WaitForSeconds(holdDuration);

            yield return StartCoroutine(
                FadeNarration(1f, 0f)
            );

            yield return new WaitForSeconds(
                narrationGapDuration
            );
        }
    }

    private IEnumerator FadeNarration(float start, float end)
    {
        float elapsed = 0f;

        while (elapsed < narrationFadeDuration)
        {
            elapsed += Time.deltaTime;

            narrationGroup.alpha = Mathf.Lerp(
                start,
                end,
                Mathf.Clamp01(
                    elapsed / narrationFadeDuration
                )
            );

            yield return null;
        }

        narrationGroup.alpha = end;
    }
}
