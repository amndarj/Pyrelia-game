using UnityEngine;

public class TrashPickupInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (Day2MissionManager.instance == null)
            return;

        if (!Day2MissionManager.instance.BolehAmbilSampah)
            return;

        Day2MissionManager.instance.CatatSampahDiambil();

        Destroy(gameObject);
    }
}