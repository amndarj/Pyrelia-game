using UnityEngine;
using UnityEngine.EventSystems;

public class PlantingDropArea : MonoBehaviour, IDropHandler
{
    public PlantingMiniGame plantingMiniGame;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        PlantingToolDrag tool =
            eventData.pointerDrag.GetComponent<PlantingToolDrag>();

        if (tool == null || plantingMiniGame == null)
            return;

        bool berhasil = plantingMiniGame.TryUseTool(tool.toolType);

        if (berhasil)
        {
            tool.berhasilDigunakan = true;
        }
    }
}
