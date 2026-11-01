
using UnityEngine;

public class Day2MissionManager : MonoBehaviour
{
    public static Day2MissionManager instance;

    [Header("Data Masker")]
    public int totalMasker = 3;
    public int maskerDimiliki = 0;
    public int maskerDibagikan = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("Day2MissionManager duplikat!");
            return;
        }

        instance = this;
    }

    public void AmbilMasker(int jumlah)
    {
        maskerDimiliki += jumlah;

        Debug.Log("Berhasil mengambil masker!");
        Debug.Log("Masker dimiliki: " + maskerDimiliki);
    }

    public void CatatPembagianMasker()
    {
        maskerDibagikan++;

        Debug.Log(
            "Progres pembagian masker: " +
            maskerDibagikan + "/" + totalMasker
        );

        if (maskerDibagikan >= totalMasker)
        {
            Debug.Log("MISI PEMBAGIAN MASKER SELESAI!");
        }
    }
}
