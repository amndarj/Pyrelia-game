using UnityEngine;

public class TrashPickupInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (Day2MissionManager.instance == null)
        {
            Debug.LogWarning("Day2MissionManager tidak ditemukan!");
            return;
        }

        Day2MissionManager.instance.CatatSampahDiambil();

        Destroy(gameObject);
    }
}
