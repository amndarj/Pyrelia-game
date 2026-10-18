using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    public RectTransform target;
    public FirstAidMiniGame miniGame;
    public bool isWood;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Mulai menyeret item.");
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float distance = Vector2.Distance(
            rectTransform.position,
            target.position
        );

        if (distance < 300f)
        {
            rectTransform.position = target.position;

            if (isWood)
            {
                miniGame.PasangKayuBerhasil();
            }
            else
            {
                miniGame.PasangPerbanBerhasil();
            }
        }
    }
}