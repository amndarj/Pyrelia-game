
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour,
    IPointerDownHandler,
    IDragHandler,
    IPointerUpHandler
{
    [Header("Joystick UI")]
    public RectTransform background;
    public RectTransform handle;

    [Header("Settings")]
    public float handleRange = 1f;

    public Vector2 Direction { get; private set; }

    public float Horizontal => Direction.x;
    public float Vertical => Direction.y;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background,
            eventData.position,
            eventData.pressEventCamera,
            out position))
        {
            Vector2 radius = background.sizeDelta / 2f;

            Direction = new Vector2(
                position.x / radius.x,
                position.y / radius.y
            );

            Direction = Vector2.ClampMagnitude(Direction, 1f);

            handle.anchoredPosition = new Vector2(
                Direction.x * radius.x * handleRange,
                Direction.y * radius.y * handleRange
            );
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Direction = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}
