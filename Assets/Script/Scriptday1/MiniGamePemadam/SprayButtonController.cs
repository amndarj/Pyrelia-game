using UnityEngine;
using UnityEngine.EventSystems;

public class SprayButtonController : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler
{
    public GameObject efekSemprotan;

    private void Start()
    {
        if (efekSemprotan != null)
            efekSemprotan.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (efekSemprotan != null)
            efekSemprotan.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (efekSemprotan != null)
            efekSemprotan.SetActive(false);
    }

    private void OnDisable()
    {
        if (efekSemprotan != null)
            efekSemprotan.SetActive(false);
    }
}