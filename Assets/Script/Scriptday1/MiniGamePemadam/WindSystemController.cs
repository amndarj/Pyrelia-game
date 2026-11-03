using UnityEngine;
using TMPro;

public class WindSystemController : MonoBehaviour
{
    public RectTransform windArrow;
    public TMP_Text windDirectionText;

    private string[] directions =
    {
        "Timur",
        "Selatan",
        "Barat",
        "Utara"
    };

    private float[] rotations =
    {
        0f,
        -90f,
        180f,
        90f
    };

    private int currentDirection = 0;

    void Start()
    {
        UpdateWind();
        InvokeRepeating(nameof(ChangeWind), 10f, 10f);
    }

    void ChangeWind()
    {
        currentDirection = (currentDirection + 1) % directions.Length;

        UpdateWind();

        Debug.Log("Arah angin berubah: " + directions[currentDirection]);
    }

    void UpdateWind()
    {
        windDirectionText.text = "Angin: " + directions[currentDirection];

        windArrow.rotation = Quaternion.Euler(
            0f, 0f, rotations[currentDirection]
        );
    }

    public string GetWindDirection()
    {
        return directions[currentDirection];
    }
}