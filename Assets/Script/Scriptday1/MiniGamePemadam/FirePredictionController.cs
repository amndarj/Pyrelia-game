
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class FirePredictionController : MonoBehaviour
{
    public WindSystemController windSystem;
    public TMP_Text predictionText;

    public Transform[] firePoints;
    public GameObject[] warningMarkers;

    public float predictionDistance = 11f;

    private HashSet<int> predictedFires = new HashSet<int>();

    void Start()
    {
        UpdatePrediction();
        InvokeRepeating(nameof(UpdatePrediction), 1f, 1f);
    }

    public void UpdatePrediction()
    {
        if (windSystem == null || predictionText == null ||
            firePoints == null || warningMarkers == null)
            return;

        predictedFires.Clear();

        Vector2 windDirection =
            GetWindVector(windSystem.GetWindDirection());

        // Setiap titik api menjadi sumber prediksi.
        for (int i = 0; i < firePoints.Length; i++)
        {
            if (firePoints[i] == null)
                continue;

            // Periksa titik api lain sebagai target.
            for (int j = 0; j < firePoints.Length; j++)
            {
                if (i == j || firePoints[j] == null)
                    continue;

                Vector2 offset =
                    (Vector2)firePoints[j].position -
                    (Vector2)firePoints[i].position;

                float distance = offset.magnitude;

                if (distance <= 0 || distance > predictionDistance)
                    continue;

                float dot = Vector2.Dot(
                    offset.normalized,
                    windDirection
                );

                if (dot > 0.5f)
                {
                    predictedFires.Add(j);
                }
            }
        }

        UpdateWarningMarkers();
        ShowPrediction();
    }

    Vector2 GetWindVector(string direction)
    {
        switch (direction)
        {
            case "Timur":
                return Vector2.right;

            case "Selatan":
                return Vector2.down;

            case "Barat":
                return Vector2.left;

            case "Utara":
                return Vector2.up;

            default:
                return Vector2.right;
        }
    }

    void UpdateWarningMarkers()
    {
        for (int i = 0; i < warningMarkers.Length; i++)
        {
            if (warningMarkers[i] != null)
            {
                warningMarkers[i].SetActive(
                    predictedFires.Contains(i)
                );
            }
        }
    }

    void ShowPrediction()
    {
        if (predictedFires.Count == 0)
        {
            predictionText.text =
                "Belum ada titik api yang diprediksi menyebar.";
            return;
        }

        List<int> sortedFires =
            new List<int>(predictedFires);

        sortedFires.Sort();

        string result =
            "Angin berpotensi menyebarkan api ke titik: ";

        foreach (int index in sortedFires)
        {
            result += (index + 1) + ", ";
        }

        predictionText.text =
            result.TrimEnd(',', ' ') + "!";
    }
}
