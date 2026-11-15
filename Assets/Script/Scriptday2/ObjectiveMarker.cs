using UnityEngine;

public class ObjectiveMarker : MonoBehaviour
{
    public float tinggi = 0.25f;
    public float kecepatan = 2f;

    private Vector3 posisiAwal;

    private void Start()
    {
        posisiAwal = transform.localPosition;
    }

    private void Update()
    {
        float gerakan = Mathf.Sin(Time.time * kecepatan) * tinggi;

        transform.localPosition =
            posisiAwal + Vector3.up * gerakan;
    }
}
