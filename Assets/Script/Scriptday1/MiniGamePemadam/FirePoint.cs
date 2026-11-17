using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FirePoint : MonoBehaviour
{
    public GameObject fireVisual;
    public GameObject warningMarker;
    public float fadeDuration = 2f;

    public FireCounterManager fireCounterManager;

    private bool isExtinguished = false;

    private string SaveKey =>
        "Day1_" + gameObject.name + "_Extinguished";

    private void Start()
    {
        // Pulihkan status api dari penyimpanan.
        isExtinguished =
            PlayerPrefs.GetInt(SaveKey, 0) == 1;

        if (isExtinguished)
        {
            // Api yang sudah padam tidak boleh muncul lagi.
            if (fireVisual != null)
                fireVisual.SetActive(false);

            if (warningMarker != null)
                warningMarker.SetActive(false);
        }
    }

    public void Extinguish()
    {
        if (isExtinguished)
            return;

        isExtinguished = true;

        // Simpan status titik api.
        PlayerPrefs.SetInt(SaveKey, 1);
        PlayerPrefs.Save();

        if (fireCounterManager != null)
        {
            fireCounterManager.AddExtinguishedFire();
        }

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        List<SpriteRenderer> renderers =
            new List<SpriteRenderer>();

        if (fireVisual != null)
        {
            renderers.AddRange(
                fireVisual.GetComponentsInChildren<SpriteRenderer>(true)
            );
        }

        if (warningMarker != null)
        {
            renderers.AddRange(
                warningMarker.GetComponentsInChildren<SpriteRenderer>(true)
            );
        }

        float elapsedTime = 0f;

        Dictionary<SpriteRenderer, Color> originalColors =
            new Dictionary<SpriteRenderer, Color>();

        foreach (SpriteRenderer renderer in renderers)
        {
            if (renderer != null)
                originalColors[renderer] = renderer.color;
        }

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float alpha =
                1f - Mathf.Clamp01(elapsedTime / fadeDuration);

            foreach (var item in originalColors)
            {
                if (item.Key != null)
                {
                    Color color = item.Value;
                    color.a = alpha;
                    item.Key.color = color;
                }
            }

            yield return null;
        }

        if (fireVisual != null)
            fireVisual.SetActive(false);

        if (warningMarker != null)
            warningMarker.SetActive(false);

        Debug.Log(gameObject.name + " berhasil dipadamkan!");
    }

    public bool IsExtinguished()
    {
        return isExtinguished;
    }

    public void ResetFirePoint()
    {
        isExtinguished = false;

        PlayerPrefs.DeleteKey(SaveKey);
        PlayerPrefs.Save();

        if (fireVisual != null)
        {
            fireVisual.SetActive(true);
            ResetSpriteAlpha(fireVisual);
        }

        if (warningMarker != null)
        {
            warningMarker.SetActive(true);
            ResetSpriteAlpha(warningMarker);
        }
    }

    private void ResetSpriteAlpha(GameObject target)
    {
        SpriteRenderer[] renderers =
            target.GetComponentsInChildren<SpriteRenderer>(true);

        foreach (SpriteRenderer renderer in renderers)
        {
            Color color = renderer.color;
            color.a = 1f;
            renderer.color = color;
        }
    }
}
