
using UnityEngine;

public class PoskoMaskerInteractable : MonoBehaviour, IInteractable
{
    public int jumlahMasker = 3;

    private bool sudahDiambil = false;

    public void Interact()
    {
        if (sudahDiambil)
        {
            Debug.Log("Masker sudah diambil.");
            return;
        }

        if (Day2MissionManager.instance == null)
        {
            Debug.LogError("Day2MissionManager tidak ditemukan!");
            return;
        }

        if (!Day2MissionManager.instance.BolehInteraksiMasker)
        return;

        Day2MissionManager.instance.AmbilMasker(jumlahMasker);

        sudahDiambil = true;

        gameObject.SetActive(false);
    }
}
