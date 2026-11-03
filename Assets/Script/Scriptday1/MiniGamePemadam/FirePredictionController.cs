using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class FirePredictionController : MonoBehaviour
{
    public WindSystemController windSystem;
    public TMP_Text predictionText;

    public Transform[] firePoints;

    public int mainFireIndex = 0;

    public float predictionDistance = 5f;

    private List<int> predictedFires = new List<int>();

    void Start()
    {
        UpdatePrediction();
        InvokeRepeating(nameof(UpdatePrediction), 1f, 1f);
    }

    public void UpdatePrediction()
    {
        if (windSystem == null || predictionText == null ||
            firePoints == null || firePoints.Length == 0)
            return;

        predictedFires.Clear();

        if (mainFireIndex < 0 || mainFireIndex >= firePoints.Length)
            return;

        Transform mainFire = firePoints[mainFireIndex];

        if (mainFire == null)
            return;

        Vector2 windDirection = GetWindVector(
            windSystem.GetWindDirection()
        );

        for (int i = 0; i < firePoints.Length; i++)
        {
            if (i == mainFireIndex || firePoints[i] == null)
                continue;

            Vector2 offset = firePoints[i].position - mainFire.position;

            float distance = offset.magnitude;

            if (distance > 0 && distance <= predictionDistance)
            {
                float dot = Vector2.Dot(
                    offset.normalized,
                    windDirection
                );

                if (dot > 0.5f)
                {
                    predictedFires.Add(i);
                }
            }
        }

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

    void ShowPrediction()
    {
        if (predictedFires.Count == 0)
        {
            predictionText.text =
                "Prediksi: Belum ada titik api yang terancam.";
            return;
        }

        string result = "Prediksi penyebaran api: ";

        foreach (int index in predictedFires)
        {
            result += "Fire_" + (index + 1) + " ";
        }

        predictionText.text = result;
    }
}