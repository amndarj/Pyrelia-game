using UnityEngine;

public class SprayAimController : MonoBehaviour
{
    public VirtualJoystick virtualJoystick;
    public RectTransform tanganDamkar;

    public float jarakGeser = 150f;

    private Vector2 posisiAwal;

    private void Start()
    {
        posisiAwal = tanganDamkar.anchoredPosition;
    }

    private void Update()
    {
        float arahX = virtualJoystick.InputDirection.x;

        if (Mathf.Abs(arahX) > 0.01f)
        {
            posisiAwal += new Vector2(
                arahX * jarakGeser * Time.deltaTime,
                0f
            );

            tanganDamkar.anchoredPosition = posisiAwal;
        }
    }
}