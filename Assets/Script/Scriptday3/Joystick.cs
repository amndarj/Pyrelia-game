
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour,
    IPointerDownHandler,
    IDragHandler,
    IPointerUpHandler
{
    [Header("Joystick UI")]
    public RectTransform joystickBG;
    public RectTransform joystickMove;

    [Header("Settings")]
    [Range(0.1f, 1.5f)]
    public float handleRange = 1.2f;

    public Vector2 Direction { get; private set; }

    public float Horizontal => Direction.x;
    public float Vertical => Direction.y;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBG,
            eventData.position,
            eventData.pressEventCamera,
            out localPosition))
        {
            Vector2 radius = (joystickBG.rect.size -
                              joystickMove.rect.size) / 1f;

            radius *= handleRange;

            Vector2 rawDirection = new Vector2(
                localPosition.x / radius.x,
                localPosition.y / radius.y
            );

            Direction = Vector2.ClampMagnitude(rawDirection, 1f);

            joystickMove.anchoredPosition = new Vector2(
                Direction.x * radius.x,
                Direction.y * radius.y
            );
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Direction = Vector2.zero;
        joystickMove.anchoredPosition = Vector2.zero;
    }
}
