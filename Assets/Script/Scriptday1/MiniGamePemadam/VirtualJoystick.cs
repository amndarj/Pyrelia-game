using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour,
    IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform joystickBackground;
    public RectTransform joystickHandle;

    public Vector2 InputDirection { get; private set; }

    private float handleRange;

    private void Start()
    {
        handleRange = joystickBackground.sizeDelta.x / 2f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBackground,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint))
        {
            InputDirection = Vector2.ClampMagnitude(
                localPoint / handleRange, 1f);

            joystickHandle.anchoredPosition =
                InputDirection * handleRange;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputDirection = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }
}