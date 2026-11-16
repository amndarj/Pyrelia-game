
using System.Collections;
using UnityEngine;

public class Day3MusicTransition : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource peacefulMusic;
    public AudioSource mysteriousMusic;

    [Header("Volume")]
    [Range(0f, 1f)]
    public float peacefulVolume = 0.4f;

    [Range(0f, 1f)]
    public float mysteriousVolume = 0.5f;

    [Header("Transition")]
    public float fadeDuration = 3f;

    private bool transitioned = false;

    private void Start()
    {
        if (peacefulMusic == null || mysteriousMusic == null)
        {
            Debug.LogError("Audio Source Day 3 belum diisi!");
            return;
        }

        peacefulMusic.volume = 0f;
        mysteriousMusic.volume = 0f;

        mysteriousMusic.Stop();

        peacefulMusic.Play();

        StartCoroutine(FadeIn(
            peacefulMusic,
            peacefulVolume
        ));
    }

    public void TransitionToMystery()
    {
        if (transitioned) return;

        if (peacefulMusic == null || mysteriousMusic == null)
            return;

        transitioned = true;

        StartCoroutine(CrossfadeMusic());
    }

    private IEnumerator CrossfadeMusic()
    {
        mysteriousMusic.Play();

        float elapsed = 0f;
        float startVolume = peacefulMusic.volume;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            float progress = Mathf.Clamp01(
                elapsed / fadeDuration
            );

            peacefulMusic.volume = Mathf.Lerp(
                startVolume,
                0f,
                progress
            );

            mysteriousMusic.volume = Mathf.Lerp(
                0f,
                mysteriousVolume,
                progress
            );

            yield return null;
        }

        peacefulMusic.volume = 0f;
        peacefulMusic.Stop();

        mysteriousMusic.volume = mysteriousVolume;
    }

    private IEnumerator FadeIn(
        AudioSource source,
        float targetVolume)
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            source.volume = Mathf.Lerp(
                0f,
                targetVolume,
                Mathf.Clamp01(elapsed / fadeDuration)
            );

            yield return null;
        }

        source.volume = targetVolume;
    }
}
