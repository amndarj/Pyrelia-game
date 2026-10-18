using UnityEngine;

public class ItemPerban : MonoBehaviour, IInteractable
{
    public ItemManager itemManager;

    public void Interact()
    {
        if (!itemManager.questStarted)
        {
            return;
        }

        Debug.Log("Perban diambil!");

        itemManager.hasCloth = true;
        itemManager.CheckItems();

        gameObject.SetActive(false);
    }
}