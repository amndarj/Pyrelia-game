using UnityEngine;
using TMPro;

public class FireCounterManager : MonoBehaviour
{
    public TMP_Text fireCounterText;

    private int jumlahApiPadam = 0;
    private int totalApi = 4;

    private void Start()
    {
        UpdateCounter();
    }

    public void AddExtinguishedFire()
    {
        jumlahApiPadam++;

        if (jumlahApiPadam > totalApi)
            jumlahApiPadam = totalApi;

        UpdateCounter();
    }

    private void UpdateCounter()
    {
        fireCounterText.text =
            "Api Dipadamkan: " + jumlahApiPadam + "/" + totalApi;
    }
}