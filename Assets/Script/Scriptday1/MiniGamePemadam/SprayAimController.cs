using UnityEngine;

public class SprayAimController : MonoBehaviour
{
    public VirtualJoystick virtualJoystick;
    public RectTransform tanganDamkar;

    public float kecepatanGeser = 300f;
    public float batasKiri = -500f;
    public float batasKanan = 500f;

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
            posisiAwal.x += arahX * kecepatanGeser * Time.deltaTime;

            posisiAwal.x = Mathf.Clamp(
                posisiAwal.x,
                batasKiri,
                batasKanan
            );

            tanganDamkar.anchoredPosition = posisiAwal;
        }
    }
}