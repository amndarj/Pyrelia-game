using UnityEngine;

public class ItemKayu : MonoBehaviour, IInteractable
{
    public ItemManager itemManager;

    public void Interact()
    {
        if (!itemManager.questStarted)
        {
            return;
        }

        Debug.Log("Kayu diambil!");

        itemManager.hasWood = true;
        itemManager.CheckItems();

        gameObject.SetActive(false);
    }
}