using UnityEngine;

public class PlantingPointInteractable : MonoBehaviour, IInteractable
{
    public GameObject plantingMinigamePanel;

    private bool sudahDitanam = false;

    public void Interact()
    {
        if (sudahDitanam)
            return;

        if (plantingMinigamePanel != null)
        {
            plantingMinigamePanel.SetActive(true);
            Debug.Log("Minigame menanam pohon dimulai!");
        }
        else
        {
            Debug.LogError("Planting Minigame Panel belum dihubungkan!");
        }
    }
}

