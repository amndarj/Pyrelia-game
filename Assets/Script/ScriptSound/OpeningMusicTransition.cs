
using System.Collections;
using UnityEngine;

public class OpeningMusicTransition : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource peacefulMusic;
    public AudioSource fireMusic;

    [Header("Settings")]
    public float fadeDuration = 3f;
    public float peacefulVolume = 0.4f;
    public float fireVolume = 0.5f;

    private bool transitioned = false;

    private void Start()
    {
        peacefulMusic.volume = 0f;
        fireMusic.volume = 0f;

        peacefulMusic.Play();
        StartCoroutine(FadeIn(peacefulMusic, peacefulVolume));
    }

    // Dipanggil saat adegan kebakaran dimulai
    public void TransitionToFire()
    {
        if (transitioned) return;

        transitioned = true;
        StartCoroutine(CrossfadeMusic());
    }

    private IEnumerator CrossfadeMusic()
    {
        fireMusic.Play();

        float elapsed = 0f;
        float startPeaceful = peacefulMusic.volume;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / fadeDuration);

            peacefulMusic.volume = Mathf.Lerp(
                startPeaceful, 0f, progress
            );

            fireMusic.volume = Mathf.Lerp(
                0f, fireVolume, progress
            );

            yield return null;
        }

        peacefulMusic.volume = 0f;
        peacefulMusic.Stop();

        fireMusic.volume = fireVolume;
    }

    private IEnumerator FadeIn(AudioSource source, float targetVolume)
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            source.volume = Mathf.Lerp(
                0f, targetVolume, elapsed / fadeDuration
            );

            yield return null;
        }

        source.volume = targetVolume;
    }
}

