using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FirePoint : MonoBehaviour
{
    public GameObject fireVisual;
    public GameObject warningMarker;
    public float fadeDuration = 2f;

    private bool isExtinguished = false;

    public void Extinguish()
    {
        if (isExtinguished)
            return;

        isExtinguished = true;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        List<SpriteRenderer> renderers = new List<SpriteRenderer>();

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
            float alpha = 1f - Mathf.Clamp01(elapsedTime / fadeDuration);

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
}