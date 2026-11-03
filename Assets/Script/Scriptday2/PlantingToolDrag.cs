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

    [HideInInspector]
    public bool berhasilDigunakan;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        image = GetComponent<Image>();

        if (image != null)
            spriteAwal = image.sprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        posisiAwal = transform.position;
        berhasilDigunakan = false;

        canvasGroup.blocksRaycasts = false;

        if (image != null && spriteSaatDrag != null)
            image.sprite = spriteSaatDrag;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // Semua alat kembali ke posisi awal
        transform.position = posisiAwal;

        // Sprite kembali ke kondisi awal
        if (image != null)
            image.sprite = spriteAwal;
    }
}
