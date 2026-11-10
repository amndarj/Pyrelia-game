
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public bool hasWood = false;
    public bool hasCloth = false;
    public bool questStarted = false;

    public GameObject questObjective;
    public MissionObjective missionObjective;

    private bool itemsCollected = false;

    public bool HasFirstAidItems()
    {
        return hasWood && hasCloth;
    }

    public void CheckItems()
    {
        if (HasFirstAidItems() && !itemsCollected)
        {
            itemsCollected = true;

            if (questObjective != null)
                questObjective.SetActive(true);

            if (missionObjective != null)
            {
                missionObjective.UpdateObjective(
                    "Berikan pertolongan pertama kepada anak SMA."
                );
            }

            Debug.Log("Kayu dan perban berhasil dikumpulkan!");
        }
    }
}
