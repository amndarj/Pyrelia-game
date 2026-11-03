using UnityEngine;

public class SprayHitDetector : MonoBehaviour
{
    public RectTransform efekSemprotan;
    public Camera mainCamera;
    public FirePoint[] firePoints;

    public float jangkauanSemprotan = 150f;

    private void Update()
    {
        if (efekSemprotan == null || mainCamera == null)
            return;

        if (!efekSemprotan.gameObject.activeInHierarchy)
            return;

        Vector3 posisiSemprotan =
            efekSemprotan.position;

        foreach (FirePoint firePoint in firePoints)
        {
            if (firePoint == null || firePoint.IsExtinguished())
                continue;

            Vector3 posisiApi =
                mainCamera.WorldToScreenPoint(firePoint.transform.position);

            float jarak = Vector2.Distance(
                posisiSemprotan,
                posisiApi
            );

            if (jarak <= jangkauanSemprotan)
            {
                firePoint.Extinguish();
            }
        }
    }
}