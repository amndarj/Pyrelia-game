using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public bool hasWood = false;
    public bool hasCloth = false;
    public bool questStarted = false;

    public GameObject questObjective;

    public bool HasFirstAidItems()
    {
        return hasWood && hasCloth;
    }

    public void CheckItems()
    {
        if (HasFirstAidItems())
        {
            questObjective.SetActive(true);
        }
    }
}