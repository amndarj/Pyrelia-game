using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlantingToolDrag : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum ToolType
    {
        Sekop,
        Bibit,
        Penyiram
    }

    public ToolType toolType;

    [Header("Sprite Saat Diseret")]
    public Sprite spriteSaatDrag;

    private Vector3 posisiAwal;
    private CanvasGroup canvasGroup;
    private Image image;
    private Sprite spriteAwal;
    private RectTransform rectTransform;
    private Canvas canvas;

    [HideInInspector]
    public bool berhasilDigunakan;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        image = GetComponent<Image>();

        if (image != null)
            spriteAwal = image.sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        posisiAwal = rectTransform.position;
        berhasilDigunakan = false;

        canvasGroup.blocksRaycasts = false;

        if (image != null && spriteSaatDrag != null)
            image.sprite = spriteSaatDrag;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas != null)
        {
            rectTransform.position +=
                (Vector3)(eventData.delta / canvas.scaleFactor);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        rectTransform.position = posisiAwal;

        if (image != null)
            image.sprite = spriteAwal;
    }
}
