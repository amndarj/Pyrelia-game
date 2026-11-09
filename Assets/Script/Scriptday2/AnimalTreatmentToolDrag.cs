using UnityEngine;
using UnityEngine.EventSystems;

public enum JenisAlatPerawatan
{
    Sikat,
    ObatLuka,
    Perban
}

[RequireComponent(typeof(CanvasGroup))]
public class AnimalTreatmentToolDrag : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Jenis Alat")]
    public JenisAlatPerawatan jenisAlat;

    [Header("Treatment Manager")]
    public AnimalTreatmentManager treatmentManager;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Transform parentAwal;
    private Vector3 posisiAwal;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("MULAI DRAG: " + jenisAlat);

        parentAwal = transform.parent;
        posisiAwal = rectTransform.position;

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.position = posisiAwal;
        transform.SetParent(parentAwal);

        canvasGroup.blocksRaycasts = true;
    }
}
